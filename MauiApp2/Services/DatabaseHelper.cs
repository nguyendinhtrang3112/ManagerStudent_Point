using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MauiApp2.Models; // Đảm bảo bạn có thư mục Models chứa class Student và Subject

namespace MauiApp2.Services
{
    public class DatabaseHelper
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseHelper()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "studentdb.db3");
            _database = new SQLiteAsyncConnection(dbPath);

            // Tạo bảng nếu chưa có
            _database.CreateTableAsync<Student>().Wait();
            _database.CreateTableAsync<Subject>().Wait(); // 👈 Thêm bảng Subject
        }

        // --------- STUDENT ---------
        public Task<int> AddStudentAsync(Student student)
        {
            return _database.InsertAsync(student);
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            var students = await _database.Table<Student>().ToListAsync();
            var subjects = await _database.Table<Subject>().ToListAsync();

            // Gán SubjectName cho mỗi học sinh dựa trên SubjectId
            foreach (var student in students)
            {
                var subject = subjects.Find(s => s.Id == student.SubjectId);
                student.SubjectName = subject?.Name ?? "(Không rõ)";
            }

            return students;
        }

        public Task<int> UpdateStudentAsync(Student student)
        {
            return _database.UpdateAsync(student);
        }

        public Task<int> DeleteStudentAsync(Student student)
        {
            return _database.DeleteAsync(student);
        }

        // --------- SUBJECT ---------
        public Task<int> AddSubjectAsync(Subject subject)
        {
            return _database.InsertAsync(subject);
        }

        public Task<List<Subject>> GetAllSubjectsAsync()
        {
            return _database.Table<Subject>().ToListAsync();
        }

        public Task<int> UpdateSubjectAsync(Subject subject)
        {
            return _database.UpdateAsync(subject);
        }

        public Task<int> DeleteSubjectAsync(Subject subject)
        {
            return _database.DeleteAsync(subject);
        }
    }
}
