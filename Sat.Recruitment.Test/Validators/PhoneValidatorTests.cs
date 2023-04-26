using FluentAssertions;
using NUnit.Framework;
using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Test.Validators
{
    [TestFixture]
    public class PhoneValidatorTests
    {
        public PhoneValidator _sut;

        [SetUp]
        public void Init()
        {
            _sut = new PhoneValidator();
        }

        [Test]
        public void Validate_PhoneIsValid_NoErrorMessageReturned()
        {
            //Arrange
            var newUser = new UserDTO();
            newUser.Phone = "444-5555";

            //Act
            var errorMessage = _sut.Validate(newUser);

            //Assert
            errorMessage.Should().BeEmpty();
        }

        [Test]
        public void Validate_PhoneIsNull_ErrorMessageAdded()
        {
            //Arrange
            var newUser = new UserDTO();

            //Act
            var errorMessage = _sut.Validate(newUser);

            //Assert
            errorMessage.Should().Be("The phone is required");
        }


        [Test]
        public void Validate_PhoneIsEmpty_ErrorMessageAdded()
        {
            //Arrange
            var newUser = new UserDTO();
            newUser.Phone = string.Empty;

            //Act
            var errorMessage = _sut.Validate(newUser);

            //Assert
            errorMessage.Should().Be("The phone is required");
        }
    }
}
