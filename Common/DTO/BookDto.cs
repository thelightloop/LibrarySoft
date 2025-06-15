using System.ComponentModel.DataAnnotations;

namespace Library.Common.DTO
{
    public class BookDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Total Copies is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Total Copies must be at least 1")]
        public int? TotalCopies { get; set; }

        public int? AvailableCopies { get; set; }
    }
}