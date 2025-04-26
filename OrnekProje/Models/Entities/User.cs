using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OrnekProje.Models.Entities
{
    /// <summary>
    ///  Id verilmesine gerek yok çünkü zaten <int> diyerek otomatik belirtmiş olundu.
    /// </summary>
    public class User : IdentityUser<int>
    {
        [MaxLength(50)] // Database constraint
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        // Navigation property
        public ICollection<Book> Books { get; set; }
    }
}
