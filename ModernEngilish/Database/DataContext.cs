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
        public DbSet<Career> Careers { get; set; }
        public DbSet<Graduate> Graduates { get; set; }

        public DbSet<StudentSay> StudentSays { get; set; }
        public DbSet<User> Users {get; set;}
        public DbSet<Role> Roles {get; set;}
        public DbSet<OnlineCourse> OnlineCourses{get; set;}
        public DbSet<Contact> Contacts {get; set;}

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly<Program>();

            modelBuilder.Entity<User>().
            HasData(
            new User{Id = 2 ,Mail = "admin@gmail.com",Password ="admin",RoleID = 1});

            modelBuilder.Entity<Role>().HasData(new Role{Id = 1 ,RoleName = "Admin"});
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
