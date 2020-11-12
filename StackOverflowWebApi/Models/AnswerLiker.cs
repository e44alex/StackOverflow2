using System;

namespace StackOverflowWebApi.Models
{
    public class AnswerLiker
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Guid AnswerId { get; set; }

        public User User { get; set; }

        public Answer Answer { get; set; }
    }
}