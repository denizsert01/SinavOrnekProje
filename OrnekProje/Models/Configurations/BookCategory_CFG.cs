using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrnekProje.Models.Entities;

namespace OrnekProje.Models.Configurations
{
    public class BookCategory_CFG : IEntityTypeConfiguration<BookCategory>
    {
        
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.HasKey(x => new { x.BookId, x.CategoryId });

            builder.HasOne(x => x.Book)
                .WithMany(b => b.BookCategories)
                .HasForeignKey(x => x.BookId);

            builder.HasOne(x => x.Category)
                .WithMany(c => c.BookCategories)
                .HasForeignKey(x => x.CategoryId);
        }
    }
    
}
