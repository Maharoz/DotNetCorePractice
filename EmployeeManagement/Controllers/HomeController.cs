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

       
        public ViewResult Details(int? id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = _employeeRepository.GetEmployee(id ?? 1),
                PageTitle = "Employee details"
            };

            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public RedirectToActionResult Create(Employee employee)
        {
          Employee newEmployee =  _employeeRepository.Add(employee);
          return RedirectToAction("details", new {id = newEmployee.Id});
        }

    }
}
