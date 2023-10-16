using System;

namespace Library.Hobbits.Models
{
    public class Submission
    {
        private static int lastId = 0;
        public int Id
        {
            get; private set;
        }

        public int StudentId { get; set; }
        public int AssignmentId { get; set; }
        public string Content { get; set; }

        public Submission()
        {
            Id = ++lastId;
            Content = string.Empty;
        }
    }
}