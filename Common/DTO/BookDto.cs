using System.ComponentModel.DataAnnotations;

namespace Library.Common.DTO
{
    public class BookDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required(ErrorMessage = "Title is required")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-\',\.]{1,100}$", ErrorMessage = "Title contains invalid characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [RegularExpression(@"^[a-zA-Z\s\-\',\.]{1,100}$", ErrorMessage = "Author contains invalid characters.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [RegularExpression(@"^[a-zA-Z\s\-\',\.]{1,100}$", ErrorMessage = "Category contains invalid characters.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        [RegularExpression(@"^(97(8|9))?[\-\s]?\d{1,5}[\-\s]?\d{1,7}[\-\s]?\d{1,7}[\-\s]?(\d|X)$", ErrorMessage = "ISBN format is invalid.")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Total Copies is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Total Copies must be at least 1")]
        public int? TotalCopies { get; set; }

        public int? AvailableCopies { get; set; }

    }
}