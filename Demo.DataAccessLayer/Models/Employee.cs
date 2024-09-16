using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccessLayer.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int Age { get; set; }
       
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? ImageName { get; set; }
        public bool IsActive { get; set; }

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
