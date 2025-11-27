using Ecommerce.Domain.Models.Identity;
using Ecommerce.Shared.DTOs.IdentityDto_s;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashBoardV1._0._0.Controllers
{
    public class AdminController(SignInManager<ApplicationUser> _signInManager,
                                     UserManager<ApplicationUser> _userManager) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Invalid Email");
                return RedirectToAction("Login");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (!result.Succeeded || !await _userManager.IsInRoleAsync(user, "Admin"))
            {
                ModelState.AddModelError(string.Empty, "You are not Authorized");
                return RedirectToAction("Login");
            }
            else
                return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
