﻿using MauiApp2.Services;
using MauiApp2.Views;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using MauiApp2.Models;

namespace MauiApp2.ViewModels
{
    public class LoginViewModel : BindableObject
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _isErrorVisible = false;
        private readonly UserService _userService;

        public LoginViewModel()
        {
            _userService = new UserService();

            LoginCommand = new Command(OnLogin);
            NavigateToRegisterCommand = new Command(OnNavigateToRegister);
        }

        public string Username
        {
            get => _username;
            set
            {
                if (_username == value)
                    return;
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password == value)
                    return;
                _password = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (_errorMessage == value)
                    return;
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public bool IsErrorVisible
        {
            get => _isErrorVisible;
            set
            {
                if (_isErrorVisible == value)
                    return;
                _isErrorVisible = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }

        private async void OnLogin()
        {
            IsErrorVisible = false;

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Vui lòng nhập tên đăng nhập và mật khẩu";
                IsErrorVisible = true;
                return;
            }

            var user = await _userService.GetUserByUsernameAndPassword(Username, Password);

            if (user != null)
            {
                switch (user.Role.ToLower())
                {
                    case "admin":
                        await Shell.Current.GoToAsync("//MainPage");
                        break;
                    case "teacher":
                        await Shell.Current.GoToAsync("//MainPage");
                        break;
                    case "student":
                    default:
                        await Shell.Current.GoToAsync("//StudentPage");
                        break;
                }
            }
            else
            {
                ErrorMessage = "Sai tên đăng nhập hoặc mật khẩu";
                IsErrorVisible = true;
            }
        }


        private async void OnNavigateToRegister()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SignUpPage());
        }
    }
}
