using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackOverflowWebApi.Models;

namespace StackOverflowWebApi.Services
{
    public interface IApiClient
    {
        Task<List<Question>> GetQuestionsAsync();
        Task<Question> GetQuestionAsync(Guid id);
        Task<Answer> GetAnswerAsync(Guid answerId);
        Task<List<Answer>> GetAnswersByQuestionAsync(Guid questionId);
        Task<bool> AddQuestionAsync(Question question, string token);
        Task<bool> AddAnswerAsync(Answer answer, string token);
        Task<bool> UpdateAnswerAsync(Answer answer);
        Task<bool> UpdateQuestionAsync(Question question);
        Task<User> GetUserDataAsync(string username);
        Task<Guid> GetUserIdAsync(string username);
        Task<string> Authenticate(string username, string password);

        Task<bool> UnAuthenticate(string inputUsername);
        Task<bool> UpdateUserAsync(User user, string token);
        Task<bool> AddUserAsync(User user);

        Task<bool> LikeAnswerAsync(Guid answerId, string username,string token);
    }
}