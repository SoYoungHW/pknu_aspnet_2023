﻿using _01_myPortfolio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Diagnostics.Eventing.Reader;

namespace _01_myPortfolio.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            ViewData["NoScroll"] = "true";
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            ViewData["NoScroll"] = "true";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateRoleModel model)
        {
            if (ModelState.IsValid) 
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = model.RoleName
                };
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded) 
                {
                    return RedirectToAction("ListRoles", "Admin");
                }

                foreach (var error in result.Errors) 
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            ViewData["NoScroll"] = "true";
            var role = await _roleManager.FindByIdAsync(id);

            if (id == null)
            {
                TempData["error"] = $"권한이 없습니다.";
                return NotFound();
            }

            var model = new EditRoleModel()
            {
                Id = role.Id,
                RoleName = role.Name,
            };

            var userList = await _userManager.Users.ToListAsync();

            foreach (var user in userList) 
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(EditRoleModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                TempData["error"] = $"권한이 없습니다.";
                return NotFound();
            }

            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Admin");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewData["NoScroll"] = "true";
            ViewBag.roleId = roleId;

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                TempData["error"] = $"권한이 없습니다.";
                return NotFound();
            }

            var model = new List<UserRoleModel>();

            var userList = await _userManager.Users.ToListAsync();

            foreach (var user in userList) 
            {
                var userRoleViewModel = new UserRoleModel()
                {
                    UserId = user.Id,
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleModel> model, string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                TempData["error"] = $"권한이 없습니다.";
                return NotFound();
            }

            IdentityResult result = null;

            foreach (var user in model)
            {
                var userObj = await _userManager.FindByIdAsync(user.UserId);

                if (user.IsSelected && !(await _userManager.IsInRoleAsync(userObj, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(userObj, role.Name);
                }
                else if (!user.IsSelected && await _userManager.IsInRoleAsync(userObj, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(userObj, role.Name);    
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }
    }
}
