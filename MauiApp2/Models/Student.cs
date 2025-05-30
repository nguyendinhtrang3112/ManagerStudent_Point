using SQLite;

namespace MauiApp2.Models
{
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public double Grade { get; set; }
        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string Class { get; set; }

        [NotNull]
        public DateTime DateOfBirth { get; set; }

        // Liên kết đến môn học chính (nếu cần)
        public int SubjectId { get; set; }

        // Liên kết đến tài khoản người dùng
        public int? UserId { get; set; }

        [Ignore]
        public string SubjectName { get; set; } = string.Empty;

        [Ignore]
        public int RowIndex { get; set; }

        public Student()
        {
            Name = string.Empty;
            Class = string.Empty;
            DateOfBirth = DateTime.MinValue;
        }

        public Student(string name, string className, DateTime dob, int subjectId, int? userId = null, double grade = 0)
        {
            Name = name;
            Class = className;
            DateOfBirth = dob;
            Grade = grade;
            SubjectId = subjectId;
            UserId = userId;
        }
    }
}
