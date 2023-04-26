namespace Sat.Recruitment.Api.UserTypes
{
    public interface IUserType
    {
        public string GetUserType();
        public decimal CalculateMoneyWithBonus(decimal money);
    }
}
