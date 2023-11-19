using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interfaces
{
    public interface IEmployeeBL
    {
        public void AddEmployee(EmployeeModel employeeModel);
        public IEnumerable<EmployeeModel> GetAllEmployees();
        public EmployeeModel UpdateEmployee(EmployeeModel employeeModel);
        public bool DeleteEmployee(int? id);
        public EmployeeModel GetEmployeeData(int? id);
        public EmployeeModel Login(EmpLogin empLogin);
        public EmployeeModel GetEmployeeByName(string name);
    }
}
