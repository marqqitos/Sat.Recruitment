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
    public class EmailValidatorTests
    {
        public EmailValidator _sut;

        [SetUp]
        public void Init()
        {
            _sut = new EmailValidator();
        }

        [Test]
        public void Validate_EmailIsValid_NoErrorMessageReturned()
        {
            //Arrange
            var newUser = new UserDTO();
            newUser.Email = "name@mail.com";

            //Act
            var errorMessage = _sut.Validate(newUser);

            //Assert
            errorMessage.Should().BeEmpty();
        }

        [Test]
        public void Validate_EmailIsNull_ErrorMessageAdded()
        {
            //Arrange
            var newUser = new UserDTO();

            //Act
            var errorMessage = _sut.Validate(newUser);

            //Assert
            errorMessage.Should().Be("The email is required");
        }


        [Test]
        public void Validate_EmailIsEmpty_ErrorMessageAdded()
        {
            //Arrange
            var newUser = new UserDTO();
            newUser.Email = string.Empty;

            //Act
            var errorMessage = _sut.Validate(newUser);

            //Assert
            errorMessage.Should().Be("The email is required");
        }

        [Test]
        [TestCase("john")]
        [TestCase("john@mail@mail.com")]
        [TestCase("john[@mail.com")]
        public void Validate_EmailHasIncorrectFormat_ErrorMessageAdded(string email)
        {
            //Arrange
            var newUser = new UserDTO();
            newUser.Email = email;

            //Act
            var errorMessage = _sut.Validate(newUser);

            //Assert
            errorMessage.Should().Be("The email is not formatted correctly");
        }
    }
}
