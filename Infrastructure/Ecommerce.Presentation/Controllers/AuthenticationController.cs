using Ecommerce.Abstraction.Services;
using Ecommerce.Shared.DTOs.IdentityDto_s;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;



namespace Ecommerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AuthenticationController(IServiceManger manger):ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto)
        {
            var User = await manger.AuthenticationService.LoginAsync(dto);
            return Ok(User);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
        {
            var User = await manger.AuthenticationService.RegisterAsync(dto);
            return Ok(User);
        }
        [Authorize]
        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var user = await manger.AuthenticationService.CheckEmailAsync(email);
            return Ok(user);
        }
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await manger.AuthenticationService.GetCurrentUserAsync(email);
            return Ok(user);
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<UserDto>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await manger.AuthenticationService.GetCurrentUserAddressAsync(email);
            return Ok(address);
        }
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto dto)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var updatedAddress = await manger.AuthenticationService.UpdateCurrentUserAddressAsync(Email, dto);
            return Ok(updatedAddress);

        }

        //===============================================
        // Google OAuth Endpoints
        //===============================================

        /// <summary>
        /// Initiates Google OAuth flow
        /// GET: api/Authentication/google-login
        /// Redirects user to Google's login page
        /// </summary>
        [HttpGet("google-login")]
        [AllowAnonymous] // Anyone can attempt to login
        public async Task<IActionResult> GoogleLogin()
        {
            // Prepare the redirect URL for after Google authentication
            // This tells Google where to send the user after they log in
            var redirectUrl = Url.Action(nameof(GoogleCallback), "Authentication", null, Request.Scheme);

            // Configure authentication properties
            var properties = new AuthenticationProperties
            {
                RedirectUri = redirectUrl, // Where to go after Google auth
                Items = { { "scheme", GoogleDefaults.AuthenticationScheme } }// Track which OAuth provider
            };

            // Challenge = tells ASP.NET to redirect user to Google's login page
            // Google will handle authentication, then redirect back to CallbackPath (/signin-google)

            await Task.CompletedTask;

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Handles Google OAuth callback
        /// GET: api/Authentication/google-callback
        /// Called AFTER /signin-google middleware processes Google's response
        /// </summary>
        [HttpGet("google-callback")]
        [AllowAnonymous] // No JWT needed yet - user is logging in
        public async Task<ActionResult<UserDto>> GoogleCallback()
        {
            // Step 1: Authenticate using the cookie set by /signin-google middleware
            // This cookie contains the user info that Google sent back
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Step 2: Check if authentication succeeded
            if (!result.Succeeded || result.Principal == null)
            {
                return BadRequest(new { error = "Google authentication failed" });
            }

            // Step 3: Extract user information from Google's claims
            var claims = result.Principal.Claims.ToList();

            // Get email from Google (required)
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            // Get display name from Google (optional)
            var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            // Get Google's unique user ID (required - this identifies the Google account)
            var googleId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            // Step 4: Validate that Google provided required information
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest(new { error = "Email not provided by Google" });
            }

            if (string.IsNullOrEmpty(googleId))
            {
                return BadRequest(new { error = "Google ID not provided" });
            }

            // Step 5: Get or create user in YOUR database and generate JWT token
            var user = await manger.AuthenticationService.GetOrCreateGoogleUserAsync(email, name, googleId);

            // Step 6: Clean up the temporary authentication cookie
            // We don't need it anymore since we have a JWT now
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Step 7: Return user data with JWT token
            // Client will use this JWT for all future API requests
            return Ok(user);
        }





    }
}
