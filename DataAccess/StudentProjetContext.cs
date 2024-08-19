using Microsoft.EntityFrameworkCore;
using ProjetEtudiantBackend.Models;
using StudentBackend.Models;

namespace ProjetEtudiantBackend.Entity
{
    
        public class StudentProjetContext : DbContext
        {
            public DbSet<Person> People { get; set; }
            public DbSet<Course> Courses { get; set; }
            public DbSet<Enrollment> Enrollments { get; set; }
            public DbSet<Assignment> Assignments { get; set; }

            public DbSet<FileAssignment> FileAssignments { get; set; }
            
            public DbSet<Inscription> Inscriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    IConfigurationRoot configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();

                    string connectionString = configuration.GetConnectionString("WebApiDatabase")!;

                    optionsBuilder.UseSqlServer(connectionString);
                }

            }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { //Configuration de la relation entre Course et Inscription 
          modelBuilder.Entity<Inscription>() 
                      .HasOne(i => i.Course) 
                      .WithMany(c => c.Inscriptions) 
                      .HasForeignKey(i => i.CourseId); 
          base.OnModelCreating(modelBuilder); }
        }
    }

