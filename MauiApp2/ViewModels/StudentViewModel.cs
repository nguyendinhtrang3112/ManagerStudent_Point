using MauiApp2.Models;
using MauiApp2.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Linq;

namespace MauiApp2.ViewModels
{
    public class StudentDisplay
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public double Grade { get; set; }
        public string AlternatingColor { get; set; } = "#FFFFFF";
    }

    public class StudentViewModel : BindableObject
    {
        private readonly DatabaseHelper _database;

        public ObservableCollection<StudentDisplay> FilteredStudents { get; set; } = new();
        private List<StudentDisplay> _allStudents = new();

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        public StudentViewModel()
        {
            _database = new DatabaseHelper();
        }

        public async Task LoadStudentsAsync()
        {
            Console.WriteLine("✅ LoadStudentsAsync() được gọi");

            var students = await _database.GetAllStudentsAsync();
            Console.WriteLine($"📊 Số lượng học sinh trong database: {students.Count}");

            int index = 0;
            _allStudents.Clear();

            foreach (var student in students)
            {
                Console.WriteLine($"🔹 Đọc học sinh: {student.Name}, Lớp: {student.Class}, Môn ID: {student.SubjectId}");

                var subject = await _database.GetSubjectByIdAsync(student.SubjectId);
                var subjectName = subject?.Name ?? "(Không rõ)";

                var displayStudent = new StudentDisplay
                {
                    Id = student.Id,
                    Name = student.Name,
                    Class = student.Class,
                    Grade = student.Grade,
                    SubjectName = subjectName,
                    AlternatingColor = (index++ % 2 == 0) ? "#FFFFFF" : "#F2F6FF"
                };

                _allStudents.Add(displayStudent);
            }

            Console.WriteLine($"✅ Tổng học sinh sau xử lý: {_allStudents.Count}");

            ApplyFilter();

            Console.WriteLine($"📌 Tổng số học sinh được hiển thị sau lọc: {FilteredStudents.Count}");
        }

        private void ApplyFilter()
        {
            FilteredStudents.Clear();

            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? _allStudents
                : _allStudents.Where(s => s.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();

            foreach (var s in filtered)
                FilteredStudents.Add(s);
        }
    }
}
