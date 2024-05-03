namespace Backend.Models
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile Image { get; set; }
    }
}
