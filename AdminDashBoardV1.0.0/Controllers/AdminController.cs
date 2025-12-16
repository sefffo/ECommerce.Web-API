using Ecommerce.Domain.Models.Identity;
using Ecommerce.Shared.DTOs.IdentityDto_s;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdminDashBoardV1._0._0.Controllers
{
    public class AdminController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(SignInManager<ApplicationUser> signInManager,
                               UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Displays the admin login page
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            // If user is already logged in, redirect to home
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// Handles email/password login for admin users
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AdminLoginDto login, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(login);
            }

            // Find user by email
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password");
                return View(login);
            }

            // Check if user is in Admin role
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (!isAdmin)
            {
                ModelState.AddModelError(string.Empty, "You are not authorized to access the admin dashboard");
                return View(login);
            }

            // Sign in with password
            var result = await _signInManager.PasswordSignInAsync(
                user,
                login.Password,
                isPersistent: login.RememberMe,
                lockoutOnFailure: true
            );

            if (result.Succeeded)
            {
                // Successful login
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Your account is locked due to multiple failed login attempts. Please try again later.");
                return View(login);
            }

            if (result.RequiresTwoFactor)
            {
                ModelState.AddModelError(string.Empty, "Two-factor authentication required");
                return View(login);
            }

            // Invalid password
            ModelState.AddModelError(string.Empty, "Invalid email or password");
            return View(login);
        }

        /// <summary>
        /// Logs out the current admin user
        /// </summary>
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // Sign out from Identity cookies
            await _signInManager.SignOutAsync();

            // Clean up external authentication cookie if any
            try
            {
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            }
            catch { }

            return RedirectToAction("Login");
        }

        /// <summary>
        /// Displays the access denied page
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        //===============================================
        // Google OAuth Authentication
        //===============================================

        /// <summary>
        /// Initiates Google OAuth login flow for admin dashboard
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleLogin()
        {
            // Sign out any existing sessions to start fresh
            await _signInManager.SignOutAsync();

            try
            {
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            }
            catch { }

            // Configure OAuth callback URL
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleCallback")
            };

            // Redirect to Google login
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Handles the OAuth callback from Google after user authentication
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleCallback()
        {
            // Authenticate using Identity's External scheme
            var result = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);

            if (!result.Succeeded || result.Principal == null)
            {
                TempData["Error"] = "Google authentication failed";
                return RedirectToAction("Login");
            }

            // Extract user information from Google claims
            var claims = result.Principal.Claims;
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                TempData["Error"] = "Email not provided by Google";
                return RedirectToAction("Login");
            }

            // Check if user exists in database
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                TempData["Error"] = "No admin account found for this Google account";
                return RedirectToAction("Login");
            }

            // Verify user has admin privileges
            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Admin") && !roles.Contains("SuperAdmin"))
            {
                TempData["Error"] = "Access denied. Admin privileges required.";
                return RedirectToAction("Login");
            }

            // Sign in the admin user with application cookie
            await _signInManager.SignInAsync(user, isPersistent: false);

            // Clean up external authentication cookie
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
