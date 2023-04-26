using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Validators.Interfaces;

namespace Sat.Recruitment.Api.Validators
{
    public class PhoneValidator : IUserValidator
    {
        public string Validate(UserDTO newUser)
            => string.IsNullOrWhiteSpace(newUser.Phone) ? "The phone is required"
                                                   : string.Empty;
    }
}
