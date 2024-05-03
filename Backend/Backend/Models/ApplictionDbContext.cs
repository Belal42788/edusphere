using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class ApplictionDbContext : IdentityDbContext
    {
        public ApplictionDbContext(DbContextOptions<ApplictionDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Student>Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }    
        public DbSet<Lesson>  Lessons { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
