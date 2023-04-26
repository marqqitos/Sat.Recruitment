using FluentAssertions;
using NUnit.Framework;
using Sat.Recruitment.Api.UserTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Test.UserTypes
{
    [TestFixture]
    public class NormalUserTests
    {
        public NormalUser _sut;

        [SetUp]
        public void Init()
        {
            _sut = new NormalUser();
        }

        [Test]
        [Category("GetUserType")]
        public void GetUserType_WhenIsNormalUser_ReturnsTrue()
        {
            //Arrange
            var expected = "Normal";

            //Act
            var result = _sut.GetUserType();

            //Assert
            result.Should().Be(expected);
        }

        [Test]
        [Category("CalculateMoneyWithBonus")]
        public void CalculateMoneyWithBonus_MoneyToDepositIsGreaterThan100_ReturnsMoneyWithBonusOf12Percent()
        {
            //Arrange
            var moneyToDeposit = 101;
            var expected = Convert.ToDecimal(moneyToDeposit * 1.12);

            //Act
            var result = _sut.CalculateMoneyWithBonus(moneyToDeposit);

            //Assert
            result.Should().Be(expected);
        }

        [Test]
        [Category("CalculateMoneyWithBonus")]
        public void CalculateMoneyWithBonus_MoneyToDepositIs100_ReturnsMoneyWithBonusOf8Percent()
        {
            //Arrange
            var moneyToDeposit = 100;
            var expected = Convert.ToDecimal(moneyToDeposit * 1.08);

            //Act
            var result = _sut.CalculateMoneyWithBonus(moneyToDeposit);

            //Assert
            result.Should().Be(expected);
        }

        [Test]
        [Category("CalculateMoneyWithBonus")]
        public void CalculateMoneyWithBonus_MoneyToDepositIs10_ReturnsMoneyWithNoBonus()
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
