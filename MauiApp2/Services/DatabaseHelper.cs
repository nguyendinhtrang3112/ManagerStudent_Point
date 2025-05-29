using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            _database.CreateTableAsync<Student>().Wait();
            _database.CreateTableAsync<Subject>().Wait();
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Teacher>().Wait();
        }

        // ------------------ STUDENT ------------------

        public async Task<int> AddStudentAsync(Student student)
        {
            bool exists = await CheckStudentExistsAsync(student);
            if (exists)
                return 0;

            return await _database.InsertAsync(student);
        }

        public async Task<bool> CheckStudentExistsAsync(Student student)
        {
            var existingStudents = await _database.Table<Student>().ToListAsync();
            return existingStudents.Exists(s =>
                s.Name == student.Name &&
                s.Class == student.Class &&
                s.DateOfBirth == student.DateOfBirth &&
                s.SubjectId == student.SubjectId
            );
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

        // ✅ Lấy học sinh theo username (dựa vào UserId)
        public async Task<Student?> GetStudentByUserIdAsync(int userId)
        {
            return await _database.Table<Student>()
                .FirstOrDefaultAsync(s => s.UserId == userId);
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

        public async Task<Subject?> GetSubjectByIdAsync(int id)
        {
            return await _database.Table<Subject>().FirstOrDefaultAsync(s => s.Id == id);
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

        public Task<User> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
            return _database.Table<User>()
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
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

        // ------------------ TEACHER ------------------

        public Task<int> AddTeacherAsync(Teacher teacher)
        {
            return _database.InsertAsync(teacher);
        }

        public Task<List<Teacher>> GetAllTeachersAsync()
        {
            return _database.Table<Teacher>().ToListAsync();
        }

        public Task<int> UpdateTeacherAsync(Teacher teacher)
        {
            return _database.UpdateAsync(teacher);
        }

        public Task<int> DeleteTeacherAsync(Teacher teacher)
        {
            return _database.DeleteAsync(teacher);
        }
    }
}
