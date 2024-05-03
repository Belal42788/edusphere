using Backend.Dtos;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ApplictionDbContext _cotext;
        private readonly UserManager<User> _userManager;
        private readonly ICourseService _courseService;

        public CourseController(ApplictionDbContext cotext,  UserManager<User> userManager, ICourseService courseService)
        {
            _cotext = cotext;
            _userManager = userManager;
            _courseService = courseService;
        }
        [Authorize]
        [HttpPost("CreateCourse")]
        public async Task<IActionResult> CreateCourseAsync([FromForm] CreateCourse model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var userID = HttpContext.User.FindFirst("uid").Value;
            var result = await _courseService.CreateCourseAsync(model, userID);
            return Ok(result);
        }




        [Authorize]
        [HttpGet("FindCourse")]
        public async Task<IActionResult> FindCourseAsync([FromBody] string Name)
        {
            var result = await _courseService.FindCourseAsync(Name);
            return Ok(result);

        }

    }
}
