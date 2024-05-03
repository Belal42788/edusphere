namespace Backend.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set;}
        public int CourseID { get; set;}
        public Course Course { get; set;}
        public string Topic { get; set;}
        public string  Video { get; set;}
    }
}
