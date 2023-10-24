using System;
using System.Linq.Expressions;

namespace Library.Hobbits.Models
{
    public class Submission
    {
        private static int lastId = 0;
        public int Id
        {
            get; private set;
        }

        public Student student { get; set; }
        public Assignment assignment { get; set; }
        public string Content { get; set; }

        public Submission()
        {
            Id = ++lastId;
            Content = string.Empty;
        }

        public override string ToString()
        {
            return $"[{Id}] {student.Name}: {assignment}";
        }
    }
}