using EmployeeApi.Data;
using EmployeeApi.Repo;
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

        [HttpGet]
        public async Task<IActionResult> GetEmployeesAsync()
        {
            var employees = await _repo.GetEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeByIdAsync(int id)
        {
            Employee? employee = await _repo.GetEmployeeByIdAsync(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
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
