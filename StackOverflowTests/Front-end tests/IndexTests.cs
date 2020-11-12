using System.Collections.Generic;
using System.Threading.Tasks;
using ApiFrontEnd;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using StackOverflowWebApi.Models;
using StackOverflowWebApi.Services;

namespace StackOverflowTests
{
    public class IndexTests
    {
        [Test]
        public async Task IndexModel_OnGetWithSearch_IncorrectSearch()
        {
            var apiClientMock = new Mock<IApiClient>();
            apiClientMock
                .Setup(x => x.GetQuestionsAsync())
                .ReturnsAsync(new List<Question>()
                {
                    new Question(){Topic = "redundant"},
                    new Question(){Topic = "searched"},
                });

            var subjectUnderTest = new IndexModel(apiClientMock.Object);

            await subjectUnderTest.OnGet("kavabanga!");

            Assert.IsEmpty(subjectUnderTest.Questions);

        }

        [Test]
        public async Task IndexModel_OnGetWithSearch_FiltersList()
        {
            var apiClientMock = new Mock<IApiClient>();
            apiClientMock
                .Setup(x => x.GetQuestionsAsync())
                .ReturnsAsync(new List<Question>()
                {
                    new Question(){Topic = "redundant"},
                    new Question(){Topic = "searched"},
                });

            var subjectUnderTest = new IndexModel(apiClientMock.Object);

            await subjectUnderTest.OnGet("searched");
             
            Assert.IsNotEmpty(subjectUnderTest.Questions);
            Assert.AreEqual( 1,subjectUnderTest.Questions.Count);

        }

        [Test]
        public async Task IndexModel_OnGet_SetsListOfQuestions()
        {
            var apiClientMock = new Mock<IApiClient>();
            apiClientMock
                .Setup(x => x.GetQuestionsAsync())
                .ReturnsAsync(new List<Question>()
                {
                    new Question()
                });

            var subjectUnderTest = new IndexModel(apiClientMock.Object);

            await subjectUnderTest.OnGet();

            Assert.IsNotEmpty(subjectUnderTest.Questions);

        }
    }
}