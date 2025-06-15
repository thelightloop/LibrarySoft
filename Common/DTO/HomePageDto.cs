namespace Library.Common.DTO
{
    public class HomePageDto
    {
        public IEnumerable<BookDto> BookDto { get; set; }

        public IEnumerable<MemberDto> MembersDto { get; set; }

    }
}
