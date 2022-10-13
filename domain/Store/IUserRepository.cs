using Store.Data;
using System.Threading.Tasks;

namespace Store
{
    public interface IUserRepository
    {
        Task<User> GetUserOrDedaultAsync(string login);
        void AddUser(UserDto userDto);
        void EditUser(UserDto userDto);
        void DeleteUser(UserDto userDto);
    }
}
