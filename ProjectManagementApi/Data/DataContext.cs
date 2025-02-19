using Microsoft.EntityFrameworkCore;
using ProjectManagementApi.Models;

namespace ProjectManagementApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Companies> Companies { get; set; }
        public DbSet<Doljnosti> Doljnosti { get; set; }
        public DbSet<DoljnostiEmployee> DoljnostiEmployees { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<ProjectEmployees> ProjectEmployees { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Tasks> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-Many: Company → Projects
            modelBuilder.Entity<Companies>()
                .HasMany(c => c.Projects)
                .WithOne(p => p.Companies)
                .HasForeignKey(p => p.CustomerCompanyID);

            // Many-to-Many: Employees ↔ Doljnosti
            modelBuilder.Entity<DoljnostiEmployee>()
                .HasKey(de => new { de.EmployeeID, de.PostID });

            modelBuilder.Entity<DoljnostiEmployee>()
                .HasOne(de => de.Employees)
                .WithMany(e => e.DoljnostiEmployees)
                .HasForeignKey(de => de.EmployeeID);

            modelBuilder.Entity<DoljnostiEmployee>()
                .HasOne(de => de.Doljnosti)
                .WithMany(d => d.DoljnostiEmployee)
                .HasForeignKey(de => de.PostID);

            // Many-to-Many: Employees ↔ Projects
            modelBuilder.Entity<ProjectEmployees>()
                .HasKey(pe => new { pe.ProjectID, pe.EmployeeID });

            modelBuilder.Entity<ProjectEmployees>()
                .HasOne(pe => pe.Employees)
                .WithMany(e => e.ProjectEmployees)
                .HasForeignKey(pe => pe.EmployeeID);

            modelBuilder.Entity<ProjectEmployees>()
                .HasOne(pe => pe.Projects)
                .WithMany(p => p.ProjectEmployees)
                .HasForeignKey(pe => pe.ProjectID);

            // One-to-Many: Employees → Projects (Project Manager)
            modelBuilder.Entity<Projects>()
                .HasOne(p => p.Employees)
                .WithMany(e => e.Projects)
                .HasForeignKey(p => p.ProjectManagerID)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many: Projects → Tasks
            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.Projects)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectID)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many: Employees → Tasks (Author)
            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.Employees)
                .WithMany(e => e.Tasks)
                .HasForeignKey(t => t.AuthorID)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many: Employees → Tasks (Performer)
            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.Employees)
                .WithMany(e => e.Tasks)
                .HasForeignKey(t => t.PerformerID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

