using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Validators.Interfaces;

namespace Sat.Recruitment.Api.Validators
{
    public class AddressValidator : IUserValidator
    {
        public string Validate(UserDTO newUser)
            => string.IsNullOrWhiteSpace(newUser.Address) ? "The address is required"
                                                     : string.Empty;          
    }
}
