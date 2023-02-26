using BlazorAppDemo.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorAppDemo.Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(x => x.City)
            .IsRequired();
        
        builder.Property(x => x.Country)
            .IsRequired();
        
        builder.Property(x => x.State)
            .IsRequired();
        
        builder.Property(x => x.Street)
            .IsRequired();
        
        builder.Property(x => x.FirstName)
            .IsRequired();
        
        builder.Property(x => x.LastName)
            .IsRequired();
        
        builder.Property(x => x.Zip)
            .IsRequired();
    }
}