namespace OrnekProje.Models.ViewModels
{
    public class BookDetails_VM
    {
        public int Id { get; set; } // Details.cshtml için
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Summary { get; set; }
        public int PageCount { get; set; }
        public string OwnerEmail { get; set; }
        public int OwnerId { get; set; } // Kitap detayında kullanılıyor
        public List<string> Categories { get; set; }
    }
}
