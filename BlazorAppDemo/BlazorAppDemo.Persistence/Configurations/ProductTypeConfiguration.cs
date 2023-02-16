using BlazorAppDemo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorAppDemo.Persistence.Configurations;

public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        #region seed ProductType

        builder.HasData(
            new ProductType {Id = 1, Name = "Default"},
            new ProductType {Id = 2, Name = "Paperback"},
            new ProductType {Id = 3, Name = "E-Book"},
            new ProductType {Id = 4, Name = "Audiobook"},
            new ProductType {Id = 5, Name = "Stream"},
            new ProductType {Id = 6, Name = "Blu-ray"},
            new ProductType {Id = 7, Name = "VHS"},
            new ProductType {Id = 8, Name = "PC"},
            new ProductType {Id = 9, Name = "PlayStation"},
            new ProductType {Id = 10, Name = "Xbox"}
        );

        #endregion

        builder.Property(x => x.Name)
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}