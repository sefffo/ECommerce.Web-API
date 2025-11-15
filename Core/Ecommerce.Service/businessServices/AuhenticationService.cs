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
    }
}
