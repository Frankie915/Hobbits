using App.Hobbits.Helpers;
using Library.Hobbits.Models;
using Library.Hobbits.Services;
using System.Reflection;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var studentSrvc = new StudentService();
            var studentHelper = new StudentHelper();
            var courseHelper = new CourseHelper();
            bool cont = true;

            while (cont)
            {
                Console.WriteLine("1. Maintain People");
                Console.WriteLine("2. Maintain Courses");
                Console.WriteLine("3. Exit");                           //Sys
                var input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                {
                    if (result == 1)
                    {
                        ShowStudentMenu(studentHelper);
                    }
                    else if (result == 2)
                    {
                        ShowCourseMenu(courseHelper);
                    }
                    else if (result == 3)
                    {
                        cont = false;
                    }
                    int.TryParse(input, out result);

                }
            }
        }

        static void ShowStudentMenu(StudentHelper studentHelper)
        {
            Console.WriteLine("1. Add a new person");
            Console.WriteLine("2. Update a person");
            Console.WriteLine("3. List all people");
            Console.WriteLine("4. Search for person");

            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                if (result == 1)
                {
                    studentHelper.CreateStudentRecord();
                }
                else if (result == 2)
                {
                    studentHelper.UpdateStudentRecord();
                }
                else if (result == 3)
                {
                    studentHelper.ListStudents();
                }
                else if (result == 4)
                {
                    studentHelper.SearchStudents();
                }
            }
        }

        static void ShowCourseMenu(CourseHelper courseHelper)
        {
            Console.WriteLine("1. Add a new course");               
            Console.WriteLine("2. Update a course");                
            Console.WriteLine("3. Add a student to a course");
            Console.WriteLine("4. Remove a student from a course");
            Console.WriteLine("5. Add an assignment");
            Console.WriteLine("6. Update assignment");
            Console.WriteLine("7. Remove an assignment");
            Console.WriteLine("8. Add a modules to a course");
            Console.WriteLine("9. List all courses");               
            Console.WriteLine("10. Search for a course");           

            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                if (result == 1)
                {
                    courseHelper.CreateCourseRecord();
                }
                else if (result == 2)
                {
                    courseHelper.UpdateCourseRecord();
                }
                else if (result == 3)
                {
                    courseHelper.AddStudent();
                }
                else if (result == 4)
                {
                    courseHelper.RemoveStudent();
                }
                else if (result == 5)
                {
                    courseHelper.AddAssignment();
                }
                else if (result == 6)
                {
                    courseHelper.UpdateAssignment();
                }
                else if (result == 7)
                {
                    courseHelper.RemoveAssignment();
                }
                else if (result == 8)
                {
                    courseHelper.AddModule();
                }
                else if (result == 9)
                {
                    courseHelper.SearchCourses();
                }
                else if (result == 10)
                {
                    Console.WriteLine("Enter query: ");
                    var query = Console.ReadLine() ?? string.Empty;
                    courseHelper.SearchCourses();
                }
            }
        }
    }
}