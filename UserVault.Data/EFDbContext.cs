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
    }
}
