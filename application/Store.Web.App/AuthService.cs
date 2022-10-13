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
                //var hash = PasswordHasher.Hash(password);

                var result = PasswordHasher.Verify(password, user.Password);

                return result;
            }

            return false;
        }
    }
}
