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
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [HttpGet("search")]
        public async Task<ActionResult> serachEmployees(string name,Gender?gender)
        {
            try
            {
                var employees = await _employeeRepository.Search(name,gender);
                if (employees.Any())
                {
                    return Ok(employees);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                var employees = await _employeeRepository.GetEmployees();
                return Ok(employees);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeById(id);
                if (employee==null)
                {
                    return NotFound();
                }
                return Ok(employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Employee>>CreateEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();
                }
                var emp = await _employeeRepository.GetEmployeeByEmali(employee.Email);
                if (emp != null)
                {
                    ModelState.AddModelError("Email","邮箱已被注册");
                    return BadRequest(ModelState);
                }
              var createEmployee= await _employeeRepository.AddEmployee(employee);
                return CreatedAtAction(nameof(GetEmployee),
                    new {id=createEmployee.EmployeeId},createEmployee);
            }
            catch (Exception )
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "创建员工失败");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id,Employee employee)
        {
            try
            {
                if (id!=employee.EmployeeId)
                {
                    return BadRequest("员工不存在");
                }
                var updateEmployee = await _employeeRepository.GetEmployeeById(id);
                if (updateEmployee == null)
                {
                    return NotFound($"员工编号{id}未找到");
                }
               return await _employeeRepository.UpdateEmployee(employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "更新员工信息失败");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var deleteEmp = await _employeeRepository.GetEmployeeById(id);
                if (deleteEmp == null)
                {
                    return NotFound($"员工编号{id}的信息未找到");
                }
                await _employeeRepository.DeleteEmployee(id);
                return Ok($"员工编号{id}的信息已删除");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "员工信息删除失败");
            }
        }
    }
}
