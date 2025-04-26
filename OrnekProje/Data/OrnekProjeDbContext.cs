using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrnekProje.Models.Entities;
using System.Reflection;
using System.Reflection.Emit;

namespace OrnekProje.Data
{
    public class OrnekProjeDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public OrnekProjeDbContext(DbContextOptions options) : base(options)
        {
        }

        protected OrnekProjeDbContext()
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // BookCategor'nin primary key'i burada verildi, böylece aynı kitap ve kategoriyle veri tekrarı karışıklığın önüne geçilmiş oldu
            builder.Entity<BookCategory>()
                .HasKey(bc => new { bc.BookId, bc.CategoryId });

            builder.Entity<BookCategory>()
    .HasOne(bc => bc.Book)
    .WithMany(b => b.BookCategories)
    .HasForeignKey(bc => bc.BookId);

            builder.Entity<BookCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BookCategories)
                .HasForeignKey(bc => bc.CategoryId);

            // Bütün CFG dosyalarına uygulaması için
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        
    }
}
