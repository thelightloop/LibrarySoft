using Library.Database.Entities;

namespace Library.Common.DTO
{
    public class IssueBookDto
    {
        public int Id { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
