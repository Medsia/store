using Store.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        Task<User> GetUserOrDedaultAsync(string login);
        void AddUser(UserDto userDto);
        void EditUser(UserDto userDto);
        void DeleteUser(UserDto userDto);
    }
}
