using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Exceptions;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDTO newUserDto)
        {
            _logger.LogDebug("Validating user - Name: {name}, Email: {email}, Address: {address}, Phone: {phone}", 
                newUserDto.Name, newUserDto.Email, newUserDto.Address, newUserDto.Phone);

            var validationResult = _userService.ValidateNewUser(newUserDto);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(", ", validationResult.ErrorMessages);

                _logger.LogError("CreateUser - Validation Errors: {errorMessages}", errorMessages);

                var result = new Result()
                {
                    IsSuccess = false,
                    Errors = errorMessages
                };

                return BadRequest(result);
            }

            try
            {
                _logger.LogDebug("User passes initial validation, " +
                    "creating user -> Name: {Name}, Email: {email}, Address: {address}, Phone: {phone}, UserType: {type}, Money: {money}",
                    newUserDto.Name, newUserDto.Email, newUserDto.Address, newUserDto.Phone, newUserDto.UserType, newUserDto.Money);

                var user = await _userService.CreateUser(newUserDto);

                var result = new Result()
                {
                    IsSuccess = true,
                    Data = user
                };

                _logger.LogDebug("User {email} created succesfully", user.Email);

                return Ok(result);
            }
            catch (Exception ex) when 
            (
                ex is ArgumentException ||
                ex is EntityExistsException
            )
            {
                _logger.LogError("CreateUser - There was an error when trying to create the user. " +
                    "Exception type: {exType} - Exception Message: {exMessage} - Stacktrace: {exStackTrace}", 
                    ex.GetType(), 
                    ex.Message, 
                    ex.StackTrace
                );

                var result = new Result()
                {
                    IsSuccess = false,
                    Errors = ex.Message
                };

                return BadRequest(result);
            }
        }

    }
}
