using System.ComponentModel.DataAnnotations;

namespace OrnekProje.Models.ViewModels
{
    public class BookList_VM
    {
        public int Id { get; set; }

        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }

        [Display(Name = "Özet")]
        public string Summary { get; set; }

        [Display(Name = "Sayfa Sayısı")]
        public int PageCount { get; set; }

        [Display(Name = "Ekleyen")]
        public string OwnerFullName { get; set; }
        public int OwnerId { get; set; } // Kitap listelemede kullanılıyor

        [Display(Name = "Kategoriler")]
        public List<string> Categories { get; set; }
    }
}
