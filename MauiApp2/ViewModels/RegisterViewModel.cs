using MauiApp2.Models;
using MauiApp2.Services;
using MauiApp2.Views;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace MauiApp2.ViewModels
{
    public class RegisterViewModel : BindableObject
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _email = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isErrorVisible = false;

        private readonly UserService _userService;

        public RegisterViewModel()
        {
            _userService = new UserService();

            RegisterCommand = new Command(async () => await OnRegister());
            NavigateToLoginCommand = new Command(async () => await OnNavigateToLogin());
        }

        public string Username
        {
            get => _username;
            set
            {
                if (_username == value) return;
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password == value) return;
                _password = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (_email == value) return;
                _email = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (_errorMessage == value) return;
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public bool IsErrorVisible
        {
            get => _isErrorVisible;
            set
            {
                if (_isErrorVisible == value) return;
                _isErrorVisible = value;
                OnPropertyChanged();
            }
        }

        public ICommand RegisterCommand { get; }
        public ICommand NavigateToLoginCommand { get; }

        private async Task OnRegister()
        {
            IsErrorVisible = false;

            if (string.IsNullOrWhiteSpace(Username) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(Email))
            {
                ErrorMessage = "Vui lòng nhập đầy đủ thông tin.";
                IsErrorVisible = true;
                return;
            }

            if (await _userService.IsUserExists(Username))
            {
                ErrorMessage = "Tên đăng nhập đã tồn tại.";
                IsErrorVisible = true;
                return;
            }

            var newUser = new User
            {
                Username = Username,
                Password = Password,
                Email = Email
            };

            await _userService.AddUser(newUser);

            await Application.Current.MainPage.DisplayAlert("Thành công", "Đăng ký tài khoản thành công!", "OK");
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }

        private async Task OnNavigateToLogin()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
    }
}
