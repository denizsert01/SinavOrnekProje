using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrnekProje.Models.Entities;

namespace OrnekProje.Models.Configurations
{
    public class Category_CFG : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { Id = 1, Name = "Roman" },
                new Category { Id = 2, Name = "Bilim Kurgu" },
                new Category { Id = 3, Name = "Kişisel Gelişim" }

                );
        }
    }
}
