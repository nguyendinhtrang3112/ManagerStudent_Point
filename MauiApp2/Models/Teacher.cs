using SQLite;

namespace MauiApp2.Models
{
    public class Teacher
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string Email { get; set; }

        // Liên kết với tài khoản đăng nhập (nếu có)
        public int? UserId { get; set; } // FK đến User.Id, có thể null nếu giáo viên chưa có tài khoản

        public Teacher()
        {
            Name = string.Empty;
            Email = string.Empty;
        }

        public Teacher(string name, string email, int? userId = null)
        {
            Name = name;
            Email = email;
            UserId = userId;
        }
    }
}
