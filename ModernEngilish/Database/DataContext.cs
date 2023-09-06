using Azure;
using Microsoft.EntityFrameworkCore;
using ModernEngilish.Database.Models;
using ModernEngilish.Extensions;
using System.Data;
using System.Drawing;
using System.Reflection.Metadata;

namespace ModernEngilish.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
       : base(options)
        {

        }

        public DbSet<About> Abouts { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly<Program>();
        }
    }
}
