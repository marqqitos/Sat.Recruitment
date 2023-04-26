using System;

namespace Sat.Recruitment.Api.UserTypes
{
    public class SuperUser : IUserType
    {
        public string GetUserType()
            => "SuperUser";

        public decimal CalculateMoneyWithBonus(decimal money)
            => (money > 100) ? money * Convert.ToDecimal(1.20) : money;
    }
}
