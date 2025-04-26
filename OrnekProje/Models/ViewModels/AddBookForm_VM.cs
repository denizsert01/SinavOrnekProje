using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OrnekProje.Models.ViewModels
{
    public class AddBookForm_VM
    {
        public AddBook_VM Book { get; set; }

        // Kategori Idlerini alır
        [Required(ErrorMessage = "En az bir kategori seçmelisiniz.")]
        public List<int> SelectCategoryIds { get; set; } = new();

        // Dropdown'da kullanılır
        public SelectList? Categories { get; set; }
    }
}
