using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Model;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
   
    public class HomeController : Controller
    {
        private IEmployeeRepository _employeeRepository;
        private IHostingEnvironment hostingEnvironment;
        public HomeController(IEmployeeRepository employeeRepository,IHostingEnvironment hostingEnvironment)
        {
            //_employeeRepository = new MockEmployeeRepository();
            _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        public ViewResult Index()
        {           
            var model = _employeeRepository.GetAllEmployee();
            return View(model);
        }

       
        public ViewResult Details(int? id)
        {

            Employee employee = _employeeRepository.GetEmployee(id.Value);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }
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

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if (model.Photos != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                      string filePath=  Path.Combine(hostingEnvironment.WebRootPath,
                            "images", model.ExistingPhotoPath);
                      System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = ProcessUploadedFiles(model);
                }
                
            
                _employeeRepository.Update(employee);
                return RedirectToAction("index", new { id = employee.Id });
            }

            return View();

        }

        private string ProcessUploadedFiles(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photos != null)
            {
                foreach (IFormFile photo in model.Photos)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                   
                }
            }

            return uniqueFileName;
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var uniqueFileName = ProcessUploadedFiles(model);
                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                _employeeRepository.Add(newEmployee);
                 return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View();

        }

    }
}
