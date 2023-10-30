﻿using Library.Hobbits.Models;
using Library.Hobbits.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                Console.WriteLine("What is the prefix of the course?");
                selectedCourse.Prefix = Console.ReadLine().ToUpper() ?? string.Empty;
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
                SetupModules(selectedCourse);
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

        public void SearchCourses(string? query = null)
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

        public void AddStudent()
        {
            Console.WriteLine("Enter the code for the course to add the student to:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                studentService.Students.Where(s => !selectedCourse.Roster.Any(s2 => s2.Id == s.Id)).ToList().ForEach(Console.WriteLine);
                if (studentService.Students.Any(s => !selectedCourse.Roster.Any(s2 => s2.Id == s.Id)))
                {
                    selection = Console.ReadLine() ?? string.Empty;
                }

                if (selection != null)
                {
                    var selectedId = int.Parse(selection);
                    var selectedStudent = studentService.Students.FirstOrDefault(s => s.Id == selectedId);
                    if (selectedStudent != null)
                    {
                        selectedCourse.Roster.Add(selectedStudent);
                    }
                }
            }
        }

        public void RemoveStudent()
        {
            Console.WriteLine("Enter the code for the course to remove the student from:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                selectedCourse.Roster.ForEach(Console.WriteLine);
                if (selectedCourse.Roster.Any())
                {
                    selection = Console.ReadLine() ?? string.Empty;
                } else
                {
                    selection = null;
                }

                if (selection != null)
                {
                    var selectedId = int.Parse(selection);
                    var selectedStudent = studentService.Students.FirstOrDefault(s => s.Id == selectedId);
                    if (selectedStudent != null)
                    {
                        selectedCourse.Roster.Remove(selectedStudent);
                    }
                }
            }

        }

        public void AddAssignment()
        {
            Console.WriteLine("Enter the code for the course to add the assignment to:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                CreateAssignmentWithGroup(selectedCourse);
            }
        }

        public void RemoveAssignment()
        {
            Console.WriteLine("Enter the code for the course:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                Console.WriteLine("Choose an assignment to remove:");
                selectedCourse.Assignments.ToList().ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);

                var selectedGroup = selectedCourse.AssignmentGroups.FirstOrDefault(ag => ag.Assignments.Any(a => a.Id == selectionInt));
                if (selectedGroup != null)
                {
                    var selectedAssignment = selectedGroup.Assignments.FirstOrDefault(a => a.Id == selectionInt);
                    if (selectedAssignment != null)
                    {
                        var index = selectedGroup.Assignments.Remove(selectedAssignment);
                    }
                }
            }
        }

        public void UpdateAssignment()
        {
            Console.WriteLine("Enter the code for the course:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                Console.WriteLine("Choose an assignment to update:");
                selectedCourse.Assignments.ToList().ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedGroup = selectedCourse.AssignmentGroups
                                .FirstOrDefault(ag => ag.Assignments.Any(a => a.Id == selectionInt));
                if (selectedGroup != null)
                {
                    var selectedAssignment = selectedGroup.Assignments.FirstOrDefault(a => a.Id == selectionInt);
                    if (selectedAssignment != null)
                    {
                        var index = selectedGroup.Assignments.IndexOf(selectedAssignment);
                        selectedGroup.Assignments.RemoveAt(index);
                        selectedGroup.Assignments.Insert(index, CreateAssignment());
                    }
                }
            }
        }
 
        public void AddModule()
        {
            Console.WriteLine("Enter the code for the course to add the module to:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                selectedCourse.Modules.Add(CreateModule(selectedCourse));
            }
        }

        public void RemoveModule()
        {
            Console.WriteLine("Enter the code for the course:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));

            if (selectedCourse != null)
            {
                Console.WriteLine("Choose an module to remove:");
                selectedCourse.Modules.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedModule = selectedCourse.Modules.FirstOrDefault(m => m.Id == selectionInt);
                if (selectedModule != null)
                {
                    selectedCourse.Modules.Remove(selectedModule);
                }
            }
        }

        public void UpdateModule()
        {
            Console.WriteLine("Enter the code for the course:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            
            if(selectedCourse != null && selectedCourse.Modules.Any())
            {
                Console.WriteLine("Enter the id for the module to update:");
                selectedCourse.Modules.ForEach(Console.WriteLine);

                selection = Console.ReadLine();
                var selectedModule = selectedCourse
                    .Modules
                    .FirstOrDefault(m => m.Id.ToString().Equals(selection, StringComparison.InvariantCultureIgnoreCase));

                if (selectedModule != null)
                {
                    Console.WriteLine("Would you like to modify the module name? (Y/N)");
                    selection = Console.ReadLine();
                    if (selection?.Equals("Y", StringComparison.InvariantCultureIgnoreCase) ?? false)
                    {
                        Console.WriteLine("Name:");
                        selectedModule.Name = Console.ReadLine();
                    }

                    Console.WriteLine("Would you like to modify the module description? (Y/N)");
                    selection = Console.ReadLine();
                    if (selection?.Equals("Y", StringComparison.InvariantCultureIgnoreCase) ?? false)
                    {
                        Console.WriteLine("Description:");
                        selectedModule.Description = Console.ReadLine();
                    }

                    Console.WriteLine("Would you like to remove content from this module? (Y/N)");
                    selection = Console.ReadLine();
                    if (selection?.Equals("Y", StringComparison.InvariantCultureIgnoreCase) ?? false)
                    {
                        var keepRemoving = true;

                        while (keepRemoving)
                        {
                            selectedModule.Content.ForEach(Console.WriteLine);
                            selection = Console.ReadLine();

                            var contentToRemove = selectedModule
                                .Content
                                .FirstOrDefault(c => c.Id.ToString().Equals(selection, StringComparison.InvariantCultureIgnoreCase));

                            if (contentToRemove != null)
                            {
                                selectedModule.Content.Remove(contentToRemove);
                            }

                            Console.WriteLine("Would you like to remove more content? (Y/N)");
                            selection = Console.ReadLine();
                            if (selection?.Equals("N", StringComparison.InvariantCultureIgnoreCase) ?? false)
                            {
                                keepRemoving = false;
                            }
                        }
                    }

                    Console.WriteLine("Would you like to add content? (Y/N)");
                    var choice = Console.ReadLine() ?? "N";
                    while (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine("What type of content would you like to add?");
                        Console.WriteLine("1. Assignment");
                        Console.WriteLine("2. File");
                        Console.WriteLine("3. Page");
                        var contentChoice = int.Parse(Console.ReadLine() ?? "0");

                        switch (contentChoice)
                        {
                            case 1:
                                var newAssignmentContent = CreateAssignmentItem(selectedCourse);
                                if (newAssignmentContent != null)
                                {
                                    selectedModule.Content.Add(newAssignmentContent);
                                }
                                break;

                            case 2:
                                var newFileContent = CreateFileItem(selectedCourse);
                                if (newFileContent != null)
                                {
                                    selectedModule.Content.Add(newFileContent);
                                }
                                break;

                            case 3:
                                var newPageContent = CreatePageItem(selectedCourse);
                                if (newPageContent != null)
                                {
                                    selectedModule.Content.Add(newPageContent);
                                }
                                break;

                            default:
                                break;
                        }

                        Console.WriteLine("Would you like to add more content?");
                        choice = Console.ReadLine() ?? "N";
                    }
                }
            }
        }

        public void AddAnnouncement()
        {
            Console.WriteLine("Enter the code for the course to add the announcement to:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                selectedCourse.Announcements.Add(CreateAnnouncement(selectedCourse));
            }
        }

        public void RemoveAnnouncement()
        {
            Console.WriteLine("Enter the code for the course:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                Console.WriteLine("Choose an announcement to remove:");
                selectedCourse.Announcements.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedAnnouncement = selectedCourse.Announcements.FirstOrDefault(a => a.Id == selectionInt);
                if (selectedAnnouncement != null)
                {
                    selectedCourse.Announcements.Remove(selectedAnnouncement);
                }
            }
        }

        public void UpdateAnnouncement()
        {
            Console.WriteLine("Enter the code for the course:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                Console.WriteLine("Choose an announcement to update:");
                selectedCourse.Announcements.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedAnnouncement = selectedCourse.Announcements.FirstOrDefault(a => a.Id == selectionInt);
                if (selectedAnnouncement != null)
                {
                    Console.WriteLine("Name:");
                    selectedAnnouncement.Name = Console.ReadLine();

                    Console.WriteLine("Description:");
                    selectedAnnouncement.Description = Console.ReadLine();
                }
            }
        }

        public void AddSubmission()
        {
            Console.WriteLine("Enter the code for the course to add the submission to:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                Console.WriteLine("Enter the id for the student");
                selectedCourse.Roster.Where(r => r is Student).ToList().ForEach(Console.WriteLine);
                var selectedStudentId = int.Parse(Console.ReadLine() ?? "0");
                var selectedStudent = selectedCourse.Roster.FirstOrDefault(s => s.Id == selectedStudentId);

                Console.WriteLine("Enter the id for the assignment");
                selectedCourse.Assignments.ToList().ForEach(Console.WriteLine);
                var selectedAssignmentId = int.Parse(Console.ReadLine() ?? "0");
                var selectedAssignment = selectedCourse.Assignments.FirstOrDefault(a => a.Id == selectedAssignmentId);

                CreateSubmission(selectedCourse, selectedStudent as Student, selectedAssignment);

            }
        }

        public void ListSubmissions()
        {
            Console.WriteLine("Enter the code for the course to add the submission to:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                selectedCourse.Submissions.ForEach(Console.WriteLine);
            }
        }

        public void RemoveSubmission()
        {
            Console.WriteLine("Enter the code for the course to add the submission to:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                selectedCourse.Submissions.ForEach(Console.WriteLine);
                var selectedId = int.Parse(Console.ReadLine() ?? "0");

                var selectedSubmission = selectedCourse.Submissions.FirstOrDefault(s => s.Id == selectedId);
                if (selectedSubmission != null)
                {
                    selectedCourse.Submissions.Remove(selectedSubmission);
                }
            }
        }

        public void UpdateSubmission()
        {
            Console.WriteLine("Enter the code for the course to add the submission to:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                selectedCourse.Submissions.ForEach(Console.WriteLine);
                var selectedId = int.Parse(Console.ReadLine() ?? "0");

                Console.WriteLine("Enter new content:");
                selectedCourse.Submissions.FirstOrDefault(s => s.Id == selectedId).Content = Console.ReadLine() ?? string.Empty;
            }
        }

        public void GradeSubmission()
        {
            Console.WriteLine("Enter the code for the course to add the submission to:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                selectedCourse.Submissions.ForEach(Console.WriteLine);
                var selectedId = int.Parse(Console.ReadLine() ?? "0");

                Console.WriteLine("Enter grade:");
                selectedCourse.Submissions.FirstOrDefault(s => s.Id == selectedId).Grade = decimal.Parse(Console.ReadLine() ?? "0");
            }
        }

        public void GetStudentGrade()
        {
            Console.WriteLine("Enter the code for the course to add the assignment to:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                Console.WriteLine("Enter the id for the student:");
                selectedCourse.Roster.Where(r => r is Student).ToList().ForEach(Console.WriteLine);
                var selectedStudentId = int.Parse(Console.ReadLine() ?? "0");

                var weightedAverage = courseService.GetWeightedGrade(selectedCourse.Id, selectedStudentId); ;

                Console.WriteLine($"Student Grade: ({courseService.GetLetterGrade(weightedAverage)}) {weightedAverage}");

            }
            
        }

        public string GetLetterGrade(decimal grade)
        {
            if (grade >= 93)
            {
                return "A";
            }
            else if (grade < 93 && grade >= 90)
            {
                return "A-";
            }
            else if (grade < 90 && grade >= 87)
            {
                return "B+";
            }
            else if (grade < 87 && grade >= 83)
            {
                return "B";
            }
            else if (grade < 83 && grade >= 80)
            {
                return "B-";
            }
            else if (grade < 80 && grade >= 77)
            {
                return "C+";
            }
            else if (grade < 77 && grade >= 73)
            {
                return "C";
            }
            else if (grade < 73 && grade >= 70)
            {
                return "C-";
            }
            else if (grade < 70 && grade >= 60)
            {
                return "D";
            }
            else
            {
                return "F";
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
                var selection = "Q";
                if (studentService.Students.Any(s => !c.Roster.Any(s2 => s2.Id == s.Id)))
                {
                    selection = Console.ReadLine() ?? string.Empty;
                }

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
            }
        }

        private void SetupAssignments(Course c)
        {
            Console.WriteLine("Would you like to add assignments? (Y/N)");
            var assignResponse = Console.ReadLine() ?? "N";
            bool continueAdding;
            if (assignResponse.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                continueAdding = true;
                while (continueAdding)
                {
                    CreateAssignmentWithGroup(c);
                    Console.WriteLine("Add more assignments? (Y/N)");
                    assignResponse = Console.ReadLine() ?? "N";
                    if (assignResponse.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        continueAdding = false;
                    }
                }
            }
        }

        private void SetupModules(Course c)
        {
            Console.WriteLine("Would u like to add modules? (Y/N)");
            var assignResponse = Console.ReadLine() ?? "N";
            bool continueAdding;
            if (assignResponse.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                continueAdding = true;
                while (continueAdding)
                {
                    c.Modules.Add(CreateModule(c));
                    Console.WriteLine("Add more modules? (Y/N)");
                    assignResponse = Console.ReadLine() ?? "N";
                    if (assignResponse.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        continueAdding = false;
                    }
                }
            }
        }

        private void CreateAssignmentWithGroup(Course selectedCourse)
        {
            if (selectedCourse.AssignmentGroups.Any())
            {
                Console.WriteLine("[0] Add a new group");
                selectedCourse.AssignmentGroups.ForEach(Console.WriteLine);

                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);

                if (selectionInt == 0)
                {
                    var newGroup = new AssignmentGroup();
                    Console.WriteLine("Group Name:");
                    newGroup.Name = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("Group Weight:");
                    newGroup.Weight = decimal.Parse(Console.ReadLine() ?? "1");

                    newGroup.Assignments.Add(CreateAssignment());
                    selectedCourse.AssignmentGroups.Add(newGroup);
                }
                else if (selectionInt != 0)
                {
                    var selectedGroup = selectedCourse.AssignmentGroups.FirstOrDefault(g => g.Id == selectionInt);
                    if (selectedGroup != null)
                    {
                        selectedGroup.Assignments.Add(CreateAssignment());
                    }
                }
            }
            else
            {
                var newGroup = new AssignmentGroup();
                Console.WriteLine("Group Name:");
                newGroup.Name = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("Group Weight:");
                newGroup.Weight = decimal.Parse(Console.ReadLine() ?? "1");

                newGroup.Assignments.Add(CreateAssignment());
                selectedCourse.AssignmentGroups.Add(newGroup);
            }
        }

        private Announcement CreateAnnouncement(Course c)
        {
            Console.WriteLine("Enter the name of the announcement:");
            var name = Console.ReadLine();

            Console.WriteLine("Enter the description of the announcement:");
            var description = Console.ReadLine();

            return new Announcement 
            { 
                Name = name,    
                Description = description
            };

        }

        private Module CreateModule(Course c)
        {
            // Name 
            Console.WriteLine("Name:");
            var name = Console.ReadLine() ?? String.Empty;

            // Description
            Console.WriteLine("Description:");
            var description = Console.ReadLine() ?? String.Empty;

            var module = new Module
            {
                Name = name,
                Description = description
            };
            Console.WriteLine("Would you like to add content? (Y/N)");
            var choice = Console.ReadLine() ?? "N";
            while (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("What type of content would you like to add?");
                Console.WriteLine("1. Assignment");
                Console.WriteLine("2. File");
                Console.WriteLine("3. Page");
                var contentChoice = int.Parse(Console.ReadLine() ?? "0");

                switch (contentChoice)
                {
                    case 1:
                        var newAssignmentContent = CreateAssignmentItem(c);
                        if (newAssignmentContent != null)
                        {
                            module.Content.Add(newAssignmentContent);
                        }
                        break;

                    case 2:
                        var newFileContent = CreateFileItem(c);
                        if (newFileContent != null)
                        {
                            module.Content.Add(newFileContent);
                        }
                        break;

                    case 3:
                        var newPageContent = CreatePageItem(c);
                        if (newPageContent != null)
                        {
                            module.Content.Add(newPageContent);
                        }
                        break;

                    default:
                        break;
                }

                Console.WriteLine("Would you like to add more content?");
                choice = Console.ReadLine() ?? "N";
            }

            return module;
        }

        private AssignmentItem? CreateAssignmentItem(Course c) 
        {

            // Name 
            Console.WriteLine("Name:");
            var name = Console.ReadLine() ?? String.Empty;

            // Description
            Console.WriteLine("Description:");
            var description = Console.ReadLine() ?? String.Empty;

            Console.WriteLine("Which assignment should be added?");
            c.Assignments.ToList().ForEach(Console.WriteLine);
            var choice = int.Parse(Console.ReadLine() ?? "-1");
            if(choice >= 0)
            {
                var assignment = c.Assignments.FirstOrDefault(a => a.Id == choice);
                return new AssignmentItem
                {
                    Assignment = assignment,
                    Name = name,
                    Description = description
                };
            }
            return null;
        }

        private FileItem? CreateFileItem(Course c)
        {

            // Name 
            Console.WriteLine("Name:");
            var name = Console.ReadLine() ?? String.Empty;

            // Description
            Console.WriteLine("Description:");
            var description = Console.ReadLine() ?? String.Empty;

            Console.WriteLine("Enter a path to the file:");
            var filepath = Console.ReadLine();

            return new FileItem { 
                Name = name,
                Description = description,
                Path = filepath 
            };
        }

        private PageItem? CreatePageItem(Course c)
        {

            // Name 
            Console.WriteLine("Name:");
            var name = Console.ReadLine() ?? String.Empty;

            // Description
            Console.WriteLine("Description:");
            var description = Console.ReadLine() ?? String.Empty;

            Console.WriteLine("Enter page item:");
            var body = Console.ReadLine();

            return new PageItem
            {
                Name = name,
                Description = description,
                HtmlBody = body
            };
        }

        private Assignment CreateAssignment()
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

            return new Assignment
            {
                Name = assignmentName,
                Description = assignmentDescription,
                TotalAvailablePoints = totalPoints, 
                DueDate = dueDate
            };
        }

        public void CreateSubmission(Course c, Student student, Assignment assignment)
        {
            if (student == null || assignment == null) 
            {
                return;
            }

            Console.WriteLine("What is the content of the submission?");
            var content = Console.ReadLine();
            c.Submissions.Add(
                new Submission
                {
                    student = student,
                    assignment = assignment,
                    Content = content ?? string.Empty
                }
            );
        }
    }
}
