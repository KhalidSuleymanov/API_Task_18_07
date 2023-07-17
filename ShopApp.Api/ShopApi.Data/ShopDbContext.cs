using Microsoft.EntityFrameworkCore;
using ShopApi.Data.Configurations;
using ShopApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApi.Data
{
    public class ShopDbContext:DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BrandConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
