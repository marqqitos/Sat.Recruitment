using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Sat.Recruitment.Api.Builders.Interfaces;
using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Entities;
using Sat.Recruitment.Api.Exceptions;
using Sat.Recruitment.Api.Repositories.Interfaces;
using Sat.Recruitment.Api.Services;
using Sat.Recruitment.Api.Services.Interfaces;
using Sat.Recruitment.Api.UserTypes;
using Sat.Recruitment.Api.Validators.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Test
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserService _sut;
        private Fixture _fixture;
        private Mock<IUserValidator> mockUserValidator;
        private Mock<IUserBuilder> mockUserBuilder;
        private Mock<IRepository> mockUserRepository;

        [SetUp]
        public void Init()
        {
            mockUserValidator = new Mock<IUserValidator>();
            var validators = new List<IUserValidator>() { mockUserValidator.Object };

            mockUserBuilder = new Mock<IUserBuilder>();

            mockUserBuilder.Setup(mub => mub.WithName(It.IsAny<string>())).Returns(mockUserBuilder.Object);
            mockUserBuilder.Setup(mub => mub.WithEmail(It.IsAny<string>())).Returns(mockUserBuilder.Object);
            mockUserBuilder.Setup(mub => mub.WithAddress(It.IsAny<string>())).Returns(mockUserBuilder.Object);
            mockUserBuilder.Setup(mub => mub.WithPhone(It.IsAny<string>())).Returns(mockUserBuilder.Object);
            mockUserBuilder.Setup(mub => mub.WithUserType(It.IsAny<string>())).Returns(mockUserBuilder.Object);
            mockUserBuilder.Setup(mub => mub.WithMoney(It.IsAny<decimal>())).Returns(mockUserBuilder.Object);

            mockUserRepository = new Mock<IRepository>();
            mockUserRepository.Setup(m => m.GetUser(It.IsAny<UserDTO>())).ReturnsAsync((User)null);

            _sut = new UserService(validators, mockUserBuilder.Object, mockUserRepository.Object);
            _fixture = new Fixture();
        }

        [Test]
        [Category("ValidateNewUser")]
        public void ValidateNewUser_IfNewUserIsValid_ResultIsValid()
        {
            //Arrange
            var newUser = _fixture.Create<UserDTO>();
            mockUserValidator.Setup(validator => validator.Validate(It.IsAny<UserDTO>()))
                             .Returns(string.Empty);

            //Act
            var result = _sut.ValidateNewUser(newUser);

            //Assert
            result.IsValid.Should().BeTrue();
            result.ErrorMessages.Should().BeEmpty();
        }

        [Test]
        [Category("ValidateNewUser")]
        public void ValidateNewUser_IfNewUserIsNotValid_ContainsErrorMessages()
        {
            //Arrange
            var newUser = new UserDTO();
            mockUserValidator.Setup(validator => validator.Validate(It.IsAny<UserDTO>()))
                             .Returns("error");

            //Act
            var result = _sut.ValidateNewUser(newUser);

            //Assert
            result.IsValid.Should().BeFalse();
            result.ErrorMessages.Should().NotBeEmpty();
        }

        [Test]
        [Category("CreateUser")]
        public async Task CreateUser_WhenUserIsNormalAndMoneyIs100_CreatesNormalUserWith108USD()
        {
            //Arrange
            var userDto = _fixture.Build<UserDTO>()
                               .With(u => u.UserType, "Normal")
                               .With(u => u.Money, "100")
                               .Create();

            var user = new User()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Address = userDto.Address,
                Phone = userDto.Phone,
                UserType = new NormalUser(),
                Money = 108
            };

            mockUserBuilder.Setup(mub => mub.Build()).Returns(user);

            mockUserRepository.Setup(mur => mur.CreateUser(It.IsAny<UserDTO>())).ReturnsAsync(user);

            //Act
            var result = await _sut.CreateUser(userDto);

            //Assert
            result.Name.Should().Be(user.Name);
            result.Email.Should().Be(user.Email);
            result.Address.Should().Be(user.Address);
            result.Phone.Should().Be(user.Phone);
            result.UserType.Should().Be(user.UserType.GetUserType());
            result.Money.Should().Be(user.Money.ToString());
        }

        [Test]
        [Category("CreateUser")]
        public async Task CreateUser_WhenUserIsNormalAndMoneyIs101_CreatesNormalUserWith113_12USD()
        {
            //Arrange
            var userDto = _fixture.Build<UserDTO>()
                               .With(u => u.UserType, "Normal")
                               .With(u => u.Money, "101")
                               .Create();

            var user = new User()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Address = userDto.Address,
                Phone = userDto.Phone,
                UserType = new NormalUser(),
                Money = (decimal)113.12
            };

            mockUserBuilder.Setup(mub => mub.Build()).Returns(user);

            mockUserRepository.Setup(mur => mur.CreateUser(It.IsAny<UserDTO>())).ReturnsAsync(user);

            //Act
            var result = await _sut.CreateUser(userDto);

            //Assert
            result.Name.Should().Be(userDto.Name);
            result.Email.Should().Be(userDto.Email);
            result.Address.Should().Be(userDto.Address);
            result.Phone.Should().Be(userDto.Phone);
            result.UserType.Should().Be(userDto.UserType);
            result.Money.Should().Be(user.Money.ToString());
        }

        [Test]
        [Category("CreateUser")]
        public async Task CreateUser_WhenUserIsNormalAndMoneyIs10_CreatesNormalUserWith10USD()
        {
            //Arrange
            var userDto = _fixture.Build<UserDTO>()
                               .With(u => u.UserType, "Normal")
                               .With(u => u.Money, "10")
                               .Create();

            var user = new User()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Address = userDto.Address,
                Phone = userDto.Phone,
                UserType = new NormalUser(),
                Money = 10
            };

            mockUserBuilder.Setup(mub => mub.Build()).Returns(user);
            mockUserRepository.Setup(mur => mur.CreateUser(It.IsAny<UserDTO>())).ReturnsAsync(user);

            //Act
            var result = await _sut.CreateUser(userDto);

            //Assert
            result.Name.Should().Be(user.Name);
            result.Email.Should().Be(user.Email);
            result.Address.Should().Be(user.Address);
            result.Phone.Should().Be(user.Phone);
            result.UserType.Should().Be(user.UserType.GetUserType());
            result.Money.Should().Be(user.Money.ToString());
        }

        [Test]
        [Category("CreateUser")]
        public async Task CreateUser_WhenUserIsSuperUserAndMoneyIs100_CreatesSuperUserWith100USD()
        {
            //Arrange
            var userDto = _fixture.Build<UserDTO>()
                               .With(u => u.UserType, "SuperUser")
                               .With(u => u.Money, "100")
                               .Create();

            var user = new User()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Address = userDto.Address,
                Phone = userDto.Phone,
                UserType = new SuperUser(),
                Money = 100
            };

            mockUserBuilder.Setup(mub => mub.Build()).Returns(user);
            mockUserRepository.Setup(mur => mur.CreateUser(It.IsAny<UserDTO>())).ReturnsAsync(user);

            //Act
            var result = await _sut.CreateUser(userDto);

            //Assert
            result.Name.Should().Be(user.Name);
            result.Email.Should().Be(user.Email);
            result.Address.Should().Be(user.Address);
            result.Phone.Should().Be(user.Phone);
            result.UserType.Should().Be(user.UserType.GetUserType());
            result.Money.Should().Be(user.Money.ToString());
        }

        [Test]
        [Category("CreateUser")]
        public async Task CreateUser_WhenUserIsSuperUserAndMoneyIs101_CreatesSuperUserWith121_2USD()
        {
            //Arrange
            var userDto = _fixture.Build<UserDTO>()
                               .With(u => u.UserType, "SuperUser")
                               .With(u => u.Money, "101")
                               .Create();

            var user = new User()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Address = userDto.Address,
                Phone = userDto.Phone,
                UserType = new SuperUser(),
                Money = (decimal)121.2
            };

            mockUserBuilder.Setup(mub => mub.Build()).Returns(user);
            mockUserRepository.Setup(mur => mur.CreateUser(It.IsAny<UserDTO>())).ReturnsAsync(user);

            //Act
            var result = await _sut.CreateUser(userDto);

            //Assert
            result.Name.Should().Be(user.Name);
            result.Email.Should().Be(user.Email);
            result.Address.Should().Be(user.Address);
            result.Phone.Should().Be(user.Phone);
            result.UserType.Should().Be(user.UserType.GetUserType());
            result.Money.Should().Be(user.Money.ToString());
        }

        [Test]
        [Category("CreateUser")]
        public async Task CreateUser_WhenUserIsPremiumUserAndMoneyIs100_CreatesPremiumUserWith100USD()
        {
            //Arrange
            var userDto = _fixture.Build<UserDTO>()
                               .With(u => u.UserType, "Premium")
                               .With(u => u.Money, "100")
                               .Create();

            var user = new User()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Address = userDto.Address,
                Phone = userDto.Phone,
                UserType = new PremiumUser(),
                Money = 100
            };

            mockUserBuilder.Setup(mub => mub.Build()).Returns(user);
            mockUserRepository.Setup(mur => mur.CreateUser(It.IsAny<UserDTO>())).ReturnsAsync(user);

            //Act
            var result = await _sut.CreateUser(userDto);

            //Assert
            result.Name.Should().Be(user.Name);
            result.Email.Should().Be(user.Email);
            result.Address.Should().Be(user.Address);
            result.Phone.Should().Be(user.Phone);
            result.UserType.Should().Be(user.UserType.GetUserType());
            result.Money.Should().Be(user.Money.ToString());
        }

        [Test]
        [Category("CreateUser")]
        public async Task CreateUser_WhenUserIsPremiumUserAndMoneyIs101_CreatesPremiumUserWith202USD()
        {
            //Arrange
            var userDto = _fixture.Build<UserDTO>()
                               .With(u => u.UserType, "Premium")
                               .With(u => u.Money, "101")
                               .Create();

            var user = new User()
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Address = userDto.Address,
                Phone = userDto.Phone,
                UserType = new PremiumUser(),
                Money = 303
            };

            mockUserBuilder.Setup(mub => mub.Build()).Returns(user);
            mockUserRepository.Setup(mur => mur.CreateUser(It.IsAny<UserDTO>())).ReturnsAsync(user);

            //Act
            var result = await _sut.CreateUser(userDto);

            //Assert
            result.Name.Should().Be(user.Name);
            result.Email.Should().Be(user.Email);
            result.Address.Should().Be(user.Address);
            result.Phone.Should().Be(user.Phone);
            result.UserType.Should().Be(user.UserType.GetUserType());
            result.Money.Should().Be(user.Money.ToString());
        }

        [Test]
        [Category("CreateUser")]
        public async Task CreateUser_WhenMoneyIsNotValid_ThrowsArgumentException()
        {
            //Arrange
            var userDto = _fixture.Build<UserDTO>()
                               .With(u => u.UserType, "Premium")
                               .With(u => u.Money, "101a")
                               .Create();

            //Act
            Func<Task> act = async () => await _sut.CreateUser(userDto);

            //Assert
            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Test]
        [Category("CreateUser")]
        public async Task CreateUser_WhenUserIsAlreadyCreated_ThrowsEntityExistsException()
        {
            //Arrange
            var userDto = _fixture.Build<UserDTO>()
                               .With(u => u.UserType, "Premium")
                               .With(u => u.Money, "101")
                               .Create();

            var existingUser = _fixture.Build<User>()
                                       .OmitAutoProperties()
                                       .With(u => u.Name, "Marcos")
                                       .Create();

            mockUserRepository.Setup(mur => mur.GetUser(It.IsAny<UserDTO>())).ReturnsAsync(existingUser);

            //Act
            Func<Task> act = async () => await _sut.CreateUser(userDto);

            //Assert
            await act.Should().ThrowAsync<EntityExistsException>();
        }
    }
}
