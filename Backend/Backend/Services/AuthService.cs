using Backend.Dtos;
using Backend.Helprers;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly Jwt _jwt;
        private readonly IImageService _imageService;
        public AuthService(UserManager<User> userManager, IOptions<Jwt> jwt, IImageService imageService)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _imageService = imageService;
        }
        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
            {
                return new AuthModel { Message = "Email is already use" };
            }
            var user = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                UserName = model.Email,
                ImageUrl= _imageService.SetImage(model.Image)

            };
            var reasult = await _userManager.CreateAsync(user, model.Password);
            if (!reasult.Succeeded)
            {

                var errors = string.Empty;
                foreach (var error in reasult.Errors)
                {
                    errors += $"{error.Description},";
                }
                return new AuthModel { Message = errors };

            }
            await _userManager.AddToRoleAsync(user, "User");
            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthModel
            {
                Email = user.Email,
                Expire = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),

            };
        }
        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            var authModel = new AuthModel();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null|| !await _userManager.CheckPasswordAsync(user,model.Password)) {
                authModel.Message = "Email or password is incorrect";
                return authModel;
             }
            
            var jwtSecurityToken = await CreateJwtToken(user);
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Expire = jwtSecurityToken.ValidTo;
            var rolesList = await _userManager.GetRolesAsync(user);
            authModel.Roles = rolesList.ToList();
            authModel.FirstName = user.FirstName;
            authModel.LastName = user.LastName;
            authModel.Gender= user.Gender;
            authModel.Image = "https://localhost:7225" + user.ImageUrl;
            return authModel;
        }
        async Task<AuthModel> IAuthService.ChangePasswordAsync(ChangePassword model,User user)
        {
            var authModel = new AuthModel();
            
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.PasswordOld))
            {
                authModel.Message = "Email or password is incorrect";
                return authModel;
            }
           await _userManager.ChangePasswordAsync(user,model.PasswordOld,model.PasswordNew);
            var jwtSecurityToken = await CreateJwtToken(user);
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Expire = jwtSecurityToken.ValidTo;
            var rolesList = await _userManager.GetRolesAsync(user);
            authModel.Roles = rolesList.ToList();
            authModel.FirstName = user.FirstName;
            authModel.LastName = user.LastName;
            authModel.Gender = user.Gender;
            authModel.Image = "https://localhost:7225" + user.ImageUrl;
            authModel.Message = "ok,password is changed";
            return authModel;


        }
        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        public async Task<AuthModel>  ChangeImageAsync(ChangeImage model, User user)
        {
           string image= _imageService.UpdateImage(model.Image,user.ImageUrl);
            user.ImageUrl = image;
            await _userManager.UpdateAsync(user);
            var authModel = new AuthModel();
            authModel.Image= "https://localhost:7225" + user.ImageUrl;
            return authModel;
        }
    }
}