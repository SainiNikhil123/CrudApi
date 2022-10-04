using EmployeeCrud.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrud.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Designation> designations { get; set; }
        public DbSet<EmpDepTbl> empDepTbls { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoleTable> userRoleTables { get; set; }
        public DbSet<ClaimsTable> ClaimTable { get; set; }
        public DbSet<UserClaimTable> UserClaimTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Composite Key
            modelBuilder.Entity<EmpDepTbl>().HasKey(x => new { x.DepartmentId, x.EmployeeId });

            modelBuilder.Entity<UserRoleTable>().HasKey(x => new { x.RoleId, x.UserId });

            modelBuilder.Entity<UserClaimTable>().HasKey(x => new { x.UserId, x.ClaimId });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
