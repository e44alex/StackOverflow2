using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;
using StackOverflowWebApi.Models;
using StackOverflowWebApi.Services;

namespace StackOverflowTests
{
    public class ApiClientTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task ApiClient_GetQuestions_ReturnsQuestionList()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ObjectContent(typeof(List<Question>), new List<Question>()
                    {
                        new Question()
                    },
                    new JsonMediaTypeFormatter())
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var subjectUnderTest = new ApiClient(httpClient);

            var result = await subjectUnderTest
                .GetQuestionsAsync();

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);

            // also check the 'http' call was like we expected it
            var expectedUri = new Uri("http://test.com/api/Questions");

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get
                        && req.RequestUri == expectedUri
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Test]
        public async Task ApiClient_GetQuestion_ReturnsQuestion()
        {
            var id = Guid.NewGuid();
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ObjectContent(typeof(Question), new Question()
                    {
                        Id = id
                    },
                        new JsonMediaTypeFormatter())
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var subjectUnderTest = new ApiClient(httpClient);

            var result = await subjectUnderTest
                .GetQuestionAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, id);

            // also check the 'http' call was like we expected it
            var expectedUri = new Uri("http://test.com/api/Questions/" + id);

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get
                    && req.RequestUri == expectedUri
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Test]
        public async Task ApiClient_GetAnswersByQuestion_ReturnsListOfAnswers()
        {
            var id = Guid.NewGuid();
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ObjectContent(typeof(List<Answer>), new List<Answer>()
                        {
                            new Answer()
                            {
                                Question = new Question(){Id = id}
                            }
                        },
                        new JsonMediaTypeFormatter())
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var subjectUnderTest = new ApiClient(httpClient);



            var result = await subjectUnderTest
                .GetAnswersByQuestionAsync(id);

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
            Assert.AreEqual(result[0].Question.Id, id);

            // also check the 'http' call was like we expected it
            var expectedUri = new Uri("http://test.com/api/Answers/byQuestion/" + id);

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get
                    && req.RequestUri == expectedUri
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Test]
        public async Task ApiClient_AddAnswerAsync_ReturnsTrue()
        {
            var guid = Guid.NewGuid();
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var subjectUnderTest = new ApiClient(httpClient);

            var result = await subjectUnderTest
                .AddAnswerAsync(new Answer(), "token");

            Assert.IsTrue(result);

            // also check the 'http' call was like we expected it
            var expectedUri = new Uri("http://test.com/api/Answers/");

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Post
                    && req.RequestUri == expectedUri
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Test]
        public async Task ApiClient_AddQuestionAsync_ReturnsTrue()
        {
            var guid = Guid.NewGuid();
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var subjectUnderTest = new ApiClient(httpClient);

            var result = await subjectUnderTest
                .AddQuestionAsync(new Question(), "token");

            Assert.IsTrue(result);

            // also check the 'http' call was like we expected it
            var expectedUri = new Uri("http://test.com/api/Questions");

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Post
                    && req.RequestUri == expectedUri
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Test]
        public async Task ApiClient_UpdateQuestionAsync_ReturnsTrue()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var subjectUnderTest = new ApiClient(httpClient);

            var result = await subjectUnderTest
                .UpdateQuestionAsync(new Question());

            Assert.IsTrue(result);

            // also check the 'http' call was like we expected it
            var expectedUri = new Uri("http://test.com/api/Questions/" + Guid.Empty);

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Put
                    && req.RequestUri == expectedUri
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Test]
        public async Task ApiClient_GetAnswerAsync_ReturnsAnswer()
        {
            var id = Guid.NewGuid();
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ObjectContent(typeof(Answer), new Answer()
                    {
                        Id = id
                    },
                        new JsonMediaTypeFormatter())
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var subjectUnderTest = new ApiClient(httpClient);

            var result = await subjectUnderTest
                .GetAnswerAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, id);

            // also check the 'http' call was like we expected it
            var expectedUri = new Uri("http://test.com/api/Answers/" + id);

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get
                    && req.RequestUri == expectedUri
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Test]
        public async Task ApiClient_UpdateAnswerAsync_ReturnsTrue()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var subjectUnderTest = new ApiClient(httpClient);

            var result = await subjectUnderTest
                .UpdateAnswerAsync(new Answer());

            Assert.IsTrue(result);

            // also check the 'http' call was like we expected it
            var expectedUri = new Uri("http://test.com/api/Answers/"+Guid.Empty);

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Put
                    && req.RequestUri == expectedUri
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Test]
        public async Task ApiClient_LikeAnswersAsync_ReturnsTrue()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var subjectUnderTest = new ApiClient(httpClient);
            var guid = Guid.NewGuid();

            var result = await subjectUnderTest
                .LikeAnswerAsync(guid, "username", "token");

            Assert.IsTrue(result);

            // also check the 'http' call was like we expected it
            var expectedUri = new Uri("http://test.com/like?answerId="+guid+"&username=username");

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get
                    && req.RequestUri == expectedUri
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Test]
        public async Task ApiClient_GetUserDataAsync_ReturnsUser()
        {
            string username = "username";
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ObjectContent(typeof(User), new User()
                    {
                        Email = username
                    },
                        new JsonMediaTypeFormatter())
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var subjectUnderTest = new ApiClient(httpClient);

            var result = await subjectUnderTest
                .GetUserDataAsync(username);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Email, username);

            // also check the 'http' call was like we expected it
            var expectedUri = new Uri("http://test.com/api/Users/byName/" + username);

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get
                    && req.RequestUri == expectedUri
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Test]
        public async Task ApiClient_GetUserIdAsync_ReturnsGuid()
        {
            var guid = Guid.NewGuid();
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ObjectContent(typeof(Guid), guid,
                        new JsonMediaTypeFormatter())
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var subjectUnderTest = new ApiClient(httpClient);

            var result = await subjectUnderTest
                .GetUserIdAsync("username");

            Assert.IsNotNull(result);
            Assert.AreEqual(result, guid);

            // also check the 'http' call was like we expected it
            var expectedUri = new Uri("http://test.com/api/Users/getId/username");

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get
                    && req.RequestUri == expectedUri
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Test]
        public async Task ApiClient_UpdateUserAsync_ReturnsTrue()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var subjectUnderTest = new ApiClient(httpClient);

            var result = await subjectUnderTest
                .UpdateUserAsync(new User(), "token");

            Assert.IsTrue(result);

            // also check the 'http' call was like we expected it
            var expectedUri = new Uri("http://test.com/api/Users/"+Guid.Empty);

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Put
                    && req.RequestUri == expectedUri
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Test]
        public async Task ApiClient_AddUserAsync_ReturnsTrue()
        {
            var guid = Guid.NewGuid();
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var subjectUnderTest = new ApiClient(httpClient);

            var result = await subjectUnderTest
                .AddUserAsync(new User());

            Assert.IsTrue(result);

            // also check the 'http' call was like we expected it
            var expectedUri = new Uri("http://test.com/api/Users");

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Post
                    && req.RequestUri == expectedUri
                ),
                ItExpr.IsAny<CancellationToken>()
            );
        }

        [Test]
        public async Task ApiClient_UnAuthenticate_ReturnsTrue()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var subjectUnderTest = new ApiClient(httpClient);

            var result = await subjectUnderTest
                .UnAuthenticate("inputUsername");

            var expectedUri = new Uri("http://test.com/logOut?username=inputUsername");


            Assert.IsTrue(result);

            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get
                    && req.RequestUri == expectedUri
                ),
                ItExpr.IsAny<CancellationToken>()
            );

        }

        [Test]
        public async Task ApiClient_Authenticate_ReturnsString()
        {
            var hrm = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{'access_token':'token','username':'username'}")
            };
            hrm.Headers.Add("token","token_here");

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(hrm)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            var subjectUnderTest = new ApiClient(httpClient);

            var result = await subjectUnderTest
                .Authenticate("username", "password");

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);

        }
    }
}