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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Composit Key
            modelBuilder.Entity<EmpDepTbl>().HasKey(x => new { x.DepartmentId, x.EmployeeId });

            modelBuilder.Entity<Employee>(entity =>
            {
                //entity.HasOne(x=>x.Department.DepartmentId==departments.)
                //.WithMany(a=>a.)

            });
        }
    }
}
