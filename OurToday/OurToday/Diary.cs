using SQLite;

namespace OurToday
{
    public class Diary
    {
        private string _content;
        public Diary() { }
        public Diary(string Title, string Content)
        {
            this.Title = Title;
            this.Content= Content;
        }

        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public string Content
        {
            get => _content;
            set => _content = value;
        }
        public string DisplayContent
        {
            get => _content.Length <= 30 ? _content : _content.Substring(0, 30) + "...";
        }
    }
}
