using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Homework_NetCore.Models.db
{
    public class DbInitializer
    {
        public static void Initialize(CompanyContext context)
        {
            context.Database.Migrate();

            if (context.Employees.Any() && context.Roles.Any() && context.Projects.Any())
            {
                return;
            }

            var roles = new List<Role>()
            {
                new Role { Name = "Project Manager"},
                new Role { Name = "Developer"},
                new Role { Name = "QA"}
            };
            foreach (var item in roles)
            {
                context.Roles.Add(item);
            }
            context.SaveChanges();

            var projects = new List<Project>()
            {
                new Project { Name = "Zoo", Description = "Description about zoo" },
                new Project { Name = "WebGovernment", Description = "Description about government" },
                new Project { Name = "Advertising", Description = "Description about advertising" }
            };
            foreach (var item in projects)
            {
                context.Projects.Add(item);
            }
            context.SaveChanges();

            var employees = new List<Employee>()
            {
                new Employee { FirstName = "Jack", LastName = "Nickson", Email = "jack@gmail.com", Phone = "494949", ProjectId = 1, RoleId = 1 },
                new Employee { FirstName = "Nick", LastName = "Green", Email = "nick@gmail.com", Phone = "545454", ProjectId = 2, RoleId = 1 },
                new Employee { FirstName = "Rex", LastName = "Brown", Email = "rex@gmail.com", Phone = "626262", ProjectId = 2, RoleId = 2 },
                new Employee { FirstName = "Trever", LastName = "Dilt", Email = "trever@gmail.com", Phone = "121212", ProjectId = 1, RoleId = 1 }
            };
            foreach (var item in employees)
            {
                context.Employees.Add(item);
            }
            context.SaveChanges();
        }
    }
}
