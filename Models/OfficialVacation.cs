using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation.Repository
{
    public class OfficialVacation
    {
        //ID, Official Holiday Date, Holiday Name
        public int ID { get; set; }

        [Required]
        public DateTime HolidayDate { get; set; }

        [Required]
        public string HolidayName { get; set; }
    }
}
