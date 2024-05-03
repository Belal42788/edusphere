using Backend.Models;

namespace Backend.Dtos
{
    public class CreateCourse
    {
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string Subject { get; set; }
        public string Cost { get; set; }
        
        public IFormFile Image { get; set; }
    }
}
