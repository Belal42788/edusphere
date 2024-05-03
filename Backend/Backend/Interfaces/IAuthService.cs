using Backend.Dtos;
using Backend.Models;

namespace Backend.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<AuthModel> ChangePasswordAsync(ChangePassword model,User user);
        Task<AuthModel> ChangeImageAsync(ChangeImage model, User user);
    }
}
