namespace Library.Database.Entities
{
    public class Member : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string MembershipType { get; set; }
        public DateTime MembershipStartDate { get; set; }

        public List<IssueBook> IssueBooks { get; set; }
    }
}
