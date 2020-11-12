using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using ApiFrontEnd.Areas.Identity.Pages.Account;
using ApiFrontEnd.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StackOverflowWebApi.Models;
using StackOverflowWebApi.Services;

namespace StackOverflowTests.AccountTests
{
    public class LoginTest
    {
        [Test]
        public async Task LoginModel_OnPost_LoginsUser()
        {
            var apiClientMock = new Mock<IApiClient>();
            apiClientMock.Setup(x => x.Authenticate("username", "password"))
                .ReturnsAsync("token");

            var responseCookies = new Mock<IResponseCookies>();
            responseCookies.Setup(x => x.Append("token", "token".Encrypt()));
            responseCookies.Setup(x => x.Append("user", "username"));

            var httpContextMock = new Mock<HttpContext>();

            httpContextMock.Setup(x => x.Response.Cookies)
                .Returns(responseCookies.Object);


            var subjectUnderTest = new LoginModel(apiClientMock.Object)
            {
                PageContext = new PageContext(){HttpContext = httpContextMock.Object},

                Input = new LoginModel.InputModel()
                {
                    Username = "username",
                    Password = "password"
                }
            };

            await subjectUnderTest.OnPostAsync();

            Assert.IsNotNull(subjectUnderTest.HttpContext.Response.Cookies);
            Assert.Zero(subjectUnderTest.ModelState.ErrorCount);
        }

        [Test]
        public async Task LoginModel_OnPost_NotLoginsUser()
        {
            var apiClientMock = new Mock<IApiClient>();
            apiClientMock.Setup(x => x.Authenticate("username", "password"))
                .ReturnsAsync(string.Empty);

            var subjectUnderTest = new LoginModel(apiClientMock.Object)
            {
                Input = new LoginModel.InputModel()
                {
                    Username = "username",
                    Password = "password"
                }
            };

            await subjectUnderTest.OnPostAsync();

            
            Assert.NotZero(subjectUnderTest.ModelState.ErrorCount);
        }

        [Test]
        public async Task RegisterModel_OnPost_AddsModelError()
        {
            var user = new User()
            {
                Email = "",
                PasswordHash = "",
                Login = "",
                Name = "Name",
                Surname = "Surname",
                DateRegistered = DateTime.Now
            };
            var apiClientMock = new Mock<IApiClient>();
            apiClientMock.Setup(x => x.AddUserAsync(user))
                .ReturnsAsync(true);

            apiClientMock.Setup(x => x.Authenticate("username", "password"))
                .ReturnsAsync("token");

            var subjectUnderTest = new RegisterModel(apiClientMock.Object)
            {
                Input = new RegisterModel.InputModel()
                {
                    Email = "email@email.com",
                    Password = "password",
                    ConfirmPassword = "password",
                    Name = "Name",
                    Surname = "Surname"
                }
            };

            await subjectUnderTest.OnPostAsync();

            Assert.NotZero(subjectUnderTest.ModelState.ErrorCount);
        }

        [Test]
        public async Task LogOutModel_OnPost_DeletesCookies()
        {
            var apiClientMock = new Mock<IApiClient>();
           

            var responseCookies = new Mock<IResponseCookies>();
            responseCookies.Setup(x => x.Delete("token"));
            responseCookies.Setup(x => x.Delete("user"));

            var httpContextMock = new Mock<HttpContext>();

            httpContextMock.Setup(x => x.Response.Cookies)
                .Returns(responseCookies.Object);


            var subjectUnderTest = new LogoutModel(apiClientMock.Object)
            {
                PageContext = new PageContext() { HttpContext = httpContextMock.Object },

            };

            var result = await subjectUnderTest.OnPost();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IActionResult>(result);
            Assert.IsInstanceOf<RedirectResult>(result);
        }
    }
}