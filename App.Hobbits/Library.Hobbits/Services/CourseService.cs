using Library.Hobbits.Models;

namespace Library.Hobbits.Services
{
    public class CourseService
    {
        private List<Course> courseList = new List<Course>();
        private static CourseService? _instance;

        public static CourseService Current 
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new CourseService();
                }

                return _instance;
            }
        }

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

        public IEnumerable<Course> Search(string query)
        {
            return Courses.Where(s => s.Name.ToUpper().Contains(query.ToUpper())
                || s.Description.ToUpper().Contains(query.ToUpper())
                || s.Code.ToUpper().Contains(query.ToUpper()));
        }
    }

}