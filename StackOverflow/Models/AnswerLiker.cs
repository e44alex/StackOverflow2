using System;
using System.Security.AccessControl;

namespace StackOverflow.Models
{
    public class AnswerLiker
    {
        public Guid Id { get; set; }

        public User User { get; set; }

        public Answer Answer { get; set; }
    }
}