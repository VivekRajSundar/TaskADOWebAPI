using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskADOWebAPI.Models
{
    public class Project
    {
        public Project()
        {
            this.Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string ProjectName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}