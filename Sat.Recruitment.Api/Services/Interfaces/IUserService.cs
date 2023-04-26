using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Models;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services.Interfaces
{
    public interface IUserService
    {
        public NewUserValidationResult ValidateNewUser(UserDTO newUser);
        public Task<UserDTO> CreateUser(UserDTO newUser);
    }
}
