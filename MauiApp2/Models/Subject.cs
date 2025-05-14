
using SQLite;

namespace MauiApp2.Models
{
    public class Subject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }

        // ✅ Constructor không tham số (nếu cần khởi tạo rỗng)
        public Subject()
        {
            Name = string.Empty;
            Code = string.Empty;
        }

        // ✅ Constructor có tham số
        public Subject(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }
}
