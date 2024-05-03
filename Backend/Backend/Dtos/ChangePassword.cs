using Backend.Models;

namespace Backend.Dtos
{
    public class ChangePassword
    {
       
        public string PasswordOld { get; set; }
        public string PasswordNew { get; set; }
    }
}
