using System.ComponentModel.DataAnnotations;

namespace OrnekProje.Models.Entities
{
    /// <summary>
    ///  CRUD gerektirmediğinden seed edilebilir
    /// </summary>
    public class Category
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<BookCategory> BookCategories { get; set; }
    }
}
