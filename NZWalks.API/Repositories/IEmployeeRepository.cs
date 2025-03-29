using EmployeeApi.Data;

namespace EmployeeApi.Repo
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetEmployeesAsync();
        public Task<Employee> GetEmployeeByIdAsync(int id);
        public Task InsertEmployeeAsync(Employee employee);
        public Task<Employee> RemoveEmployeeAsync(int id);
        public Task<Employee> UpdateEmployeeAsync(Employee employee);
    }
}
