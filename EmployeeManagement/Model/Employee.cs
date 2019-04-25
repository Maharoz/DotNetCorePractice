using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50,ErrorMessage = "Name cannot be exceed characters")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Invalid email format")]
        [Display(Name="Office Email")]
        public string Email { get; set; }
        public Dept Department { get; set; }
    }
}
