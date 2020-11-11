using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackOverflow.Models;

namespace StackOverflow.Controllers
{
    public class QuestionController : Controller
    {
        private AppDbContext _context;
        private UserManager<User> _userManager;

        public QuestionController(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Question/Index/5
        public async Task<ViewResult> Index(Guid id)
        {
            var appContext = _context.Questions
                .Include(q => q.Answers)
                .ThenInclude(q => q.Users)
                .Include(q => q.Creator);
            var question = await appContext.FirstOrDefaultAsync(q => q.Id == id);
            return View(question);
        }

        // GET: Question/Create
        public ActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("~/Identity/Account/Register");
            }

            return View();
        }

        //POST: Question/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Topic", "Body")] Question question)
        {
            try
            {
                if (question != null)
                {
                    question.DateCreated = DateTime.Now;
                    question.LastActivity = DateTime.Now;
                    question.Id = new Guid();
                    question.Creator = await _userManager.FindByNameAsync(User.Identity.Name);
                    question.Opened = true;

                    _context.Add(question);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index), new {id = question.Id});
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAnswer(Guid id, [Bind("Body")] Answer answer)
        {
            Question question = null;

            try
            {
                if (answer != null)
                {
                    question = await _context.Questions.FindAsync(id);

                    answer.DateCreated = DateTime.Now;
                    answer.Creator = await _userManager.FindByNameAsync(User.Identity.Name);
                    answer.Id = new Guid();

                    if (question.Answers == null)
                    {
                        question.Answers = new List<Answer>();
                    }

                    question.Answers.Add(answer);
                    question.LastActivity = DateTime.Now;

                    _context.Add(answer);
                    await _context.SaveChangesAsync();
                }

                return RedirectToActionPermanent(nameof(Index), new {id = question.Id});
            }
            catch
            {
                return View();
            }
        }


        public async Task<ActionResult> Like(Guid id, Guid questionId)
        {
            var question = await _context.Questions.FindAsync(questionId);
            var answer = await _context.Answers
                .Include(a =>a.Users)
                .FirstOrDefaultAsync(a => a.Id == id);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (!answer.Users.Any(u=> u.User.Equals(User)))
            {
                answer.Users.Add(new AnswerLiker()
                {
                    Id = new Guid(),
                    User = user,
                    Answer = answer
                });
                if (question.Creator.Equals(user))
                {
                    question.Opened = false;
                }

                user.Rating = await SetUserRatingAsync(user.UserName);

                await _userManager.UpdateAsync(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToActionPermanent(nameof(Index), new {id = questionId});
        }

        private async Task<int?> SetUserRatingAsync(string userName)
        {
            var totalQuestion = _context.Questions.Count(x => x.Creator.UserName == userName);
            var totalAnswers = _context.Answers.Count(x => x.Creator.UserName == userName);
            var likedAnswers = _context.Answers.Count(x => x.Creator.UserName == userName && x.Users.Count > 0);

            float result =((float)(totalQuestion + likedAnswers)) / (totalAnswers + totalQuestion);
            return (int?) Math.Round(result*100);
        }
    }
}