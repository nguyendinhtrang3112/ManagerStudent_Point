using MauiApp2.Models;
using MauiApp2.Services;
using System.Collections.Generic;

namespace MauiApp2;

public partial class MainPage : ContentPage
{
    private DatabaseHelper _dbHelper;

    public MainPage()
    {
        InitializeComponent();
        _dbHelper = new DatabaseHelper();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await InitSubjectsIfEmpty();   // Tạo dữ liệu mẫu nếu trống
        await LoadSubjects();          // Nạp danh sách môn học
        await LoadStudents();          // Tải danh sách học sinh nếu cần
        SubjectPlaceholder.IsVisible = true; // Hiện placeholder khi mới vào trang
    }

    private async Task InitSubjectsIfEmpty()
    {
        var existingSubjects = await _dbHelper.GetAllSubjectsAsync();
        if (existingSubjects.Count == 0)
        {
            await _dbHelper.AddSubjectAsync(new Subject("Toán", "MATH"));
            await _dbHelper.AddSubjectAsync(new Subject("Văn", "LIT"));
            await _dbHelper.AddSubjectAsync(new Subject("Anh", "ENG"));
        }
    }

    private async Task LoadSubjects()
    {
        var subjects = await _dbHelper.GetAllSubjectsAsync();
        SubjectPicker.ItemsSource = subjects;
        SubjectPicker.SelectedIndex = -1; // Reset index
        SubjectPlaceholder.IsVisible = true; // Reset placeholder
    }

    private async void OnAddStudentClicked(object sender, EventArgs e)
    {
        if (SubjectPicker.SelectedItem is not Subject selectedSubject)
        {
            await DisplayAlert("Lỗi", "Vui lòng chọn môn học", "OK");
            return;
        }

        var student = new Student
        {
            Name = NameEntry.Text?.Trim(),
            Grade = double.TryParse(GradeEntry.Text, out double g) ? g : 0,
            Class = ClassEntry.Text?.Trim(),
            DateOfBirth = DateOfBirthPicker.Date,
            SubjectId = selectedSubject.Id
        };

        // Gọi phương thức thêm học sinh (đã có kiểm tra trùng trong DatabaseHelper)
        var result = await _dbHelper.AddStudentAsync(student);

        if (result == 0)
        {
            await DisplayAlert("Thông báo", "Học sinh này đã tồn tại với môn học đã chọn!", "OK");
            return;
        }

        // Reset input fields
        NameEntry.Text = "";
        GradeEntry.Text = "";
        ClassEntry.Text = "";
        SubjectPicker.SelectedIndex = -1;
        SubjectPlaceholder.IsVisible = true;

        await LoadStudents();
        await DisplayAlert("Thành công", "Đã thêm học sinh", "OK");
    }

    private async Task LoadStudents()
    {
        var students = await _dbHelper.GetAllStudentsAsync();
        // TODO: Gán vào CollectionView nếu cần hiển thị danh sách
        // Ví dụ:
        // StudentCollectionView.ItemsSource = students;
    }

    private void SubjectPlaceholder_Tapped(object sender, EventArgs e)
    {
        SubjectPicker.Focus(); // Mở Picker dropdown
        SubjectPlaceholder.IsVisible = false;
    }

    private void SubjectPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        SubjectPlaceholder.IsVisible = SubjectPicker.SelectedIndex < 0;
    }

    private void OnMenuButtonClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }
}
