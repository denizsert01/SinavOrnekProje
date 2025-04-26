using System.ComponentModel.DataAnnotations;

namespace OrnekProje.Models.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [Range(1, 10000)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Summary { get; set; }

        [Required]
        [Range(1, 10000)]
        public int PageCount { get; set; }

        // Foreign key
        public int UserId { get; set; }

        // Navigation properties
        public User User { get; set; } // fullname ve email buradan gelir
        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();

    }
}
