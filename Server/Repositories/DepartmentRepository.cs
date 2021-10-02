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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MyContext _myContext;

        public DepartmentRepository(MyContext myContext)
        {
           _myContext = myContext;
        }
        public async Task<Department> GetDepartment(int departmentId)
        {
            return await _myContext.Departments.FindAsync(departmentId);
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await _myContext.Departments.ToListAsync();
        }
    }
}
