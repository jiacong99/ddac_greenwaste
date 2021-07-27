using DDAC_Assignment.Areas.Identity.Data;
using DDAC_Assignment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDAC_Assignment.Controllers
{
    public class RolesController : Controller
    {
        private readonly UserManager<DDAC_AssignmentUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public RolesController(UserManager<DDAC_AssignmentUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;

        }
        public async Task<IActionResult> Index()
        {


            var users = await _userManager.Users.ToListAsync();
            var viewUserRoles = new List<ViewUserRoles>();
            foreach (DDAC_AssignmentUser user in users)
            {
                var thisViewModel = new ViewUserRoles();
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.FullName = user.User_Full_Name;
                thisViewModel.Address = user.User_Address;
                thisViewModel.Roles = await GetUserRoles(user);
                viewUserRoles.Add(thisViewModel);
            }
            return View(viewUserRoles);
        }
        private async Task<List<string>> GetUserRoles(DDAC_AssignmentUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }



        //Manage button to change roles for users
        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            ViewBag.UserName = user.UserName;
            var model = new List<ManageRoles>();
            foreach (var role in _roleManager.Roles)
            {
                var viewUserRoles = new ManageRoles
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    viewUserRoles.Selected = true;
                }
                else
                {
                    viewUserRoles.Selected = false;
                }
                model.Add(viewUserRoles);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageRoles> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            return RedirectToAction("Index");
        }
    }
}
