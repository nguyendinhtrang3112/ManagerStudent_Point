using SQLite;

namespace MauiApp2.Models
{
    public class Subject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string Code { get; set; }

        public Subject()
        {
            Name = string.Empty;
            Code = string.Empty;
        }

        public Subject(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }
}
