using Domain.Entities;
using Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings
{
    public class UserMap : BaseEntityMap<User>
    {
        public override void Map(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.Property(c => c.Name).HasColumnName("Name").HasMaxLength(255);
            builder.Property(c => c.Email).HasColumnName("Email").HasMaxLength(150);
            builder.Property(c => c.Password).HasColumnName("Password").HasMaxLength(255);
        }
    }
}