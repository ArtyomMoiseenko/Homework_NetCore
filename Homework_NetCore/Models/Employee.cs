using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Homework_NetCore.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
    }
}
