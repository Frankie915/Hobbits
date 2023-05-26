using Library.Hobbits.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Hobbits.Services
{
    public class StudentService
    {
        public List<Person> studentList = new List<Person>();

        public void Add(Person student) 
        { 
            studentList.Add(student);
        }

        public List<Person> Students
        {
            get
            {
                return studentList;
            }
        }
    }
}
