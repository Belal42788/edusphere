namespace Backend.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public List<Course> Courses { get; set; }
    }
}
