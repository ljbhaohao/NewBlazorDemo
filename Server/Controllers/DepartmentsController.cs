using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewBlazorDemo.Server.Interface;
using NewBlazorDemo.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBlazorDemo.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetDepartments()
        {
            try
            {
                var departments = await _departmentRepository.GetDepartments();
                return Ok(departments);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetDepartment(int id)
        {
            try
            {
                var department = await _departmentRepository.GetDepartment(id);
                if (department==null)
                {
                    return NotFound($"Id为{id}的部门未找到");
                }
                return Ok(department);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

    }
}
