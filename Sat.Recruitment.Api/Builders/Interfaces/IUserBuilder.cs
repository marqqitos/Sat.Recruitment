using Sat.Recruitment.Api.Entities;
using Sat.Recruitment.Api.UserTypes;

namespace Sat.Recruitment.Api.Builders.Interfaces
{
    public interface IUserBuilder
    {
        IUserBuilder WithName(string name);
        IUserBuilder WithEmail(string email);
        IUserBuilder WithPhone(string phone);
        IUserBuilder WithAddress(string address);
        IUserBuilder WithUserType(string userType);
        IUserBuilder WithMoney(decimal money);
        User Build();
    }

}
