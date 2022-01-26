using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vacation.Repository;

namespace Vacation.Models.Repository
{
    public class EmployeeOperation : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeOperation(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public bool AddEmployee(Employee entity)
        {
            try
            {
                _context.Employees.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch(Exception err)
            {
                return false;
            }
        }

        public bool DeleteEmployee(int id)
        {
            try
            {
                Employee employee = _context.Employees.Find(id);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Employee FindEmployee(int id)
        {
           
                Employee employee = _context.Employees.Find(id);
                
                return employee;
            
        }

        public bool UpdateEmployee( Employee entity)
        {
            try
            {
                var employee = _context.Employees.Attach(entity);
                employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool AddNewVacation(EmpolyeeVacation entity)
        {
            try
            {
                _context.EmpolyeeVacations.Add(entity);
                _context.SaveChanges();
                //return status done
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<EmpolyeeVacation> GetAllEmployeeVacation(int id)
        {
            return _context.EmpolyeeVacations.Include("Employee").Where(x => x.EmployeeId == id).ToList();
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _context.Employees;
        }


    }
}
