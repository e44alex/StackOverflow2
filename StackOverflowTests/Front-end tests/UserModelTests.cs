using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiFrontEnd.Pages;
using Moq;
using NUnit.Framework;
using StackOverflowWebApi.Models;
using StackOverflowWebApi.Services;

namespace StackOverflowTests
{
    public class UserModelTests
    {
        [Test]
        public async Task UserModel_OnGetAsync_SetsUser()
        {
            var apiClientMock = new Mock<IApiClient>();
            apiClientMock.Setup(x => x.GetUserDataAsync("username"))
                .ReturnsAsync(new User()
                {
                    Email = "username"
                });

            var subjectUnderTest = new UserModel(apiClientMock.Object);

            await subjectUnderTest.OnGet("username");

            Assert.IsNotNull(subjectUnderTest.ModelUser);
            Assert.AreEqual(subjectUnderTest.ModelUser.Email,"username");
        }
    }
}