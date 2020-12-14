using System;
using System.Collections.Generic;

namespace StackOverflowWebApi.Models
{
    public class User 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Login{ get; set; }
        public int? Rating { get; set; }
        public DateTime DateRegistered { get; set; }
        public string? Position { get; set; }
        public float? Exerience { get; set; }

        public List<Answer> Answers { get; set; }
        public List<AnswerLiker> LikedAnswers{ get; set; }
        public List<Question> Questions{ get; set; }
        public string? Bio { get; set; }
        public string PasswordHash { get; set; }
    }
}