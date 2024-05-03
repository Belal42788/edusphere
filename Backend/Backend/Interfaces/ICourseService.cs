using Backend.Dtos;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface ICourseService
    {
        Task<CourseModel> CreateCourseAsync(CreateCourse Model,string userUid);
        Task<IEnumerable<CourseModel>> FindCourseAsync( string Name);
    }
}
