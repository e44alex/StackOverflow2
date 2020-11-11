using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Models;

namespace StackOverflow.Controllers
{
    public class UserController: Controller
    {
        private UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> UserPage(Guid id)
        {
            User user = await _userManager.FindByIdAsync(id.ToString());

            return View(user);
        }
    }
}
