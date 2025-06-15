namespace Library.Database.Entities
{
    public class BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public string ? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string ? UpdatedBy { get; set; }
    }
}
