using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vacation.Repository;

namespace Vacation.ViewModels
{
    public class EmployeeDetails:Employee
    {
       
        public int NumOfTakenVacation { get; set; }
        public int Age { get; set; }
    }
}
