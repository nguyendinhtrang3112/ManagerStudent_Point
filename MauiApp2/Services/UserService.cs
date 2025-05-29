using MauiApp2.Models;
using System.Threading.Tasks;

namespace MauiApp2.Services
{
    public class UserService
    {
        private readonly DatabaseHelper _database;
        public UserService()
        {
            _database = new DatabaseHelper();
        }

        /// <summary>
        /// Kiểm tra người dùng đã tồn tại chưa dựa trên username.
        /// </summary>
        public async Task<bool> IsUserExists(string username)
        {
            var existingUser = await _database.GetUserByUsernameAsync(username);
            return existingUser != null;
        }

        /// <summary>
        /// Thêm người dùng mới vào cơ sở dữ liệu.
        /// </summary>
        public Task<int> AddUser(User user)
        {
            return _database.AddUserAsync(user);
        }

        /// <summary>
        /// Xác thực tài khoản đăng nhập.
        /// </summary>
        public async Task<bool> Authenticate(string username, string password)
        {
            var user = await _database.GetUserByUsernameAsync(username);
            return user != null && user.Password == password;
        }

        /// <summary>
        /// Lấy user đầy đủ (có Role) nếu xác thực thành công.
        /// </summary>
        public async Task<User?> GetUserByUsernameAndPassword(string username, string password)
        {
            var user = await _database.GetUserByUsernameAsync(username);
            if (user != null && user.Password == password)
            {
                return user;
            }
            return null;
        }
        public async Task<User?> GetUserByUsername(string username)
        {
            return await _database.GetUserByUsernameAsync(username);
        }
    }
}
