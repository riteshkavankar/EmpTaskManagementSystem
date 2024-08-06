using employeetaskapis.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace employeetaskapis.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<EmployeeTask> Tasks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<EmployeeDocument> Documents { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(e => e.EmployeeId);
            modelBuilder.Entity<EmployeeTask>().HasKey(t => t.TaskId);
            modelBuilder.Entity<Note>().HasKey(n => n.NoteId);
            modelBuilder.Entity<EmployeeDocument>().HasKey(d => d.DocumentId);
            modelBuilder.Entity<User>().HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Employee)
                .WithOne()
                .HasForeignKey<User>(u => u.EmployeeId);

            // Seed Employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, Name = "Albert Joe", Email = "Albert@example.com" },
                new Employee { EmployeeId = 2, Name = "John Snow", Email = "John@example.com" },
                new Employee { EmployeeId = 3, Name = "Mike James", Email = "Mike@example.com" }
            );

            // Seed Tasks
            modelBuilder.Entity<EmployeeTask>().HasData(
                new EmployeeTask { TaskId = 1, Title = "First Task", Description = "Task Description", DueDate = DateTime.Now.AddDays(7), Status = "Open", EmployeeId = 1 }
            );

            // Seed Notes
            modelBuilder.Entity<Note>().HasData(
                new Note { NoteId = 1, Content = "This is a note.", CreatedAt = DateTime.Now, TaskId = 1 }
            );

            // Seed Documents with sample binary data
            modelBuilder.Entity<EmployeeDocument>().HasData(
                new EmployeeDocument
                {
                    DocumentId = 1,
                    FileName = "Document1.txt",
                    FileContent = Convert.FromBase64String("SGVsbG8sIHRoaXMgaXMgdGhlIGNvbnRlbnQgdG8gdGhpcyBk b2N1bWVudC4="), 
                    TaskId = 1
                }
            );

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "admin", Password = "admin", Role = "Admin", EmployeeId = 3 },
                new User { UserId = 2, Username = "manager", Password = "manager", Role = "Manager", EmployeeId = 2 },
                new User { UserId = 3, Username = "employee", Password = "employee", Role = "Employee", EmployeeId = 1 }
            );
        }
    }
}
