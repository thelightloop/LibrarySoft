namespace Library.Database.Entities
{
    public class Fine : BaseEntity
    {

        public string ReturnBookId { get; set; }
        public ReturnBook ReturnBook { get; set; }

        public decimal Amount { get; set; }
        public DateTime IssuedOn { get; set; }
    }
}
