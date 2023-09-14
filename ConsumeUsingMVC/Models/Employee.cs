using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsumeUsingMVC.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public decimal? Salary { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public DateTime? DateOfJoining { get; set; }
        
        public DateTime? DateOfResigning { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [Required]
        public bool? IsActive { get; set; }

        public int? ManagerId { get; set; }
        [Required]
        public int? DeptId { get; set; }
        public int? ProjectId { get; set; }
        [Required]
        public int? RoleId { get; set; }

    }
}