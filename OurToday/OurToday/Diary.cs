using SQLite;

namespace OurToday
{
    public class Diary
    {
        public Diary() { }
        public Diary(string Title, string Content)
        {
            this.Title = Title;
            this.Content = Content;
        }

        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
