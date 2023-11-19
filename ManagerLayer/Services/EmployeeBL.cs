using ManagerLayer.Interfaces;
using ModelLayer;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Services
{
    public class EmployeeBL:IEmployeeBL
    {
        private readonly IEmployeeRL employeeRL;
        public EmployeeBL(IEmployeeRL employeeRL)
        {
            this.employeeRL = employeeRL;
        }
        public void AddEmployee(EmployeeModel employeeModel)
        {
             employeeRL.AddEmployee(employeeModel);
        }
        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            return employeeRL.GetAllEmployees();
        }

        public EmployeeModel UpdateEmployee(EmployeeModel employeeModel)
        {
            return employeeRL.UpdateEmployee(employeeModel);
        }
        public bool DeleteEmployee(int? id)
        {
            return employeeRL.DeleteEmployee(id);
        }
        public EmployeeModel GetEmployeeData(int? id)
        {
            return employeeRL.GetEmployeeData(id);
        }
        public EmployeeModel Login(EmpLogin empLogin)
        {
            return employeeRL.Login(empLogin);
        }
        public EmployeeModel GetEmployeeByName(string name)
        {
            return employeeRL.GetEmployeeByName(name);
        }
    }

}
