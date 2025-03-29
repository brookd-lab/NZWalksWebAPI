using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace EmployeeApi.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
    }
}
