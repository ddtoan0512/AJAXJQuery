namespace AJAXTable.Data.Migrations
{
    using AJAXTable.Data.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AJAXTable.Data.EmployeeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AJAXTable.Data.EmployeeDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            context.Employees.AddOrUpdate(
                new Employee { Name = "Andrew Peters", Salary = 100000, CreatedDate = DateTime.Now, Status = true },
                new Employee { Name = "Brice Labsom", Salary = 200000, CreatedDate = DateTime.Now, Status = true },
                new Employee { Name = "Rowan Miller", Salary = 300000, CreatedDate = DateTime.Now, Status = true },

                new Employee { Name = "Rowan Andrew", Salary = 400000, CreatedDate = DateTime.Now, Status = true },
                new Employee { Name = "Peters Miller", Salary = 500000, CreatedDate = DateTime.Now, Status = true },
                new Employee { Name = "Andrew Brice", Salary = 600000, CreatedDate = DateTime.Now, Status = true },

                new Employee { Name = "Brice Miller", Salary = 700000, CreatedDate = DateTime.Now, Status = true },
                new Employee { Name = "Andrew Peters", Salary = 900000, CreatedDate = DateTime.Now, Status = true },
                new Employee { Name = "Miller Andrew", Salary = 1200000, CreatedDate = DateTime.Now, Status = true },
                new Employee { Name = "Peters Andrew", Salary = 300000, CreatedDate = DateTime.Now, Status = true }

            );

            context.SaveChanges();
        }
    }
}
