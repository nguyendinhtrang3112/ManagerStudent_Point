using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiApp2.Models;
using MauiApp2.Services;
using Microsoft.Maui.Controls;

namespace MauiApp2.ViewModels
{
    public class UserDisplay : BindableObject
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        private string _newRole = string.Empty;
        public string NewRole
        {
            get => _newRole;
            set
            {
                _newRole = value;
                OnPropertyChanged();
            }
        }
    }

    public class UserManagementViewModel : BindableObject
    {
        private readonly DatabaseHelper _database;

        public ObservableCollection<UserDisplay> Users { get; } = new();
        public List<string> RoleOptions { get; } = new() { "student", "teacher", "admin" };

        public ICommand SaveRoleCommand { get; }

        public UserManagementViewModel()
        {
            _database = new DatabaseHelper();
            SaveRoleCommand = new Command<UserDisplay>(OnSaveRole);
            LoadUsers();
        }

        private async void LoadUsers()
        {
            Users.Clear();
            var userList = await _database.GetAllUsersAsync();

            foreach (var u in userList)
            {
                Users.Add(new UserDisplay
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    Role = u.Role,
                    NewRole = u.Role
                });
            }
        }

        private async void OnSaveRole(UserDisplay userDisplay)
        {
            // Kiểm tra giá trị null hoặc trống trước
            if (string.IsNullOrWhiteSpace(userDisplay.NewRole))
            {
                await Application.Current.MainPage.DisplayAlert("❌ Lỗi", "Vui lòng chọn vai trò hợp lệ.", "OK");
                return;
            }

            if (userDisplay.Role != userDisplay.NewRole)
            {
                var user = await _database.GetUserByUsernameAsync(userDisplay.Username);
                if (user != null)
                {
                    user.Role = userDisplay.NewRole;
                    await _database.UpdateUserAsync(user);

                    // Cập nhật lại giao diện
                    userDisplay.Role = user.Role;
                    await Application.Current.MainPage.DisplayAlert("✅ Thành công", $"Đã cấp quyền '{user.Role}' cho {user.Username}", "OK");
                }
            }
        }

    }
}
