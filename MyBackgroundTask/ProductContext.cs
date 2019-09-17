using Microsoft.EntityFrameworkCore;
using MyBackgroundTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyBackgroundTask
{
    public class ProductContext: DbContext 
    {
        public ProductContext(DbContextOptions<ProductContext>options):base(options)
        {

        }

        public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Product>().ToTable("product");
        }
    }
}
