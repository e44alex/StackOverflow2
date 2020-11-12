using System;
using System.Web;
using System.Net;
using System.Threading.Tasks;
using ApiFrontEnd.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using NUnit.Framework;
using StackOverflowWebApi.Models;
using StackOverflowWebApi.Services;

namespace StackOverflowTests
{
    public class QuestionAddPageTest
    {
        [Test]
        public async Task QuestionCreatePage_OnPost_Works()
        {
            var apiClientMock = new Mock<IApiClient>();
            apiClientMock
                .Setup(x => x.AddQuestionAsync(new Question(), "token"))
                .ReturnsAsync(true);

            apiClientMock
                .Setup(x => x.GetUserIdAsync("Username"))
                .ReturnsAsync(Guid.NewGuid());

            var requestCookieCollection = new Mock<IRequestCookieCollection>();
            requestCookieCollection.Setup(x => x["token"]).Returns("token");
            requestCookieCollection.Setup(x => x["user"]).Returns("user");

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(x => x.Request.Cookies)
                .Returns(requestCookieCollection.Object);

            var subjectUnderTest = new QuestionCreateModel(apiClientMock.Object)
            {
                PageContext = new PageContext {HttpContext = httpContextMock.Object}
            };

            var question = new Question();
            var result = await subjectUnderTest.OnPostAsync(question);

            Assert.AreNotEqual(question.Id, Guid.Empty);
            Assert.IsNotNull(question.Creator);
            Assert.IsInstanceOf<RedirectResult>(result);
        }
    }
}