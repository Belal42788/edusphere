namespace Backend.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string TaskDescription { get; set; }
        public DateTime Dateon { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }

    }
}
