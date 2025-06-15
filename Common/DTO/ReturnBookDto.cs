using Library.Database.Entities;

namespace Library.Common.DTO
{
    public class ReturnBookDto
    {
        public int Id { get; set; }

        public int IssueBookId { get; set; }
        public IssueBook IssueBook { get; set; }

        public DateTime ReturnDate { get; set; }
    }
}
