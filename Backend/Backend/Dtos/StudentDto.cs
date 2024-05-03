namespace Backend.Dtos
{
    public class StudentDto
    {
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        public string AcademicYear { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }

    }
}
