namespace Library.Database.Entities
{
    public class IssueBook : BaseEntity
    {

        public string MemberId { get; set; }
        public Member Member { get; set; }

        public string BookId { get; set; }
        public Book Book { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
