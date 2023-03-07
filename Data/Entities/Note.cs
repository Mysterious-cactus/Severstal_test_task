using SQLite;

namespace Data.Entities
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public DateTimeOffset Date { get; set;}
    }
}