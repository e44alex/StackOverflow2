using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using StackOverflowWebApi.Models;

namespace StackOverflowWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AnswersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Answers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswers()
        {
            return await _context.Answers.ToListAsync();
        }

        // GET: api/Answers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Answer>> GetAnswer(Guid id)
        {
            var answer = await _context.Answers
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (answer == null)
            {
                return NotFound();
            }

            return answer;
        }

        // GET: api/Answers/5
        [HttpGet("byQuestion/{id}")]
        public async Task<List<Answer>> GetAnswersByQuestion(Guid id)
        {
            return await _context.Answers
                .Include(a =>a.Question)
                .Where(a => a.Question.Id == id)
                .ToListAsync();
        }

        // PUT: api/Answers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswer(Guid id, Answer answer)
        {
            if (id != answer.Id)
            {
                return BadRequest();
            }

            _context.Entry(answer).State = EntityState.Modified;

            foreach (var user in answer.Users)
            {
                user.Answer = answer;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Answers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Answer>> PostAnswer(Answer answer)
        {
            var question = await _context.Questions.FindAsync(answer.Question.Id);
            question.LastActivity= DateTime.Now;

            answer.DateCreated = DateTime.Now;
            answer.Question = question;
            answer.Id = Guid.NewGuid();
            answer.Creator = await _context.FindAsync<User>(answer.Creator.Id);
            
            _context.Add(answer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnswer", new { id = answer.Id }, answer);
        }

        // DELETE: api/Answers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Answer>> DeleteAnswer(Guid id)
        {
            var answer = await _context.Answers.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }

            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();

            return answer;
        }
        [Authorize]
        [HttpGet("/like")]
        public async Task<IActionResult> LikeAnswer(Guid answerId, string username)
        {
            var answer = await _context.Answers
                .Include(x => x.Question)
                .FirstOrDefaultAsync(x =>x.Id == answerId);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == username);

            if (answer.Creator.Id == user.Id)
            {
                answer.Question.Opened = false;
            }

            _context.Add(new AnswerLiker
            {
                Id = Guid.NewGuid(),
                Answer = answer,
                User = user
            });

            user.Rating = await SetUserRatingAsync(user.Login);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<int?> SetUserRatingAsync(string userName)
        {
            var totalQuestion = _context.Questions.Count(x => x.Creator.Email == userName);
            var totalAnswers = _context.Answers.Count(x => x.Creator.Email == userName);
            var likedAnswers = _context.Answers.Count(x => x.Creator.Email == userName && x.Users.Count > 0);

            float result = ((float)(totalQuestion + likedAnswers)) / (totalAnswers + totalQuestion);
            return (int?)Math.Round(result * 100);
        }

        private bool AnswerExists(Guid id)
        {
            return _context.Answers.Any(e => e.Id == id);
        }
    }
}
