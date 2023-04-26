using System;

namespace Sat.Recruitment.Api.UserTypes
{
    public class NormalUser : IUserType
    {
        public string GetUserType()
            => "Normal";
        
        public decimal CalculateMoneyWithBonus(decimal money)
        {
            var bonus = money switch {
                > 100 => money * Convert.ToDecimal(0.12),
                > 10 => money * Convert.ToDecimal(0.08),
                _ => 0
            };

            return money + bonus;
        }
    }
}
