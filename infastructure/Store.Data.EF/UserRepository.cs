using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.EF
{
    class UserRepository : IUserRepository
    {
        private readonly DbContextFactory dbContextFactory;
        public UserRepository(DbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }


        public async Task<User> GetUserOrDedaultAsync(string login)
        {
            var dbContext = dbContextFactory.Create(typeof(DbContextFactory));

            var dto = await dbContext.Users
                               .SingleOrDefaultAsync(user => user.Login == login);

            if (dto == null)
                return null;

            return User.Mapper.Map(dto);
        }

        public void AddUser(UserDto userDto)
        {
            var dbContext = dbContextFactory.Create(typeof(DbContextFactory));

            dbContext.Users.Add(userDto);
            dbContext.SaveChanges();
        }

        public void EditUser(UserDto userDto)
        {
            var dbContext = dbContextFactory.Create(typeof(DbContextFactory));

            dbContext.Users.Update(userDto);
            dbContext.SaveChanges();
        }
        public void DeleteUser(UserDto userDto)
        {
            var dbContext = dbContextFactory.Create(typeof(DbContextFactory));

            dbContext.Users.Remove(userDto);
            dbContext.SaveChanges();
        }
    }
}
