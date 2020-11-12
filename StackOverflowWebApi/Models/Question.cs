using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StackOverflowWebApi.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        [Required]
        public string Topic { get; set; }
        [Required]
        public string Body { get; set; }
        //if not opened -- not allowed to create answers
        public bool Opened { get; set; }
        public User Creator { get; set; }
        public DateTime DateCreated { get; set; }
        //if no q's date created
        //else max(a.dateCreated)
        public DateTime LastActivity { get; set; }
        public List<Answer> Answers { get; set; }

    }
}