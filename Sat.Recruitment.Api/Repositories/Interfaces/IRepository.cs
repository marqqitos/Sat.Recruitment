using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Repositories.Interfaces
{
    public interface IRepository
    {
        public Task<User> GetUser(UserDTO userDTO);
        public Task<User> CreateUser(UserDTO userDTO);
    }
}
