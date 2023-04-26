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
    public class NameValidatorTests
    {
        public NameValidator _sut;

        [SetUp]
        public void Init()
        {
            _sut = new NameValidator();
        }

        [Test]
        public void Validate_NameIsValid_NoErrorMessageReturned()
        {
            //Arrange
            var newUser = new UserDTO();
            newUser.Name = "John";

            //Act
            var errorMessage = _sut.Validate(newUser);

            //Assert
            errorMessage.Should().BeEmpty();
        }

        [Test]
        public void Validate_NameIsNull_ErrorMessageAdded()
        {
            //Arrange
            var newUser = new UserDTO();

            //Act
            var errorMessage = _sut.Validate(newUser);

            //Assert
            errorMessage.Should().Be("The name is required");
        }


        [Test]
        public void Validate_NameIsEmpty_ErrorMessageAdded()
        {
            //Arrange
            var newUser = new UserDTO();
            newUser.Name = string.Empty;

            //Act
            var errorMessage = _sut.Validate(newUser);

            //Assert
            errorMessage.Should().Be("The name is required");
        }
    }
}
