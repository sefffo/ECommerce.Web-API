using AutoMapper;
using Ecommerce.Abstraction.Services;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Domain.Models.Identity;
using Ecommerce.Shared.DTOs.IdentityDto_s;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.businessServices
{
    public class AuhenticationService(UserManager<ApplicationUser> userManager, IConfiguration configuration , IMapper mapper) : IAuthenticationService
    {

        public async Task<UserDto> LoginAsync(LoginDto dto)
        {
            var User = await userManager.FindByEmailAsync(dto.Email) ?? throw new UserNotFound(dto.Email);

            var ValidPassword = await userManager.CheckPasswordAsync(User, dto.Password);
            if (ValidPassword)
            {
                return new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = dto.Email,
                    Token = await CreateTokenAsync(User)
                };
            }
            else
            {
                throw new UnAuthorizedException();
            }
        }

        public async Task<UserDto> RegisterAsync(RegisterDto dto)
        {
            var User = new ApplicationUser()
            {
                DisplayName = dto.DisplayName,
                Email = dto.Email,
                PhoneNumber = dto.phoneNumber,
                UserName = dto.Username,
            };
            var Result = await userManager.CreateAsync(User, dto.Password);
            if (Result.Succeeded)
            {
                return new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = await CreateTokenAsync(User)
                };
            }
            else
            {
                //validation
                var Errors = Result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }

        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto dto)
        {
            var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(e => e.Email == email)??throw new UserNotFound(email);

            if(user.Address is not null)
            {
                user.Address.Street = dto.Street;
                user.Address.City = dto.City;
                user.Address.Country = dto.Country;
                user.Address.FirstName  = dto.FirstName;
                user.Address.LastName = dto.LastName;
            }
            else
            {
                user.Address = mapper.Map<AddressDto, Address>(dto);

            }
            await userManager.UpdateAsync(user);
            return mapper.Map<Address, AddressDto>(user.Address);//already added user address
        }


        public async Task<bool> CheckEmailAsync(string Email)
        {
            var user = await userManager.FindByEmailAsync(Email);
            return user != null;
        }

        public async Task<AddressDto> GetCurrentUserAddressAsync(string Email)
        {
            var user = await userManager.Users.Include(u=>u.Address).FirstOrDefaultAsync(e=>e.Email == Email) ?? throw new UserNotFound(Email);
            if (user is not null)
            {
                return mapper.Map<Address,AddressDto>(user.Address);
            }
            else
            {
                throw new AddressNotFound(user.DisplayName);
            }

        }

        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            return new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await CreateTokenAsync(user),
            };
        }


        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new(ClaimTypes.Email,user.Email),
                new(ClaimTypes.Name,user.UserName),
                new(ClaimTypes.NameIdentifier,user.Id),

            };
            var Roles = await userManager.GetRolesAsync(user);

            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityKey = configuration.GetSection("JWTOptions")["SecurityKey"];

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken
                (
                    issuer: configuration.GetSection("JWTOptions")["Issuer"],
                    audience: configuration.GetSection("JWTOptions")["Audience"],
                    claims: Claims,
                    expires: DateTime.Now.AddDays(2),
                    signingCredentials: Credentials

                );
            return new JwtSecurityTokenHandler().WriteToken(Token);

        }

        // Method parameters come from GOOGLE (not user input), extracted in the controller from Google's OAuth response
        // email: User's Gmail address (e.g., "john@gmail.com")
        // displayName: User's full name from Google profile (e.g., "John Doe")
        // googleId: Unique Google user ID (e.g., "108234567890123456789") - identifies this specific Google account
        public async Task<UserDto> GetOrCreateGoogleUserAsync(string email, string displayName, string googleId)
        {
            // Step 1: Check if a user with this email already exists in YOUR database
            // This handles the case where someone already registered with email/password
            var user = await userManager.FindByEmailAsync(email);

            // Step 2: If user doesn't exist, create a new account
            if (user == null)
            {
                // Create new ApplicationUser object with Google account data
                user = new ApplicationUser
                {
                    // Store the Google email as user's email
                    Email = email,

                    // Generate unique username: "john_a3f4" (takes part before @, adds random string to avoid duplicates)
                    // Example: "john@gmail.com" becomes "john_a3f4" 
                    UserName = email.Split('@')[0] + "_" + Guid.NewGuid().ToString().Substring(0, 4),

                    // Use Google's display name, or fallback to email prefix if Google didn't provide name
                    // Example: "John Doe" OR "john" (from john@gmail.com)
                    DisplayName = displayName ?? email.Split('@')[0],

                    // Google OAuth doesn't provide phone number, so leave empty
                    PhoneNumber = ""
                };

                // Step 3: Create the user in YOUR database (saves to AspNetUsers table)
                var result = await userManager.CreateAsync(user);

                // Step 4: Check if user creation succeeded
                if (!result.Succeeded)
                {
                    // If creation failed (e.g., username conflict), throw exception
                    throw new FailedToCreateGoogleAccount("Failed to create user from Google account");
                }

                // Step 5: Link this user account to their Google account
                // This creates a record in AspNetUserLogins table
                // Parameters: ("Google", googleId, "Google")
                //   - Provider: "Google" (which OAuth provider)
                //   - ProviderKey: googleId (unique ID from Google)
                //   - ProviderDisplayName: "Google" (friendly name to display)
                var loginInfo = new UserLoginInfo("Google", googleId, "Google"); //fe gadwal tb3 el idenity m3mol lkeda 

                // Save the external login association
                await userManager.AddLoginAsync(user, loginInfo);
            }
            // If user already exists (found by email), skip creation and continue

            // Step 6: Generate YOUR app's JWT token for this user
            // This token contains claims (email, roles, etc.) and is used for subsequent API calls
            var token = await CreateTokenAsync(user);

            // Step 7: Return the user data with JWT token
            return new UserDto
            {
                Email = user.Email,           // User's email
                DisplayName = user.DisplayName, // User's display name
                Token = token                  // JWT token for authentication
            };
        }



    }
}
