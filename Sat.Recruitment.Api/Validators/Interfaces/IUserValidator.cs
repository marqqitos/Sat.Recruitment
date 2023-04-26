using Sat.Recruitment.Api.DTOs;

namespace Sat.Recruitment.Api.Validators.Interfaces
{
    public interface IUserValidator
    {
        public string Validate(UserDTO newUser);
    }
}
