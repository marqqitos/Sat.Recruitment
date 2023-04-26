using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Validators.Interfaces;
using System.Text.RegularExpressions;

namespace Sat.Recruitment.Api.Validators
{
    public class EmailValidator : IUserValidator
    {
        public string Validate(UserDTO newUser)
        {
            if (string.IsNullOrWhiteSpace(newUser.Email))
                return "The email is required";

            return !ValidateEmail(newUser.Email) ? "The email is not formatted correctly"
                                                : string.Empty;
        }

        private bool ValidateEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; 
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
