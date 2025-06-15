namespace Library.Common.DTO
{
    public class SearchBookDto
    {

        public int Id { get; set; }
        public string Keyword { get; set; }
        public DateTime SearchedOn { get; set; }
    }
}
