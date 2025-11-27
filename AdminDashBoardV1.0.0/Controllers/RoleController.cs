using AdminDashBoardV1._0._0.Models;
using Ecommerce.Presistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AdminDashBoardV1._0._0.Controllers
{
    public class RoleController(RoleManager<IdentityRole> roleManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var Roles = await roleManager.Roles.ToListAsync();
            return View(Roles);
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var roleExsists = await roleManager.RoleExistsAsync(model.Name);
                if (!roleExsists)
                {
                    await roleManager.CreateAsync(new IdentityRole(model.Name));
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Name", "Role Name already exsists");
                    return View("Index", await roleManager.Roles.ToListAsync());
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public async Task<IActionResult> Delete(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                ModelState.AddModelError("Name", "Role Not Found");
            }
            else
            {
                string roleName = role.Name;
                var result = await roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var model = new RoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name
                };
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // 1) Find the role
            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ModelState.AddModelError("", "Role not found.");
                return RedirectToAction("Index");
            }

            // 2) Check if the new name already exists (but allow the same role to keep its name)
            var roleExists = await roleManager.RoleExistsAsync(model.Name);

            if (roleExists && role.Name != model.Name)
            {
                ModelState.AddModelError("Name", "Role name already exists.");
                return View(model);
            }

            // 3) Update the role
            role.Name = model.Name;
            var result = await roleManager.UpdateAsync(role);

            // 4) Handle Identity errors
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            return RedirectToAction("Index");


        }
    }
}
