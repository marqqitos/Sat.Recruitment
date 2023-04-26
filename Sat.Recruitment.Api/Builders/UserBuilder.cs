using Sat.Recruitment.Api.Builders.Interfaces;
using Sat.Recruitment.Api.Entities;
using Sat.Recruitment.Api.UserTypes;
using System;

namespace Sat.Recruitment.Api.Builders
{
    public class UserBuilder : IUserBuilder
    {
        private string Name;
        private string Email;
        private string Phone;
        private string Address;
        private IUserType UserType;
        private decimal Money;

        public IUserBuilder WithName(string name)
        {
            this.Name = name;
            return this;
        }

        public IUserBuilder WithEmail(string email)
        {
            this.Email = email;
            return this;
        }

        public IUserBuilder WithPhone(string phone)
        {
            this.Phone = phone;
            return this;
        }

        public IUserBuilder WithAddress(string address)
        {
            this.Address = address;
            return this;
        }

        public IUserBuilder WithUserType(string userType)
        {
            this.UserType = GetUserType(userType);
            return this;
        }

        public IUserBuilder WithMoney(decimal money)
        {
            this.Money = money;
            return this;
        }

        public User Build()
        {
            return new User()
            {
                Name = Name,
                Email = Email,
                Phone = Phone,
                Address = Address,
                UserType = UserType,
                Money = UserType.CalculateMoneyWithBonus(Money)
            };
        }

        private IUserType GetUserType(string userType)
        {
            // Get user type based on input
            return userType switch
            {
                "Normal" => new NormalUser(),
                "SuperUser" => new SuperUser(),
                "Premium" => new PremiumUser(),
                _ => throw new ArgumentException("Invalid user type")
            };
        }
    }
}
