using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Vacation.Models;
using Vacation.Repository;
using Vacation.ViewModels;

namespace Vacation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _EmployeeContext;

        private readonly IHostingEnvironment hostingEnvironment ;

        public HomeController(IEmployeeRepository employeeRepositor , IHostingEnvironment hostingEnvironment)
        {
            _EmployeeContext = employeeRepositor;
            this.hostingEnvironment = hostingEnvironment;
        }

        //return all Employee in the system
        public IActionResult Index(string done )
        {
            if (!string.IsNullOrEmpty(done))
            {
                ViewBag.Success = done;
                return View(_EmployeeContext.GetAllEmployee());
            }
            return View(_EmployeeContext.GetAllEmployee());
        }

        //get Empoyee form for Creation
        [HttpGet]
        public ViewResult AddEmployee()
        {
            return View();
        }

        //add employee in Database
        [HttpPost]
        public IActionResult Create(CreateViewModel employee)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                // has selected an image to upload.
                if (employee.Photo != null)
                {
                    uniqueFileName = ProcessUploadedFile(employee);

                }
                // employee.Id = _emplyees.GetAll().Max(e => e.Id) + 1;

                Employee newEmployee = new Employee
                {
                    Name = employee.Name,
                    Email = employee.Email,
                    BirthDate = employee.BirthDate,
                    HiringDate = employee.HiringDate,
                    Jobtitle = employee.JobTitle,
                    PhoneNumber = employee.PhoneNumber,
                    Address = employee.Address,

                    // Store the file name in ProfilePath property of the employee object
                    // which gets saved to the Employees database table
                    ProfilePath = uniqueFileName
                };

                // check employee Hiring Date
                var diff = (DateTime.Now - employee.HiringDate ).TotalDays;
                // check if  working for more than 10 years 
                if (diff > 3650)
                {
                    newEmployee.RemainingVacationDays = 30;
                    newEmployee.TotalVacationDays = 30;
                }
                else
                {
                    newEmployee.RemainingVacationDays = 21;
                    newEmployee.TotalVacationDays = 21;
                }

                _EmployeeContext.AddEmployee(newEmployee);
                //return RedirectToAction("details", new { id = newEmployee.Id });
                return RedirectToAction("Index", new { done = "done" }); 
            }
            else
            {
                return View("AddEmployee");
            }

        }

        //upload image 
        private string ProcessUploadedFile(CreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
        //Edit employee 
        [HttpGet]
        public IActionResult EditEmployee(int id)
        {
            var employee = _EmployeeContext.FindEmployee(id);
            EditViewModel employeeEditViewModel = new EditViewModel
            {
                Id = employee.ID,
                Name = employee.Name,
                Email = employee.Email,
                Address = employee.Address,
                BirthDate = employee.BirthDate,
                HiringDate = employee.HiringDate,
                JobTitle = employee.Jobtitle,
                PhoneNumber = employee.PhoneNumber,
                photoPath = employee.ProfilePath
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _EmployeeContext.FindEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Address = model.Address;
                employee.BirthDate = model.BirthDate;
                employee.HiringDate = model.HiringDate;
                employee.Jobtitle = model.JobTitle;
                employee.PhoneNumber = model.PhoneNumber;

                // has selected an image to upload.
                if (model.Photo != null)
                {
                    if (model.photoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath,
                            "images", model.photoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.ProfilePath = ProcessUploadedFile(model);

                }
                _EmployeeContext.UpdateEmployee(employee);
                //return RedirectToAction("details", new { id = employee.Id });
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }
        //Details for Employee

        public IActionResult Details(int id)
        {
            Employee Employee = _EmployeeContext.FindEmployee(id);

            if (Employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id);
            }
            // Save today's date.
            var today = DateTime.Today;

            // Calculate the age.
            var age = today.Year - Employee.BirthDate.Year;

     

            EmployeeDetails employee = new EmployeeDetails();
            employee.Name = Employee.Name;
            employee.ID = Employee.ID;
            employee.Email = Employee.Email;
            employee.Address = Employee.Address;
            employee.BirthDate = Employee.BirthDate;
            employee.HiringDate = Employee.HiringDate;
            employee.Jobtitle = Employee.Jobtitle;
            employee.PhoneNumber = Employee.PhoneNumber;
            employee.ProfilePath = Employee.ProfilePath;
            employee.TotalVacationDays = Employee.TotalVacationDays;
            employee.RemainingVacationDays = Employee.RemainingVacationDays;
            employee.NumOfTakenVacation = Employee.TotalVacationDays - Employee.RemainingVacationDays;
            employee.Age = age;

            return View(employee);
        }

        //Delete Employee
        public IActionResult DeleteEmployee(int id)
        {
           var DeleteEmp =  _EmployeeContext.DeleteEmployee(id);
            if (DeleteEmp)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

            }
        }

       [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
