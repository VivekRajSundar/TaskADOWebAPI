using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskADOWebAPI.Models
{
    public class Role
    {
        public Role()
        {
            this.Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}