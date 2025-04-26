using Microsoft.EntityFrameworkCore;
using OrnekProje.Data;
using OrnekProje.Interfaces;
using OrnekProje.Models.Entities;

namespace OrnekProje.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(OrnekProjeDbContext context) : base(context)
        {
        }

        // Kullanıcıya ait kitapları ve kategorileri alır
        public async Task<IEnumerable<Book>> GetBooksWithCategoriesByUserAsync(int userId)
        {
            return await _dbSet
                .Where(b  => b.UserId == userId)
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .Include(b => b.User)
                .ToListAsync();
        }

        // Tüm kitapları ve kategorileri alır
        public async Task<IEnumerable<Book>> GetBooksWithCategoriesAsync()
        {
            return await _dbSet
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .Include(b => b.User)
                .ToListAsync();
        }

        // Id'ye göre tek kitap alır
        public async Task<Book> GetBookWithCategoriesAsync(int id)
        {
            return await _dbSet
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
