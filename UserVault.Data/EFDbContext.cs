using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserVault.Domain.Entities;

namespace UserVault.Data
{
    public class EFDbContext : DbContext
    {
        public EFDbContext()
        {

        }

        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure all DateTime properties globally to use timestamptz
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetColumnType("timestamp with time zone");
                    }
                }
            }
        }
    }
}
