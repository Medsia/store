
using Store.Data;
using System;

namespace Store
{
    public class User
    {
        private readonly UserDto dto;

        public int Id => dto.Id;

        public string Login
        {
            get => dto.Login;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(nameof(Login));

                dto.Login = value.Trim();
            }
        }

        public string Password
        {
            get => dto.Password;
            set => dto.Password = value;
        }

        internal User(UserDto dto)
        {
            this.dto = dto;
        }

        public static class DtoFactory
        {
            public static UserDto Create(string login, string password)
            {

                if (string.IsNullOrWhiteSpace(login))
                    throw new ArgumentException(nameof(login));

                return new UserDto
                {
                    Login = login,
                    Password = password
                };
            }
        }
        public static class Mapper
        {
            public static User Map(UserDto dto) => new User(dto);

            public static UserDto Map(User domain) => domain.dto;
        }
    }
}
