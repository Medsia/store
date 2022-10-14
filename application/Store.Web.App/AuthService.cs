using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Store.Web.App
{
    public class AuthService
    {
        IUserRepository userRepository;

        public AuthService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        public async Task<bool> UserIsCorrect(string login, string password)
        {
            User user = await userRepository.GetUserOrDedaultAsync(login);

            if (user != null)
            {
                var result = PasswordHasher.Verify(password, user.Password);

                return result;
            }

            return false;
        }

        public IEnumerable<UserModel> GetAllAccounts()
        {
            IEnumerable<User> users = userRepository.GetAllUsers();

            return users.Select(Map)
                        .ToArray();
        }

        public UserModel Map (User user)
        {
            return new UserModel
            {
                Id = user.Id,
                Login = user.Login
            };
        }
    }
}
