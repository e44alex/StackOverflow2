using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using StackOverflow.Controllers;
using StackOverflowWebApi.Controllers;
using StackOverflowWebApi.Models;


namespace StackOverflowTests
{
    public  class AuthControllerTests
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
        public void AuthController_HashPassword()
        {
            var password = "admin";

            var hash = AuthController.HashPassword(password);

             Assert.AreNotEqual(password, hash);
             Assert.AreNotEqual(AuthController.HashPassword(password), hash);
             Assert.True(AuthController.VerifyHashedPassword(hash,password));
        }

        [Test]
        public void AuthController_VerifyHashedPassword_EqualPasswords()
        {
            var pasword1 = "admin";
            var hash1 = AuthController.HashPassword(pasword1);
            
            Assert.True(AuthController.VerifyHashedPassword(hash1, pasword1));
        }

        [Test]
        public void AuthController_VerifyHashedPassword_NotEqualPasswords()
        {
            var pasword1 = "admin";
            var pasword2 = "admin2";
            var hash1 = AuthController.HashPassword(pasword1);

            Assert.False(AuthController.VerifyHashedPassword(hash1, pasword2));
        }
    }
}