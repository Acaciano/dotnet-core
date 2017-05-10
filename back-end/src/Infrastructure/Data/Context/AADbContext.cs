using System.IO;
using Data;
using Domain.Entities;
using Infrastructure.CrossCutting.Configuration;
using Infrastructure.Data.Extensions;
using Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data.Context
{
    public class AADbContext : DbContext
    {
        public AADbContext(DbContextOptions<AADbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User {get;set;}
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ForSqlServerUseIdentityColumns();
            builder.AddConfiguration(new UserMap());

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Configuration.ConnectionString);
        }
    }
}
