using Library.Hobbits.Models;

namespace Library.Hobbits.Services
{
    public class CourseService
    {
        public List<Course> courseList = new List<Course>();

        public void Add(Course course)
        {
            courseList.Add(course);
        }

        public List<Course> Courses
        {
            get
            {
                return courseList;
            }
        }
    }
}