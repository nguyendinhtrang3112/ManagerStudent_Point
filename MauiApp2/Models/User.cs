using SQLite;

namespace MauiApp2.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique, NotNull]
        public string Username { get; set; }

        [NotNull]
        public string Email { get; set; }

        [NotNull]
        public string Password { get; set; }

        public string Role { get; set; } = "student"; // 'student', 'teacher', 'admin'

        public User()
        {
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            Role = "student";
        }

        public User(string username, string email, string password, string role = "student")
        {
            Username = username;
            Email = email;
            Password = password;
            Role = role;
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Username)
                && !string.IsNullOrWhiteSpace(Email)
                && !string.IsNullOrWhiteSpace(Password)
                && !string.IsNullOrWhiteSpace(Role);
        }
    }
}
