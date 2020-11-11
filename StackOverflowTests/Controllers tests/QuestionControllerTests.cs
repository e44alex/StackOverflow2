using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using StackOverflow.Controllers;
using StackOverflowWebApi.Controllers;
using StackOverflowWebApi.Models;


namespace StackOverflowTests
{
    public class QuestionControllerTests
    {
        private AppDbContext _context;

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("testDB1");
            _context = new AppDbContext(builder.Options);

            _context.Add(new User() {Email = "e44alex@gmail.com", PasswordHash = AuthController.HashPassword("admin")});
            _context.Add(new Question());
            _context.Add(new Answer());
            _context.Add(new AnswerLiker());
            _context.SaveChangesAsync();
        }

        [Test]
        public void Test_QuestionsController_GetQuestions_ReturnsListOfQuestions()
        {

            QuestionsController controller = new QuestionsController(_context);

            var result = controller.GetQuestions();

            Assert.IsNotEmpty(result.Result.Value);
        }

        [Test]
        public void Test_QuestionsController_GetQuestionById_ReturnsQuestion()
        {
            QuestionsController controller = new QuestionsController(_context);

            var result = controller.GetQuestion(_context.Questions.Select(x => x.Id).FirstOrDefault()).Result.Value;

            Assert.IsInstanceOf<Question>(result);
        }

        [Test]
        public void Test_QuestionsController_PostQuestion()
        {
            QuestionsController controller = new QuestionsController(_context);

            var result = controller.PostQuestion(new Question()
            {
                Id = Guid.NewGuid(),
                Creator = _context.Users.FirstOrDefault(),
                Body = "test",
                Topic = "test"
            }).Result;

            Assert.IsInstanceOf<ActionResult<Question>>(result);
        }

        [Test]
        public void Test_QuestionsController_PutQuestion()
        {
            QuestionsController controller = new QuestionsController(_context);

            var questionForUpdate = _context.Questions.FirstOrDefault();
            var oldQuestion = _context.Questions.FirstOrDefault();
            questionForUpdate.Answers = new List<Answer>()
            {
                new Answer()
                {
                    Question = questionForUpdate,
                    Body = "test",
                    Creator = questionForUpdate.Creator
                }
            };

            controller.PutQuestion(questionForUpdate.Id, questionForUpdate);


            var changedQuestion = _context.Questions.Find(questionForUpdate.Id);


            //because of EF entity refs 
            Assert.AreEqual(oldQuestion, changedQuestion);
        }

        [Test]
        public void Test_QuestionsController_DeleteQuestion()
        {
            QuestionsController controller = new QuestionsController(_context);

            var questionForDelete = _context.Questions.FirstOrDefault();

            var result = controller.DeleteQuestion(questionForDelete.Id).Result.Value;

            Assert.AreEqual(questionForDelete, result);
        }

    }
}