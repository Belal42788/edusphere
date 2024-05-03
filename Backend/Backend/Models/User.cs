

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Backend.Models
{
    public class User : IdentityUser
    {
        [MaxLength(100)]
        public string  FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public Student Student { get; set; }   
        public Teacher Teacher { get; set; }
       public  List<Task>  ? Tasks { get; set; }
    }
}
