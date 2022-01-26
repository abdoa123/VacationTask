using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation.Repository
{
    public interface IEmployeeRepository
    {
        Employee FindEmployee(int id);
        bool AddEmployee(Employee entity);
        bool UpdateEmployee( Employee entity);
        bool DeleteEmployee(int id);
        IEnumerable<Employee> GetAllEmployee();

        IEnumerable<EmpolyeeVacation> GetAllEmployeeVacation(int id);
        bool AddNewVacation(EmpolyeeVacation entity);
    }
}
