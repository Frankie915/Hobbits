using Library.Hobbits.Models;
using Library.Hobbits.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Hobbits.Helpers
{
	public class CourseHelper
	{
		private CourseService courseService;
        private StudentService studentService;

        public CourseHelper() { 
            studentService = StudentService.Current;
            courseService = CourseService.Current;
        }

        public void CreateCourseRecord(Course? selectedCourse = null)
        {
            Console.WriteLine("What is the code of the course?");
            var code = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("What is the name of the course?");
            var name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("What is the description of the course?");
            var description = Console.ReadLine() ?? string.Empty;
            PersonClassification classEnum = PersonClassification.Freshmen;

            Console.WriteLine("Which students should be enrolled in this course? ('Q' to quit)");
            var roster = new List<Student>();
            bool continueAdding = true;

            // Add students
            while (continueAdding)
            {
                Console.WriteLine("Top of while loop");
                // Prints list
                studentService.Students.Where(s => !roster.Any(s2 => s2.Id == s.Id)).ToList().ForEach(Console.WriteLine);
                
                // Initalizes variable
                var selection = Console.ReadLine() ?? string.Empty;

                /*
                if (studentService.Students.Any(s => !roster.Any(s2 => s2.Id == s.Id)))
                {
                    Console.WriteLine("Block 1");
                    selection = Console.ReadLine() ?? string.Empty;
                }
                else 
                */
                if (selection.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("Block 2");
                    continueAdding = false;
                }
                else
                {
                    var selectedId = int.Parse(selection);
                    var selectedStudent = studentService.Students.FirstOrDefault(s => s.Id == selectedId);
                    Console.WriteLine("About to add student...");
                    if (selectedStudent != null)
                    {
                        Console.WriteLine("STUDENT ADDED");
                        roster.Add(selectedStudent);
                    }
                }
                Console.WriteLine("After if-statement");
            }

            Console.WriteLine("Would u like to add assignments? (Y/N)");
            var assignResponse = Console.ReadLine() ?? "N";
            var assignments = new List<Assignment>();

            // Add assignments
            if (assignResponse.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                continueAdding = true;
                while (continueAdding)
                {
                    // Name 
                    Console.WriteLine("Name: ");
                    var assignmentName = Console.ReadLine() ?? String.Empty;

                    // Description
                    Console.WriteLine("Description ");
                    var assignmentDescription = Console.ReadLine() ?? String.Empty;

                    // Total points
                    Console.WriteLine("Total points: ");
                    var totalPoints = decimal.Parse(Console.ReadLine() ?? "100");
                    
                    // Due Date
                    Console.WriteLine("Due date: ");
                    var dueDate = DateTime.Parse(Console.ReadLine() ?? "01/01/1900");

                    assignments.Add(new Assignment
                    {
                        Name = assignmentName,
                        Description = assignmentDescription,
                        TotalAvailablePoints = totalPoints,
                        DueDate = dueDate
                    });

                    Console.WriteLine("Add more assignments? (Y/N)");
                    assignResponse = Console.ReadLine() ?? "N";
                    if (assignResponse.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        continueAdding = false;
                    }
                }
            }

            bool isNewCourse = false;
            if (selectedCourse == null)
            {
                isNewCourse = true;
                selectedCourse = new Course();
            }

            selectedCourse.Code = code;
            selectedCourse.Name = name;
            selectedCourse.Description = description;
            selectedCourse.Roster = new List<Person>();
            selectedCourse.Roster.AddRange(roster);
            selectedCourse.Assignments = new List<Assignment>();
            selectedCourse.Assignments.AddRange(assignments);

            if (isNewCourse)
            {
                courseService.Add(selectedCourse);
            }
        }

        public void UpdateCourseRecord()
        {
            Console.WriteLine("Enter the code for the course to update:");
            SearchCourses();

            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));

            if (selectedCourse != null)
            {
                CreateCourseRecord(selectedCourse);
            }

        }

        /*
        public void ListCourses()
        {
            courseService.Courses.ForEach(Console.WriteLine);
        }
        */

        public void SearchCourses(string query = null)
        {
            if (string.IsNullOrEmpty(query))
            {
                courseService.Courses.ForEach(Console.WriteLine);
            }
            else
            {
                courseService.Search(query).ToList().ForEach(Console.WriteLine);
            }

            Console.WriteLine("Select a course: ");
            var code = Console.ReadLine() ?? string.Empty;

            var selectedCourse = courseService
                .Courses
                .FirstOrDefault(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
            if(selectedCourse != null)
            {
                Console.WriteLine(selectedCourse.DetailDisplay);
            }
        }
    }
}
