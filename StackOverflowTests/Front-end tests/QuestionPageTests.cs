using System;
using System.Collections.Generic;
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
    public class QuestionPageTests
    {
        [Test]
        public async Task QuestionPageModel_OnGetAsync_LoadsQuestion()
        {
            var apiClientMock = new Mock<IApiClient>();
            apiClientMock.Setup(x => x.GetQuestionAsync(Guid.Empty))
                .ReturnsAsync(new Question()
                {
                    Id = Guid.Empty,
                    Answers = new List<Answer>()
                    {
                        new Answer()
                    }
                });

            var subjectUnderTest = new QuestionModel(apiClientMock.Object);

            await subjectUnderTest.OnGet(Guid.Empty);

            Assert.IsNotNull(subjectUnderTest.Question);
            Assert.AreEqual(subjectUnderTest.Question.Id, Guid.Empty);
            Assert.IsNotEmpty(subjectUnderTest.Question.Answers);
        }

        [Test]
        public async Task QuestionPageModel_OnPostAsync_SendsAnswer()
        {
            var question = new Question();
            var answer = new Answer(){Question = question};

            var apiClientMock = new Mock<IApiClient>();
            apiClientMock
                .Setup(x => x.GetQuestionAsync(Guid.Empty))
                .ReturnsAsync(new Question());

            apiClientMock
                .Setup(x => x.GetUserIdAsync("Username"))
                .ReturnsAsync(Guid.NewGuid());

            apiClientMock
                .Setup(x => x.AddAnswerAsync(answer, "token"))
                .ReturnsAsync(true);

            var requestCookieCollection = new Mock<IRequestCookieCollection>();
            requestCookieCollection.Setup(x => x["token"]).Returns("token");
            requestCookieCollection.Setup(x => x["user"]).Returns("user");

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(x => x.Request.Cookies).Returns(requestCookieCollection.Object);

            var subjectUnderTest = new QuestionModel(apiClientMock.Object)
            {
                PageContext = new PageContext {HttpContext = httpContextMock.Object}
            };

            
            var result = await subjectUnderTest.OnPost(question.Id, answer);

            
            Assert.IsInstanceOf<RedirectResult>(result);
        }

        [Test]
        public async Task QuestionPageModel_OnLike_SetsLike()
        {
            var question = new Question();
            var answer = new Answer() { Question = question };

            var apiClientMock = new Mock<IApiClient>();
            apiClientMock
                .Setup(x => x.LikeAnswerAsync(answer.Id,"user", "token"))
                .ReturnsAsync(true);

            var requestCookieCollection = new Mock<IRequestCookieCollection>();
            requestCookieCollection.Setup(x => x["token"]).Returns("token");
            requestCookieCollection.Setup(x => x["user"]).Returns("user");

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(x => x.Request.Cookies).Returns(requestCookieCollection.Object);

            var subjectUnderTest = new QuestionModel(apiClientMock.Object)
            {
                PageContext = new PageContext { HttpContext = httpContextMock.Object }
            };

            var result = await subjectUnderTest.OnPostLike(answer, question);

            Assert.IsInstanceOf<RedirectResult>(result);
        }
    }
}