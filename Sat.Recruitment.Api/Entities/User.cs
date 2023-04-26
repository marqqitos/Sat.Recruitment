using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.UserTypes;
using System;

namespace Sat.Recruitment.Api.Entities
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public IUserType UserType { get; set; }
        public decimal Money { get; set; }

        public void NormalizeEmail()
        {
            var aux = Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            Email = string.Join("@", new string[] { aux[0], aux[1] });
        }

        public bool IsSameUser(UserDTO userDTO)
            => Email == userDTO.Email || Phone == userDTO.Phone || (Name == userDTO.Name && Address == userDTO.Address);
    }
}
