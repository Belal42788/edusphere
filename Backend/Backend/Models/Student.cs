using System.Runtime.InteropServices;

namespace Backend.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
 