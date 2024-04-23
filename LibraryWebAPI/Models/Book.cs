using System.ComponentModel.DataAnnotations;

namespace LibraryWebAPI.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        [Range(1000, 9999, ErrorMessage = "Please enter a valid year")]
        public int Year { get; set; }

        public string Status { get; set; } = "Available"; // Default value for Status
        public string BorrowerName { get; set; } = "";    // Default value for BorrowerName

    }
}
