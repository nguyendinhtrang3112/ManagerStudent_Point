using MauiApp2.Models;
using MauiApp2.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MauiApp2;

public partial class StudentListPage : ContentPage
{
    private DatabaseHelper _dbHelper;
    private List<Student> allStudents = new(); // Dùng để lọc tìm kiếm

    public StudentListPage()
    {
        InitializeComponent();
        _dbHelper = new DatabaseHelper();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadStudents();
    }

    private async Task LoadStudents()
    {
        var students = await _dbHelper.GetAllStudentsAsync();
        var subjects = await _dbHelper.GetAllSubjectsAsync();

        for (int i = 0; i < students.Count; i++)
        {
            var student = students[i];
            var subject = subjects.FirstOrDefault(s => s.Id == student.SubjectId);
            student.SubjectName = subject?.Name ?? "(Không rõ)";
            student.RowIndex = i; // ✅ Cập nhật chỉ số dòng
        }

        allStudents = students;
        StudentList.ItemsSource = allStudents;
    }


    private async void OnDeleteStudentClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var student = button?.CommandParameter as Student;

        if (student != null)
        {
            bool confirm = await DisplayAlert("Xác nhận", $"Bạn có muốn xoá {student.Name}?", "Xoá", "Huỷ");

            if (confirm)
            {
                await _dbHelper.DeleteStudentAsync(student);
                await LoadStudents();
            }
        }
    }

    private async void OnEditStudentClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var student = button?.CommandParameter as Student;

        if (student != null)
        {
            string newName = await DisplayPromptAsync("Sửa tên", "Nhập tên mới:", initialValue: student.Name);
            if (string.IsNullOrWhiteSpace(newName)) return;

            string newGradeStr = await DisplayPromptAsync("Sửa điểm", "Nhập điểm mới:", initialValue: student.Grade.ToString());
            if (!double.TryParse(newGradeStr, out double newGrade)) return;

            string newClass = await DisplayPromptAsync("Sửa lớp", "Nhập lớp mới:", initialValue: student.Class);
            if (string.IsNullOrWhiteSpace(newClass)) return;

            var subjects = await _dbHelper.GetAllSubjectsAsync();
            var subjectNames = subjects.Select(s => s.Name).ToArray();

            string selectedSubject = await DisplayActionSheet("Chọn môn học", "Huỷ", null, subjectNames);
            if (selectedSubject == null || selectedSubject == "Huỷ") return;

            var chosenSubject = subjects.FirstOrDefault(s => s.Name == selectedSubject);
            if (chosenSubject == null) return;

            student.Name = newName;
            student.Grade = newGrade;
            student.Class = newClass;
            student.SubjectId = chosenSubject.Id;
            student.SubjectName = chosenSubject.Name;

            await _dbHelper.UpdateStudentAsync(student);
            await LoadStudents();
        }
    }

    private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        var keyword = e.NewTextValue?.ToLower() ?? "";

        var filtered = allStudents
            .Where(s => s.Name.ToLower().Contains(keyword))
            .Select((s, index) => new Student
            {
                Id = s.Id,
                Name = s.Name,
                Grade = s.Grade,
                Class = s.Class,
                DateOfBirth = s.DateOfBirth,
                SubjectId = s.SubjectId,
                SubjectName = s.SubjectName,
                RowIndex = index // ✅ Gán lại chỉ số
            })
            .ToList();

        StudentList.ItemsSource = filtered;
    }

}
