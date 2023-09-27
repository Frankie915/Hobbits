﻿using Library.Hobbits.Models;
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

            bool isNewCourse = false;
            if (selectedCourse == null)
            {
                isNewCourse = true;
                selectedCourse = new Course();
            }

            var choice = "Y";
            if (!isNewCourse)
            {
                Console.WriteLine("Do you want to update the course code?");
                choice = Console.ReadLine() ?? "N";
            }
            else
            {
                choice = "Y";
            }

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("What is the code of the course?");
                selectedCourse.Code = Console.ReadLine() ?? string.Empty;
            }
            if (!isNewCourse) 
            {
                Console.WriteLine("Do you want to update the course name?");
                choice = Console.ReadLine() ?? "N";
            }
            else
            {
                choice = "Y";
            }
            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("What is the name of the course?");
                selectedCourse.Name = Console.ReadLine() ?? string.Empty;
            }

            if (!isNewCourse)
            {
                Console.WriteLine("Do you want to update the course description?");
                choice = Console.ReadLine() ?? "N";
            }
            else
            {
                choice = "Y";
            }
            
            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("What is the description of the course?");
                selectedCourse.Description = Console.ReadLine() ?? string.Empty;
            }
         
            if (isNewCourse)
            { 
                SetupRoster(selectedCourse);
                SetupAssignments(selectedCourse);
            }                      
           
            if (isNewCourse)
            {
                courseService.Add(selectedCourse);
            }
        }

        public void UpdateCourseRecord()
        {
            Console.WriteLine("Enter the code for the course to update:");
            courseService.Courses.ForEach(Console.WriteLine);
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

        private void SetupRoster(Course c)
        {
            Console.WriteLine("Which students should be enrolled in this course? ('Q' to quit)");
            bool continueAdding = true;
            while (continueAdding)
            {
                // Prints list
                studentService.Students.Where(s => !c.Roster.Any(s2 => s2.Id == s.Id)).ToList().ForEach(Console.WriteLine);

                // Initalizes variable
                var selection = Console.ReadLine() ?? string.Empty;
                /*
                if (studentService.Students.Any(s => !c.Roster.Any(s2 => s2.Id == s.Id)))
                {
                    selection = Console.ReadLine() ?? string.Empty;
                }
                */

                if (selection.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    continueAdding = false;
                }
                else
                {
                    var selectedId = int.Parse(selection);
                    var selectedStudent = studentService.Students.FirstOrDefault(s => s.Id == selectedId);
                    if (selectedStudent != null)
                    {
                        c.Roster.Add(selectedStudent);
                    }
                }
                Console.WriteLine("(Q to quit)");
            }
        }

        private void SetupAssignments(Course c)
        {
            Console.WriteLine("Would u like to add assignments? (Y/N)");
            bool continueAdding = true;
            var assignResponse = Console.ReadLine() ?? "N";
            if (assignResponse.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                continueAdding = true;
                while (continueAdding)
                {
                    // Name 
                    Console.WriteLine("Name:");
                    var assignmentName = Console.ReadLine() ?? String.Empty;

                    // Description
                    Console.WriteLine("Description:");
                    var assignmentDescription = Console.ReadLine() ?? String.Empty;

                    // Total points
                    Console.WriteLine("Total points:");
                    var totalPoints = decimal.Parse(Console.ReadLine() ?? "100");

                    // Due Date
                    Console.WriteLine("Due date:");
                    var dueDate = DateTime.Parse(Console.ReadLine() ?? "01/01/1900");

                    c.Assignments.Add(new Assignment
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

        }
    }
}
