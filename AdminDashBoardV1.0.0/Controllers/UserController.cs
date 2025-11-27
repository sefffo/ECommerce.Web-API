using AdminDashBoardV1._0._0.Models;
using AdminDashBoardV1._0._0.Views.User;
using Ecommerce.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AdminDashBoardV1._0._0.Controllers
{
    public class UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : Controller
    {

        //user names and the asigend roles
        public async Task<IActionResult> Index()
        {
            var user = await userManager.Users.Select(u => new UserViewModel
            {
                Id = u.Id,
                DisplayName = u.DisplayName,
                Email = u.Email,
                UserName = u.UserName,
                PhoneNumber = u.PhoneNumber,
                Roles = userManager.GetRolesAsync(u).Result

            }).ToListAsync(); //return is a list from the model
            return View(user);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            var roles = await roleManager.Roles.ToListAsync();

            var mappedUser = new UserRoleViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                Roles = roles.Select(r => new RoleViewModel
                {
                    Name = r.Name,
                    IsSelected = userManager.IsInRoleAsync(user, r.Name).Result //to check if it has the role or not  
                }).ToList()
            };

            return View(mappedUser);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserRoleViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            var userRoles = await userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                if (userRoles.Any(r => r == role.Name && !role.IsSelected)) //if the role exist and is not selected remove it after i save changes 
                {
                    await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                if (!userRoles.Any(r => r == role.Name) && role.IsSelected) //if the role does not exist and is selected add it after i save changes
                {
                    await userManager.AddToRoleAsync(user, role.Name);
                }

            }
            return RedirectToAction("Index");
            #region diff implementation
            //var selectedRoles = model.Roles.Where(r => r.IsSelected).Select(r => r.Name);
            //var result = await userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));
            //if (!result.Succeeded)
            //{
            //    ModelState.AddModelError("", "Failed to add roles");
            //    return View(model);
            //}
            //result = await userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
            //if (!result.Succeeded)
            //{
            //    ModelState.AddModelError("", "Failed to remove roles");
            //    return View(model);
            //}
            //return RedirectToAction("Index");
            #endregion
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to delete user");
                    return View("Index", await userManager.Users.ToListAsync());
                }
            }
            ModelState.AddModelError("", "User not found");
            return View("Index", await userManager.Users.ToListAsync());
        }
    }
}