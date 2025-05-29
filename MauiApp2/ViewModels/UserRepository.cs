using MauiApp2.Models;
using System.Collections.Generic;
using System.Linq;

namespace MauiApp2.Data
{
    public class UserRepository
    {
        private List<User> _users;

        public UserRepository()
        {
            // Danh sách người dùng mẫu với Role
            _users = new List<User>
            {
                new User { Username = "admin", Password = "1234", Email = "admin@example.com", Role = "admin" },
                new User { Username = "teacher1", Password = "teach", Email = "teacher1@example.com", Role = "teacher" },
                new User { Username = "student1", Password = "stud", Email = "student1@example.com", Role = "student" }
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

        // Tìm user theo tài khoản & mật khẩu
        public User? GetUser(string username, string password)
        {
            return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
