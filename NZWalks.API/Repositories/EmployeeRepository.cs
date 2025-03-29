using EmployeeApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace EmployeeApi.Repo
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApiDbContext _context;

        public EmployeeRepository(ApiDbContext context)
        {
            _context = context;
        }

        private async Task<Employee> FindEmployeeByIdAsync(int id)
        {
            Employee? employee = await _context.Employees.FindAsync(id);
            return employee!;
        }

        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            var employees = await _context.Employees.ToListAsync();
            return employees;
        }

        [HttpGet("{id}")]
        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            Employee? employee = await FindEmployeeByIdAsync(id);
            return employee;
           
        }

        [HttpPost]
        public async Task InsertEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        [HttpDelete("id")]
        public async Task<Employee> RemoveEmployeeAsync(int id)
        {
            var employee = await FindEmployeeByIdAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            return employee;
        }

        [HttpPut]
        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            Employee? updateEmployee = await FindEmployeeByIdAsync(employee.Id);
            if (updateEmployee != null)
            {
                updateEmployee.Name = employee.Name;
                updateEmployee.Age = employee.Age;
                _context.Employees.Update(updateEmployee);
                await _context.SaveChangesAsync();
            }
            return updateEmployee!;
        }
    }
}
