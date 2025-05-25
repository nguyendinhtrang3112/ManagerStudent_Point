using MauiApp2.Models;
using System.Collections.Generic;

namespace MauiApp2.Data
{
    public class UserRepository
    {
        private List<User> _users;

        public UserRepository()
        {
            // Danh sách người dùng mẫu
            _users = new List<User>
            {
                new User { Username = "admin", Password = "1234", Email = "admin@example.com" },
                new User { Username = "test", Password = "test", Email = "test@example.com" }
            };
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }
    }
}
