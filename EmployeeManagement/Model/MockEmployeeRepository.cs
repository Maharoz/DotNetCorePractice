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
                new Employee(){Id = 1 , Name ="Mary" , Department="HR",Email = "marry@gmail.com"},
                new Employee(){Id = 1 , Name ="John" , Department="IT",Email = "john@gmail.com"},
                new Employee(){Id = 1 , Name ="Sam" , Department="IT",Email = "sam@gmail.com"}
            };
        }
        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(x => x.Id==Id);
        }
    }
}