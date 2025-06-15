namespace Library.Database.Entities
{
    public class ReturnBook : BaseEntity
    {

        public string IssueBookId { get; set; }
        public IssueBook IssueBook { get; set; }

        public DateTime ReturnDate { get; set; }
    }
}
