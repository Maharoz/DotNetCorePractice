using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Model
{
    public class ApplicationUser :IdentityUser
    {
        public string City { get; set; }

    }
}
