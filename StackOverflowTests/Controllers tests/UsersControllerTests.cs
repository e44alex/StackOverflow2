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
    public class UsersControllerTests
    {
        private AppDbContext _context;

        [SetUp]
        public void Setup()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("testDB1");
            _context = new AppDbContext(builder.Options);

            _context.Add(new User() { Email = "e44alex@gmail.com", PasswordHash = AuthController.HashPassword("admin") });
            _context.Add(new Question());
            _context.Add(new Answer());
            _context.Add(new AnswerLiker());
            _context.SaveChangesAsync();
        }

        [Test]
        public void UsersController_GetUsers_ReturnsListOfUsers()
        {
            var controller  = new UsersController(_context);

            var result = controller.GetUsers().Result.Value;

            Assert.IsNotEmpty(result);

        }

        [Test]
        public void UsersController_GetUserById_ReturnsUser()
        {
            var controller = new UsersController(_context);

            var result = controller.GetUser(_context.Users.Select(x=>x.Id).FirstOrDefault()).Result.Value;

            Assert.IsInstanceOf<User>(result);

        }

        [Test]
        public void UsersController_GetUserByUName_ReturnsUser()
        {
            var controller = new UsersController(_context);
            string uname = _context.Users.Select(x => x.Email).FirstOrDefault();
            var result = controller.GetUser(uname).Result.Value;

            Assert.IsInstanceOf<User>(result);
            Assert.AreEqual(result.Email, uname);
        }

        [Test]
        public void UsersController_GetId_ReturnsGuid()
        {
            var controller = new UsersController(_context);
            string usname = _context.Users.Select(x => x.Email).FirstOrDefault();

            var resultGuid = controller.GetId(usname).Result.Value;

            Assert.IsInstanceOf<Guid>(resultGuid);
            Assert.AreNotEqual(resultGuid, Guid.Empty);

        }

        [Test]
        public void UsersController_PutUser()
        {
            UsersController controller = new UsersController(_context);

            var user = _context.Users.FirstOrDefault();
            user.Email = "test@example.com";

            var result = controller.PutUser(user.Id, user).Result;


            var changedQuestion = _context.Users.Find(user.Id);


            //because of EF entity refs 
            Assert.AreEqual(user, changedQuestion);
        }

        [Test]
        public void UsersController_PostUser_ReturnsUser()
        {
            UsersController controller = new UsersController(_context);

            var result = controller.PostUser(new User()
            {
                Id =Guid.NewGuid(),
                Email = "test@email.com",
                Name = "test",
                Surname = "test",
                PasswordHash = "passw"
            }).Result;

            Assert.IsInstanceOf<ActionResult<User>>(result);
        }

        [Test]
        public void UsersController_DeleteUser_ReturnsUser()
        {
            UsersController controller = new UsersController(_context);
            int count = _context.Users.Count();
            var userForDelete = _context.Users.FirstOrDefault();

            var result = controller.DeleteUser(userForDelete.Id).Result.Value;

            Assert.Less(_context.Users.Count(), count);
            Assert.AreEqual(userForDelete, result);
        }
    }
}