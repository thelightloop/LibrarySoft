namespace Library.Database.Entities
{
    public class SearchBook : BaseEntity
    {
        public string Keyword { get; set; }
        public DateTime SearchedOn { get; set; }
    }
}
