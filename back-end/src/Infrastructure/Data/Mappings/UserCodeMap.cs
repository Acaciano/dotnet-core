using Domain.Entities;
using Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings
{
    public class UserCodeMap : BaseEntityMap<UserCode>
    {
        public override void Map(EntityTypeBuilder<UserCode> builder)
        {
            builder.ToTable("UserCode");

            builder.Property(c => c.Code).HasColumnName("Code");
            builder.Property(c => c.UserId).HasColumnName("UserId");
            builder.HasOne(p => p.User).WithMany(b => b.UserCodes).HasForeignKey(d => d.UserId);
        }
    }
}