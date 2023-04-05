using Microsoft.EntityFrameworkCore;
using ProductAPI.Infrastructure.Entity;
using ProductAPI.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Account.API.Infrastructure
{
    public sealed class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
          : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
        }

        public DbSet<Product> Products { get; set; } = default!;
    }
}
