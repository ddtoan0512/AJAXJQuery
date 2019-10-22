using AJAXTable.Data.Models;
using System.Data.Entity;

namespace AJAXTable.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext() : base("EmployeeConnectionString") {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
