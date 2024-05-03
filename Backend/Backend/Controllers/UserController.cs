using Backend.Dtos;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public  readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;
        public UserController(IAuthService authService, UserManager<User> userManager) { 
            _authService = authService;
            _userManager = userManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegesterAsync([FromForm] RegisterModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.RegisterAsync(model);
            if(!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] TokenRequestModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.GetTokenAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);
        }
        [Authorize]
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePassword model )
        {
            var userID = HttpContext.User.FindFirst("uid").Value;
            var user = await _userManager.FindByIdAsync(userID);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.ChangePasswordAsync(model,user);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);
        }
        [Authorize]
        [HttpPut("ChangeImage")]
        public async Task<IActionResult> ChangeImageAsync([FromForm] ChangeImage model)
        {
            var userID = HttpContext.User.FindFirst("uid").Value;
            var user = await _userManager.FindByIdAsync(userID);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.ChangeImageAsync(model, user);
           
            return Ok(result);
        }






    }
}
