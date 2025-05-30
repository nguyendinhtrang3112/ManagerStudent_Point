using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiApp2.Models;
using MauiApp2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiApp2.ViewModels;

public partial class TeacherListViewModel : ObservableObject
{
    private readonly DatabaseHelper _dbHelper;

    public ObservableCollection<Teacher> Teachers { get; } = new();

    [ObservableProperty]
    private string searchKeyword;

    public TeacherListViewModel()
    {
        _dbHelper = new DatabaseHelper();
        LoadTeachersCommand = new AsyncRelayCommand(LoadTeachersAsync);
        AddTeacherCommand = new AsyncRelayCommand(AddTeacherAsync);
        DeleteTeacherCommand = new AsyncRelayCommand<Teacher>(DeleteTeacherAsync);
        EditTeacherCommand = new AsyncRelayCommand<Teacher>(EditTeacherAsync);
    }

    public IAsyncRelayCommand LoadTeachersCommand { get; }
    public IAsyncRelayCommand AddTeacherCommand { get; }
    public IAsyncRelayCommand DeleteTeacherCommand { get; }
    public IAsyncRelayCommand EditTeacherCommand { get; }

    private async Task LoadTeachersAsync()
    {
        Teachers.Clear();
        var list = await _dbHelper.GetAllTeachersAsync();

        var filtered = string.IsNullOrWhiteSpace(SearchKeyword)
            ? list
            : list.Where(t => t.Name.ToLower().Contains(SearchKeyword.ToLower())).ToList();

        foreach (var teacher in filtered)
            Teachers.Add(teacher);
    }

    private async Task AddTeacherAsync()
    {
        string name = await App.Current.MainPage.DisplayPromptAsync("Tên", "Nhập tên giáo viên:");
        if (string.IsNullOrWhiteSpace(name)) return;

        string email = await App.Current.MainPage.DisplayPromptAsync("Email", "Nhập email giáo viên:");
        if (string.IsNullOrWhiteSpace(email)) return;

        var teacher = new Teacher { Name = name, Email = email };
        await _dbHelper.AddTeacherAsync(teacher);
        await LoadTeachersAsync();
    }

    private async Task DeleteTeacherAsync(Teacher teacher)
    {
        if (teacher == null) return;

        bool confirm = await App.Current.MainPage.DisplayAlert("Xoá", $"Xác nhận xoá {teacher.Name}?", "Xoá", "Huỷ");
        if (!confirm) return;

        await _dbHelper.DeleteTeacherAsync(teacher);
        await LoadTeachersAsync();
    }

    private async Task EditTeacherAsync(Teacher teacher)
    {
        if (teacher == null) return;

        string newName = await App.Current.MainPage.DisplayPromptAsync("Sửa tên", "Tên mới:", initialValue: teacher.Name);
        if (string.IsNullOrWhiteSpace(newName)) return;

        string newEmail = await App.Current.MainPage.DisplayPromptAsync("Sửa email", "Email mới:", initialValue: teacher.Email);
        if (string.IsNullOrWhiteSpace(newEmail)) return;

        teacher.Name = newName;
        teacher.Email = newEmail;

        await _dbHelper.UpdateTeacherAsync(teacher);
        await LoadTeachersAsync();
    }
}
