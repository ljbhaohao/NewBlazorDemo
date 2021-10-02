using Microsoft.EntityFrameworkCore;
using NewBlazorDemo.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBlazorDemo.Server.Data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region  部门的种子数据
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, DepartmentName = "IT" });
            modelBuilder.Entity<Department>().HasData(
              new Department { DepartmentId = 2, DepartmentName = "HR" });
            modelBuilder.Entity<Department>().HasData(
               new Department { DepartmentId = 3, DepartmentName = "Admin" });
            modelBuilder.Entity<Department>().HasData(
              new Department { DepartmentId = 4, DepartmentName = "Payroll" });
            #endregion
            #region  员工的种子数据
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeId = 1,
                    FirstName = "Sam",
                    LastName = "Galloway",
                    Email = "Sam@163.com",
                    DateOfBirth = new DateTime(1982 - 9 - 8),
                    DepartmentId = 1,
                    Gender = Gender.Female,
                    PhotoPath = "images/Sam.jpg"
                });
            modelBuilder.Entity<Employee>().HasData(
               new Employee
               {
                   EmployeeId = 2,
                   FirstName = "Tom",
                   LastName = "Smith",
                   Email = "Tom@163.com",
                   DateOfBirth = new DateTime(1965 - 09 - 12),
                   DepartmentId = 1,
                   Gender = Gender.Male,
                   PhotoPath = "images/Tom.jpg"
               });
            modelBuilder.Entity<Employee>().HasData(
               new Employee
               {
                   EmployeeId = 3,
                   FirstName = "Mary",
                   LastName = "Galloway",
                   Email = "Mary@163.com",
                   DateOfBirth = new DateTime(1990 - 6 - 8),
                   DepartmentId = 1,
                   Gender = Gender.Female,
                   PhotoPath = "images/Mary.jpg"
               });
            modelBuilder.Entity<Employee>().HasData(
             new Employee
             {
                 EmployeeId = 4,
                 FirstName = "Tim",
                 LastName = "King",
                 Email = "Tim@163.com",
                 DateOfBirth = new DateTime(1985, 8, 2),
                 DepartmentId = 1,
                 Gender = Gender.Male,
                 PhotoPath = "images/Tim.jpg"
             });
            #endregion
            //modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
