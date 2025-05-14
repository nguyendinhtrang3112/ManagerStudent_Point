using SQLite;

namespace MauiApp2.Models
{
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public double Grade { get; set; }
        public string Class { get; set; }
        public DateTime DateOfBirth { get; set; }

        // Liên kết với bảng Subject
        public int SubjectId { get; set; }

        [Ignore] // Không lưu vào DB
        public string SubjectName { get; set; }

        [Ignore] // Không lưu vào DB, dùng để tính màu xen kẽ
        public int RowIndex { get; set; }

        // Constructor mặc định
        public Student()
        {
            Name = string.Empty;
            Class = string.Empty;
            DateOfBirth = DateTime.MinValue;
            SubjectId = 0;
            SubjectName = string.Empty;
            RowIndex = 0;
        }

        // Constructor có tham số
        public Student(string name, double grade, string className, DateTime dateOfBirth, int subjectId, string subjectName)
        {
            Name = name;
            Grade = grade;
            Class = className;
            DateOfBirth = dateOfBirth;
            SubjectId = subjectId;
            SubjectName = subjectName;
            RowIndex = 0;
        }
    }
}
