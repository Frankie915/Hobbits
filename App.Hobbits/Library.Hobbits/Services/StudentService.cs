using Library.Hobbits.Models;


namespace Library.Hobbits.Services
{
    public class StudentService
    {
        private List<Student> studentList;

        private static StudentService? _instance;

        public StudentService() 
        { 
            studentList = new List<Student>();
        }

        public static StudentService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StudentService();
                }

                return _instance;
            }
        }
 
        public void Add(Student student) 
        { 
            studentList.Add(student);
        }

        public List<Student> Students
        {
            get
            {
                return studentList;
            }
        }

        public IEnumerable<Student> Search(string query) 
        {
            return studentList.Where(s => s.Name.ToUpper().Contains(query.ToUpper()));
        }
    }
}
