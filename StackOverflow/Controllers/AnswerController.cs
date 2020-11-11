using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Models;

namespace StackOverflow.Controllers
{
    public class AnswerController : Controller
    {
        private AppDbContext _context;
        private UserManager<User> _userManager;

        public AnswerController(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // POST: Answer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Body")] Answer answer)
        {
            try
            {
                if (answer != null)
                {
                    answer.DateCreated = DateTime.Now;
                    answer.Creator = await _userManager.FindByNameAsync(User.Identity.Name);
                    answer.Id = new Guid();
                }

               

                return RedirectToAction(nameof(Index), nameof(QuestionController));
            }
            catch
            {
                return View();
            }
        }

        // GET: Answer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Answer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Answer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Answer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}