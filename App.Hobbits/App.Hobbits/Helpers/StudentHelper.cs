using Library.Hobbits.Models;
using Library.Hobbits.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Hobbits.Helpers
{
    
    public class StudentHelper
    {

        private StudentService studentService = new StudentService();
        public void CreateStudentRecord()
        {

            Console.WriteLine("What is the id of the student?");
            var id = Console.ReadLine();
            Console.WriteLine("What is the name of the student?");
            var name = Console.ReadLine();
            Console.WriteLine("What is the classification of the student? [(F)reshmen, S(O)phomore, (J)unior, (S)enior]");
            var classification = Console.ReadLine();
            PersonClassification classEnum = PersonClassification.Freshmen;

            if (classification.Equals("O", StringComparison.InvariantCultureIgnoreCase))
            {
                classEnum = PersonClassification.Sophomore;
            }
            else if (classification.Equals("J", StringComparison.InvariantCultureIgnoreCase))
            {
                classEnum = PersonClassification.Junior;
            }
            else if (classification.Equals("S", StringComparison.InvariantCultureIgnoreCase))
            {
                classEnum = PersonClassification.Senior;
            }

            var student = new Person
            {
                Id = int.Parse(id ?? "0"),
                Name = name ?? string.Empty,
                Classification = classEnum
            };

            studentService.Add(student);

        }

        public void ListStudents()
        {
            studentService.Students.ForEach(Console.WriteLine);
        }
    }
}
