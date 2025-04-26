using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrnekProje.Models.Entities;

namespace OrnekProje.Models.Configurations
{
    public class Book_CFG : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Price).HasColumnType("money");
        }
    }
}
