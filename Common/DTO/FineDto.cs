using Library.Database.Entities;

namespace Library.Common.DTO
{
    public class FineDto
    {
        public int Id { get; set; }

        public int ReturnBookId { get; set; }
        public ReturnBook ReturnBook { get; set; }

        public decimal Amount { get; set; }
        public DateTime IssuedOn { get; set; }
    }
}
