using Azure;
using Microsoft.EntityFrameworkCore;
using ModernEngilish.Database.Models;
using ModernEngilish.Database.Models.Common;
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
        public DbSet<EngilishProgram> EngilishPrograms { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<Aged> Ageds { get; set; }
        public DbSet<Partie> Parties { get; set; }
        public DbSet<Gallery> Galleries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly<Program>();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var datas = ChangeTracker.Entries<IAuditable>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedAt = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdateAt = DateTime.UtcNow,
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
