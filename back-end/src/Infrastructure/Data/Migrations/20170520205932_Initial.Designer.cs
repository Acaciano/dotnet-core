using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Infrastructure.Data.Context;

namespace Data.Migrations
{
    [DbContext(typeof(AADbContext))]
    [Migration("20170520205932_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("Email")
                        .HasColumnName("Email")
                        .HasMaxLength(150);

                    b.Property<string>("Name")
                        .HasColumnName("Name")
                        .HasMaxLength(255);

                    b.Property<string>("Password")
                        .HasColumnName("Password")
                        .HasMaxLength(255);

                    b.Property<DateTime>("RegistrationDate");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Domain.Entities.UserCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<Guid>("Code");

                    b.Property<DateTime?>("DateModified");

                    b.Property<DateTime>("RegistrationDate");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserCode");
                });

            modelBuilder.Entity("Domain.Entities.UserCode", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("UserCodes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
