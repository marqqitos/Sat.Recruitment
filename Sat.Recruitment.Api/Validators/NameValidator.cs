using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Validators.Interfaces;

namespace Sat.Recruitment.Api.Validators
{
    public class NameValidator : IUserValidator
    {
        public string Validate(UserDTO newUser)
            => string.IsNullOrWhiteSpace(newUser.Name) ? "The name is required"
                                                  : string.Empty;     
    }
}
