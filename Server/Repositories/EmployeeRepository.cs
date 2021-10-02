using Microsoft.EntityFrameworkCore;
using NewBlazorDemo.Server.Data;
using NewBlazorDemo.Server.Interface;
using NewBlazorDemo.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBlazorDemo.Server.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyContext _myContext;
        public EmployeeRepository(MyContext myContext)
        {
            _myContext = myContext;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            if (employee.Department!=null)
            {
                _myContext.Entry(employee.Department).State = EntityState.Unchanged;
            }
            var result = await _myContext.Employees.AddAsync(employee);
            await _myContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task DeleteEmployee(int employeeId)
        {
            var exsitEmp = await GetEmployeeById(employeeId);
            if (exsitEmp != null)
            {
                _myContext.Employees.Remove(exsitEmp);
                await _myContext.SaveChangesAsync();
            }
        }
        public async Task<Employee> GetEmployeeByEmali(string email)
        {
            return await _myContext.Employees.FirstOrDefaultAsync(e => e.Email == email);
        }
        public async Task<Employee> GetEmployeeById(int employeeId)
        {
            var employee = await _myContext.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            return employee;
        }
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _myContext.Employees.ToListAsync();
        }
        public async Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        {
            IQueryable<Employee> employees = _myContext.Employees;
            if (!string.IsNullOrEmpty(name))
            {
                employees = employees.Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name));
            }
            if (gender != null)
            {
                employees = employees.Where(e => e.Gender == gender);
            }
            return await employees.ToListAsync();
        }
        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var existEmp = await GetEmployeeById(employee.EmployeeId);
            if (existEmp != null)
            {
                existEmp.FirstName = employee.FirstName;
                existEmp.LastName = employee.LastName;
                existEmp.Email = employee.Email;
                existEmp.DateOfBirth = employee.DateOfBirth;
                existEmp.Gender = employee.Gender;
                if (employee.DepartmentId != 0)
                {
                    existEmp.DepartmentId = employee.DepartmentId;
                }
                else if (employee.Department != null)
                {
                    existEmp.DepartmentId = employee.Department.DepartmentId;
                }
                existEmp.PhotoPath = employee.PhotoPath;
                await _myContext.SaveChangesAsync();
                return existEmp;

            }
            return null;
        }
    }
}
