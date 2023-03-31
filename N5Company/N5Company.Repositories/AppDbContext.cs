using Microsoft.EntityFrameworkCore;
using N5Company.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace N5Company.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {         
        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Permission>().HasData
           (
               new Permission { Id = 1, EmployeeForename = "Forename 1", EmployeeSurname = "Surname 1", PermissionDate = new DateTime(), PermissionTypeId = 1 },
               new Permission { Id = 2, EmployeeForename = "Forename 2", EmployeeSurname = "Surname 2", PermissionDate = new DateTime(), PermissionTypeId = 2 },
               new Permission { Id = 3, EmployeeForename = "Forename 3", EmployeeSurname = "Surname 3", PermissionDate = new DateTime(), PermissionTypeId = 3 }
           );

            modelBuilder.Entity<PermissionType>().HasData
           (
               new PermissionType { Id = 1, Description = "Description 1" },
               new PermissionType { Id = 2, Description = "Description 2" },
               new PermissionType { Id = 3, Description = "Description 3" }
           );
        }
    }
}
