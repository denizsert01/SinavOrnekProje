using OrnekProje.Models.Entities;

namespace OrnekProje.Interfaces
{
    public interface IBookRepository: IGenericRepository<Book>
    {
        // Tüm kitapları ve kategorileri alır
        Task<IEnumerable<Book>> GetBooksWithCategoriesAsync();

        // Kullanıcıya ait kitapları ve kategorileri alır
        Task<IEnumerable<Book>> GetBooksWithCategoriesByUserAsync(int userId);

        // Id'ye göre tek kitap alır
        Task<Book> GetBookWithCategoriesAsync(int id);



    }
}
