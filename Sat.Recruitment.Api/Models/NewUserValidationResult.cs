using System.Collections.Generic;

namespace Sat.Recruitment.Api.Models
{
    public class NewUserValidationResult
    {
        public bool IsValid { get; set; }
        public IList<string> ErrorMessages { get; set; }
    }
}
