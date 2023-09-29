﻿using Library.Hobbits.Models;
using Library.Hobbits.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Hobbits.Helpers
{   
    internal class StudentHelper
    {
        private StudentService studentService;
        private CourseService courseService;

        public StudentHelper()
        {
            studentService = StudentService.Current;
            courseService = CourseService.Current;
        }

        public void CreateStudentRecord(Person? selectedStudent = null)
        {
            bool isCreate = false;
            if (selectedStudent == null)
            {
                isCreate = true;
                Console.WriteLine("What type of person would you like to add?");
                Console.WriteLine("(S)tudent");
                Console.WriteLine("(T)eaching Assistant");
                Console.WriteLine("(I)nstructor");
                var choice = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrEmpty(choice))
                {
                    return;
                }
                if (choice.Equals("S", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedStudent = new Student();
                }
                else if (choice.Equals("T", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedStudent = new TeachingAssistant();
                }
                else if (choice.Equals("I", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedStudent = new Instructor();
                }
            }


            Console.WriteLine("What is the id of the student?");
            var id = Console.ReadLine();
            Console.WriteLine("What is the name of the student?");
            var name = Console.ReadLine();
            if (selectedStudent is Student)
            {
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

                var studentRecord = selectedStudent as Student;
                if (studentRecord != null)
                {
                    studentRecord.Classification = classEnum;
                    studentRecord.Id = int.Parse(id ?? "0");
                    studentRecord.Name = name ?? string.Empty;

                    if (isCreate)
                    {
                        studentService.Add(selectedStudent);
                    }
                }
            } 
            else {
                if (selectedStudent != null)
                {
                    selectedStudent.Id = int.Parse(id ?? "0");
                    selectedStudent.Name = name ?? string.Empty;
                    if (isCreate)
                    {
                        studentService.Add(selectedStudent);
                    }
                }
            } 
        }

        public void UpdateStudentRecord()
        {
            Console.WriteLine("Select a student to update: ");
            studentService.Students.ForEach(Console.WriteLine);

            var selectionStr = Console.ReadLine();

            if (int.TryParse(selectionStr, out int selectionInt))
            {
                var selectedStudent = studentService.Students.FirstOrDefault(s => s.Id == selectionInt);
                if(selectedStudent != null) 
                {
                    CreateStudentRecord(selectedStudent);
                }
            }
        }

        public void ListStudents()
        {
            studentService.Students.ForEach(Console.WriteLine);

            Console.WriteLine("Select a student:");
            var selectionStr = Console.ReadLine();
            var selectionInt = int.Parse(selectionStr ?? "0");

            Console.WriteLine("Student Course List:");
            courseService.Courses.Where(c => c.Roster.Any(s => s.Id == selectionInt)).ToList().ForEach(Console.WriteLine);

        }

        public void SearchStudents() 
        {
            Console.WriteLine("Enter query: ");
            var query = Console.ReadLine() ?? string.Empty;

            studentService.Search(query).ToList().ForEach(Console.WriteLine);
            var selectionStr = Console.ReadLine();
            var selectionInt = int.Parse(selectionStr ?? "0");

            Console.WriteLine("Student Course List:");
            courseService.Courses.Where(c => c.Roster.Any(s => s.Id == selectionInt)).ToList().ForEach(Console.WriteLine);
        }

    }
}
