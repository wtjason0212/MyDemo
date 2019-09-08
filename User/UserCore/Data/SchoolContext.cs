using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserCore.Models;

namespace UserCore.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {

        }

        //public DbSet<Student> Students { get; set; }
        public DbSet<Oldwhite> Oldwhite { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Oldwhite>().ToTable("oldwhite");
        }
    }
}
