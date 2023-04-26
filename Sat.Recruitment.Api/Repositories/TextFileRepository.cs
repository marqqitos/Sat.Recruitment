using Sat.Recruitment.Api.Builders.Interfaces;
using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Entities;
using Sat.Recruitment.Api.Repositories.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Sat.Recruitment.Api.Repositories
{
    public class TextFileRepository : IRepository
    {
        private IUserBuilder _userBuilder;
        private readonly string PATH = Directory.GetCurrentDirectory() + "/Files/Users.txt";

        public TextFileRepository(IUserBuilder userBuilder)
        {
            _userBuilder = userBuilder;
        }

        public async Task<User> GetUser(UserDTO userDTO)
        {
            var reader = GetReaderForUsersFile();

            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();

                if (!string.IsNullOrWhiteSpace(line))
                {
                    var textFileUser = line.Split(',');

                    var userEntity = _userBuilder
                        .WithName(textFileUser[0])
                        .WithEmail(textFileUser[1])
                        .WithPhone(textFileUser[2])
                        .WithAddress(textFileUser[3])
                        .WithUserType(textFileUser[4])
                        .WithMoney(decimal.Parse(textFileUser[5]))
                        .Build();

                    if (userEntity.IsSameUser(userDTO))
                    {
                        reader.Close();
                        return userEntity;
                    }
                }
            }

            reader.Close();

            return null;
        }

        public async Task<User> CreateUser(UserDTO userDTO)
        {
            var user = _userBuilder
                .WithName(userDTO.Name)
                .WithEmail(userDTO.Email)
                .WithPhone(userDTO.Phone)
                .WithAddress(userDTO.Address)
                .WithUserType(userDTO.UserType)
                .WithMoney(decimal.Parse(userDTO.Money))
                .Build();

            user.NormalizeEmail();

            var stringUser = string.Join(",", user.Name, user.Email, user.Phone, user.Address, user.UserType.GetUserType(), user.Money.ToString());

            using (StreamWriter writer = new StreamWriter(PATH, true))
            {
                await writer.WriteAsync(writer.NewLine);
                await writer.WriteAsync(stringUser);
            }

            return user;
        }

        private StreamReader GetReaderForUsersFile()
        {
            FileStream fileStream = new FileStream(PATH, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
