using BlazorAppDemo.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorAppDemo.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        #region seed Categories

        builder.HasData(
            new Category
            {
                Id = 1,
                Name = "Books",
                Url = "books"
            },
            new Category
            {
                Id = 2,
                Name = "Movies",
                Url = "movies"
            },
            new Category
            {
                Id = 3,
                Name = "Video Games",
                Url = "video-games"
            });

        #endregion

        builder.Property(x => x.Name)
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.Url)
            .IsRequired();

        builder.HasIndex(x => x.Url)
            .IsUnique();

        builder.HasMany(c => c.Products)
            .WithOne(e => e.Category);
    }
}