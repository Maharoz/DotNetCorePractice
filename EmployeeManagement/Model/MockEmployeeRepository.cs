﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
                _employeeList = new List<Employee>()
                {
                    new Employee() {Id = 1, Name = "Mary", Department = Dept.IT, Email = "marry@gmail.com"},
                    new Employee() {Id = 2, Name = "John", Department = Dept.IT, Email = "john@gmail.com"},
                    new Employee() {Id = 3, Name = "Sam", Department = Dept.IT, Email = "sam@gmail.com"}
                };
        }

        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(x => x.Id==Id);
        }
    }
}
