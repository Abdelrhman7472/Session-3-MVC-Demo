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
        [StringLength(maximumLength:50,MinimumLength =5)]
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Range(19,60)]
        public int Age { get; set; }
       
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        public bool IsActive { get; set; }
    }
}
