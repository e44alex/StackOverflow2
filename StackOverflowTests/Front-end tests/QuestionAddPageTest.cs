using System;
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
        [Ignore("HttpContext used")]
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
            

            var subjectUnderTest = new QuestionCreateModel(apiClientMock.Object);
            subjectUnderTest.PageContext = new PageContext();
            subjectUnderTest.PageContext.HttpContext = new DefaultHttpContext();
            subjectUnderTest.PageContext.HttpContext.Response.Cookies.Append("token", "token");
            subjectUnderTest.PageContext.HttpContext.Response.Cookies.Append("user", "token");
            var question = new Question();
            var result = await subjectUnderTest.OnPostAsync(question);

            Assert.AreNotEqual(question.Id, Guid.Empty);
            Assert.IsNotNull(question.Creator);
            Assert.AreNotEqual(question.Creator.Id, Guid.Empty);
            Assert.IsInstanceOf<RedirectResult>(result);
        }
    }
}