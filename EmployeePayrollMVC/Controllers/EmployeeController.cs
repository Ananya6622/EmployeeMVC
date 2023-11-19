using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System.Collections.Generic;
using System.Linq;

namespace EmployeePayrollMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeBL employeeBL;
        public EmployeeController(IEmployeeBL employeeBL)
        {
            this.employeeBL = employeeBL;
        }
        public IActionResult GetAllEmployees()
        {
            List<EmployeeModel> lstEmployee = new List<EmployeeModel>();
            lstEmployee = employeeBL.GetAllEmployees().ToList();
            //ViewData["EmployeeCount"] = lstEmployee.Count;
            return View(lstEmployee);

        }

        [HttpGet]
        //[Route("AddEmployee")]
        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddEmployee([Bind] EmployeeModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                employeeBL.AddEmployee(employeeModel);
                //TempData["SuccessMessage"] = "Employee added successfully!";
                return RedirectToAction("GetAllEmployees");
            }
            return View(employeeModel);
        }

        [HttpGet]
        public IActionResult UpdateEmployee(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AddEmployee");
            }
            EmployeeModel employee = employeeBL.GetEmployeeData(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public IActionResult UpdateEmployee(int id, [Bind] EmployeeModel employee)
        {
            if (id != employee.EmployeeId)
            {
                return RedirectToAction("AddEmployee"); ;
            }
            if (ModelState.IsValid)
            {
                employeeBL.UpdateEmployee(employee);
                return RedirectToAction("GetAllEmployees");
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmployeeModel employee = employeeBL.GetEmployeeData(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        
        public IActionResult DeleteConfirmed(int? id)
        {
            employeeBL.DeleteEmployee(id);
            return RedirectToAction("GetAllEmployees");
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmployeeModel employee = employeeBL.GetEmployeeData(id);

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpGet]
        
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([Bind] EmpLogin empLogin)
        {
           
                var emp = employeeBL.Login(empLogin);
            if(emp != null)
            {
                HttpContext.Session.SetInt32("EmployeeId", emp.EmployeeId);
                return RedirectToAction("GetAllEmployees");
            }
                return RedirectToAction("GetAllEmployees");
            
        }

        [HttpGet]
        public IActionResult GetEmployeeByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return NotFound();
            }
            EmployeeModel model = employeeBL.GetEmployeeByName(name);
            if(model != null)
            {
                return View("Details",model);
            }
            return View(model);
        }
    }
}
