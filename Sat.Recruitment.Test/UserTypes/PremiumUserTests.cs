using FluentAssertions;
using NUnit.Framework;
using Sat.Recruitment.Api.UserTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Test.UserTypes
{
    [TestFixture]
    public class PremiumUserTests
    {
        public PremiumUser _sut;

        [SetUp]
        public void Init()
        {
            _sut = new PremiumUser();
        }

        [Test]
        [Category("GetUserType")]
        public void GetUserType_WhenIsNormalUser_ReturnsTrue()
        {
            //Arrange
            var expected = "Premium";

            //Act
            var result = _sut.GetUserType();

            //Assert
            result.Should().Be(expected);
        }

        [Test]
        [Category("CalculateMoneyWithBonus")]
        public void CalculateMoneyWithBonus_MoneyToDepositIsGreaterThan100_ReturnsTwoTimesMoney()
        {
            //Arrange
            var moneyToDeposit = 101;
            var expected = moneyToDeposit * 3;

            //Act
            var result = _sut.CalculateMoneyWithBonus(moneyToDeposit);

            //Assert
            result.Should().Be(expected);
        }

        [Test]
        [Category("CalculateMoneyWithBonus")]
        public void CalculateMoneyWithBonus_MoneyToDepositIs100_ReturnsMoneyWithNoBonus()
        {
            //Arrange
            var moneyToDeposit = 100;

            //Act
            var result = _sut.CalculateMoneyWithBonus(moneyToDeposit);

            //Assert
            result.Should().Be(moneyToDeposit);
        }

        [Test]
        [Category("CalculateMoneyWithBonus")]
        public void CalculateMoneyWithBonus_MoneyToDepositIsLess10_ReturnsMoneyWithNoBonus()
        {
            //Arrange
            var moneyToDeposit = 10;

            //Act
            var result = _sut.CalculateMoneyWithBonus(moneyToDeposit);

            //Assert
            result.Should().Be(moneyToDeposit);
        }
    }
}
