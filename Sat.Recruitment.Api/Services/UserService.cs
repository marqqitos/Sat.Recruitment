using Sat.Recruitment.Api.Builders;
using Sat.Recruitment.Api.Builders.Interfaces;
using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Entities;
using Sat.Recruitment.Api.Exceptions;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Repositories.Interfaces;
using Sat.Recruitment.Api.Services.Interfaces;
using Sat.Recruitment.Api.UserTypes;
using Sat.Recruitment.Api.Validators.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IEnumerable<IUserValidator> _validators;
        private readonly IUserBuilder _userBuilder;
        private readonly IRepository _userRepository;

        public UserService(IEnumerable<IUserValidator> validators, IUserBuilder userBuilder, IRepository userRepository)
        {
            _validators = validators;
            _userBuilder = userBuilder;
            _userRepository = userRepository;
        }

        public NewUserValidationResult ValidateNewUser(UserDTO newUser)
        {
            var result = new NewUserValidationResult();

            result.ErrorMessages = _validators.Select(validator => validator.Validate(newUser))
                                              .Where(errorMessage => !string.IsNullOrWhiteSpace(errorMessage))
                                              .ToList();


            result.IsValid = !result.ErrorMessages.Any();
            return result;
        }

        public async Task<UserDTO> CreateUser(UserDTO newUser)
        {
            bool isDecimal = decimal.TryParse(newUser.Money, out _);

            if (!isDecimal)
                throw new ArgumentException("Money must be a decimal number");

            var existingUser = await _userRepository.GetUser(newUser);

            if (existingUser is not null)
                throw new EntityExistsException("User already exists");

            var user = await _userRepository.CreateUser(newUser);

            return new UserDTO()
            {
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,
                UserType = user.UserType.GetUserType(),
                Money = user.Money.ToString()
            };
        }
    }
}
