using EmployeeApi.Data;
using EmployeeApi.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;
        public EmployeesController(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        [Authorize(Roles = "Reader")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeesAsync()
        {
            var employees = await _repo.GetEmployeesAsync();
            return Ok(employees);
        }

        [Authorize(Roles = "Reader")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeByIdAsync(int id)
        {
            Employee? employee = await _repo.GetEmployeeByIdAsync(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> InsertEmployeeAsync(Employee employee)
        {
            await _repo.InsertEmployeeAsync(employee);
            return Created("Insert Employee",
            new {
                message = "Employee Inserted",
                employee
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> RemoveEmployeeAsync(int id)
        {
            var employee = await _repo.RemoveEmployeeAsync(id);
            if (employee == null)
                return NotFound();
            return Ok(new
            {
                message = "Remove employee",
                data = employee
            });
        }

        [HttpPut]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateEmployeeAsync(Employee employee)
        {
            var updateEmployee = await _repo.UpdateEmployeeAsync(employee);
            if (updateEmployee == null)
                return NotFound();
            return Ok(new
            {
                message = "Update employee",
                employee
            });
        }
    }
}
