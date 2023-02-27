using BlazorAppDemo.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorAppDemo.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Email)
            .IsRequired();

        builder.HasIndex(x => x.Email)
            .IsUnique();
        
        builder.Property(x => x.PasswordHash)
            .IsRequired();
        
        builder.Property(x => x.PasswordSalt)
            .IsRequired();
        
        builder.Property(x => x.DateCreated)
            .IsRequired();
    }
}