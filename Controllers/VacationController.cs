using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vacation.Repository;
using Vacation.ViewModels;

namespace Vacation.Controllers
{
    public class VacationController : Controller
    {
        private readonly IEmployeeRepository _EmployeeContext;

        public VacationController(IEmployeeRepository employeeRepositor)
        {
            _EmployeeContext = employeeRepositor;
        }
        public IActionResult GetAllVacation(int id)
        {
            var vacations = _EmployeeContext.GetAllEmployeeVacation(id);
            return View(vacations);

        }
        //get Vacation form  for Creation
        [HttpGet]
        public ViewResult AddVacation(int id)
        {
            CreateVacation temp = new CreateVacation();
            temp.PersonId = id;
            return View(temp);
        }

        [HttpPost]
        public IActionResult AddVacation(CreateVacation temp)
        {
            if (ModelState.IsValid)
            {

                var employee = _EmployeeContext.FindEmployee(temp.PersonId);
                // check if employee in already vacation 

                var check = _EmployeeContext.GetAllEmployeeVacation(temp.PersonId).LastOrDefault();

                if (check != null)
                {
                    // check if employee is already in vacation
                    if (check.VacationTo.Ticks > temp.VacationFrom.Ticks && check.VacationTo.Ticks < temp.VacationTo.Ticks)
                    {
                        ViewBag.Message = "User is Already in Vacation";//if it is redirecting to some other action then use TempData
                        return View(temp);
                    }
                }

                // get vacation days
                int diff = (int)(temp.VacationTo - temp.VacationFrom).TotalDays;
                if (employee == null)
                {
                    ModelState.AddModelError("", "User Not Found");
                    return View(temp);
                }
                else
                {
                  
                    if (diff < 0)
                    {
                        ViewBag.Message = "End date must be Greater than start date ";//if it is redirecting to some other action then use TempData
                        return View(temp);
                    }
                    if (employee.RemainingVacationDays - diff <0)
                    {
                        ViewBag.Message = "You can not take this Vacation";//if it is redirecting to some other action then use TempData
                        return View(temp);
                    }
                    else
                    {
                        EmpolyeeVacation employeeVacation = new EmpolyeeVacation {
                            EmployeeId = temp.PersonId,
                            Reason = temp.Reason,
                            VacationFrom = temp.VacationFrom,
                            VacationTo = temp.VacationTo,

                        };
                        employee.RemainingVacationDays = employee.RemainingVacationDays - diff;
                        _EmployeeContext.AddNewVacation(employeeVacation);
                        _EmployeeContext.UpdateEmployee(employee);
                        
                        
                        return RedirectToAction("Index", "home", new { done = "done" });
                    }
                }
            }
            else
            {
            return View(temp);
            }
        }
    }
}