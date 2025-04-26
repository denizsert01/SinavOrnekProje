using System.ComponentModel.DataAnnotations;

namespace OrnekProje.Models.ViewModels
{
    public class AddBook_VM
    {
        public int Id { get; set; } // Edit kısmında ulaşabilmek için
        [Required(ErrorMessage = "Başlık zorunludur.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Başlık 3-100 karakter arasında olmalıdır.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Fiyat zorunludur.")]
        [Range(1, 10000, ErrorMessage = "Fiyat 1 ile 10,000 arasında olmalıdır.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Özet zorunludur.")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Özet 10-500 karakter arasında olmalıdır.")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Sayfa sayısı zorunludur.")]
        [Range(1, 10000, ErrorMessage = "Sayfa sayısı 1 ile 10,000 arasında olmalıdır.")]
        public int PageCount { get; set; }


    }
}
