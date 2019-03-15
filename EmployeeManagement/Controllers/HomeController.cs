﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Model;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        public HomeController()
        {
            _employeeRepository = new MockEmployeeRepository();
        }
        public string Index()
        {           
            return _employeeRepository.GetEmployee(1).Name;
        }
    }
}
