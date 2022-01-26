using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation.Repository
{
    public class Employee
    {
        //ID, Name, Jobtitle, Hiring Date.Profile Image, Birthdate, Mobile No., Email, Address
        public int ID { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }


        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Invalid Email format")]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Jobtitle { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]

        [Column(TypeName = "Date")]
        public DateTime HiringDate { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]

        public DateTime BirthDate { get; set; }
        

        // Store profile Image path 
        public string ProfilePath { get; set; }

        //phone must be string to store {0}
        [Required, RegularExpression("(^[0-9]+$)", ErrorMessage = "phone  must be Number "), MinLength(10, ErrorMessage = "Phone cannot be less than 10 Digits")]
        public string PhoneNumber { get; set; }

        [Required]

        public string Address { get; set; }

        // employee total number of vacations 
        public int TotalVacationDays { get; set; }

        //employee total number of vacations Remaining.
        public int RemainingVacationDays { get; set; }

    }
}
