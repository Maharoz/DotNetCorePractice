using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Model;
using EmployeeManagement.ViewModels;
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
        public ViewResult Index()
        {           
            var model = _employeeRepository.GetAllEmployee();
            return View(model);
        }

        public ViewResult Details(int id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(1),
                PageTitle = "Employee details"
            };

            return View(homeDetailsViewModel);
        }
    }
}
