using System.Threading.Tasks;
using ApiFrontEnd.Pages.Account.Manage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using NUnit.Framework;
using StackOverflowWebApi.Models;
using StackOverflowWebApi.Services;

namespace StackOverflowTests.AccountTests
{
    public class IndexTest
    {
        [Test]
        public async Task IndexModel_OnGetAsync_SetsUser()
        {
            var apiClientMock = new Mock<IApiClient>();
            apiClientMock.Setup(x => x.GetUserDataAsync("username"))
                .ReturnsAsync(new User()
                {
                    Email = "username"
                });

            var requestCookieCollection = new Mock<IRequestCookieCollection>();
            requestCookieCollection.Setup(x => x["token"]).Returns("token");
            requestCookieCollection.Setup(x => x["user"]).Returns("user");

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(x => x.Request.Cookies)
                .Returns(requestCookieCollection.Object);

            var subjectUnderTest = new IndexModel(apiClientMock.Object)
            {
                PageContext = new PageContext { HttpContext = httpContextMock.Object }
            };



            var result = await subjectUnderTest.OnGetAsync();

            Assert.IsInstanceOf<IActionResult>(result);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task IndexModel_OnPostAsync_SetsDataAndReturnsPage()
        {
            var user = new User(){Email = "username"};

            var apiClientMock = new Mock<IApiClient>();
            apiClientMock.Setup(x => x.GetUserDataAsync("username"))
                .ReturnsAsync(new User()
                {
                    Email = "username"
                });

            apiClientMock.Setup(x => x.UpdateUserAsync(user,"token"))
                .ReturnsAsync(true);

            var requestCookieCollection = new Mock<IRequestCookieCollection>();
            requestCookieCollection.Setup(x => x["token"]).Returns("token");
            requestCookieCollection.Setup(x => x["user"]).Returns("username");

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(x => x.Request.Cookies)
                .Returns(requestCookieCollection.Object);

            var subjectUnderTest = new IndexModel(apiClientMock.Object)
            {
                PageContext = new PageContext {HttpContext = httpContextMock.Object}, 
                Input = new IndexModel.InputModel(){Bio = "Bio"}
            };


            var result = await subjectUnderTest.OnPostAsync();

            Assert.IsInstanceOf<IActionResult>(result);
            Assert.IsNotNull(result);
        }
    }
}