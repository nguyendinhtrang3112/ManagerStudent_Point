using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MauiApp2.Models;

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
            _database.CreateTableAsync<Subject>().Wait();
            _database.CreateTableAsync<User>().Wait(); // 👈 Thêm bảng User
        }

        // ------------------ STUDENT ------------------
        public Task<int> AddStudentAsync(Student student)
        {
            return _database.InsertAsync(student);
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            var students = await _database.Table<Student>().ToListAsync();
            var subjects = await _database.Table<Subject>().ToListAsync();

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

        // ------------------ SUBJECT ------------------
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

        // ------------------ USER ------------------
        public Task<int> AddUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            return _database.Table<User>()
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        public Task<int> UpdateUserAsync(User user)
        {
            return _database.UpdateAsync(user);
        }

        public Task<int> DeleteUserAsync(User user)
        {
            return _database.DeleteAsync(user);
        }
    }
}
