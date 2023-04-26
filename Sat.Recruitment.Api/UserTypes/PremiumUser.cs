namespace Sat.Recruitment.Api.UserTypes
{
    public class PremiumUser : IUserType
    {
        public string GetUserType()
            => "Premium";

        public decimal CalculateMoneyWithBonus(decimal money)
            => money > 100 ? money * 3 : money;

    }
}
