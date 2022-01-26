using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation.ViewModels
{
    public class CreateViewModel
    {
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
           ErrorMessage = "Invalid Email format")]
        [Required]
        public string Email { get; set; }

        [Required]
        public string JobTitle { get; set; }
        [Required]
        public DateTime HiringDate { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Address { get; set; }
        [Required, RegularExpression("(^[0-9]+$)", ErrorMessage = "phone  must be Number "), MinLength(10, ErrorMessage = "Phone cannot be less than 10 Digits")]

        public string PhoneNumber { get; set; }
      
        public IFormFile Photo { get; set; }
    }
}
