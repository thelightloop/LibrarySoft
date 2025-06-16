using Library.Database.Entities;

namespace Library.Common.DTO
{
    public class IssueBookDto
    {
        public int Id { get; set; }

        public string MemberId { get; set; }
        public Member Member { get; set; }

        public string BookId { get; set; }
        public Book Book { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
