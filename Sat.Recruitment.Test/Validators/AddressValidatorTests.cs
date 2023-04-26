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
    public class AddressValidatorTests
    {
        public AddressValidator _sut;

        [SetUp]
        public void Init()
        {
            _sut = new AddressValidator();
        }

        [Test]
        public void Validate_AddressIsValid_NoErrorMessageReturned()
        {
            //Arrange
            var newUser = new UserDTO();
            newUser.Address = "Address 123";

            //Act
            var errorMessage = _sut.Validate(newUser);

            //Assert
            errorMessage.Should().BeEmpty();
        }

        [Test]
        public void Validate_AddressIsNull_ErrorMessageAdded()
        {
            //Arrange
            var newUser = new UserDTO();

            //Act
            var errorMessage = _sut.Validate(newUser);

            //Assert
            errorMessage.Should().Be("The address is required");
        }


        [Test]
        public void Validate_AddressIsEmpty_ErrorMessageAdded()
        {
            //Arrange
            var newUser = new UserDTO();
            newUser.Address = string.Empty;

            //Act
            var errorMessage = _sut.Validate(newUser);

            //Assert
            errorMessage.Should().Be("The address is required");
        }
    }
}
