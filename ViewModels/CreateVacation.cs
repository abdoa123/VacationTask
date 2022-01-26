using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Vacation.Repository;

namespace Vacation.ViewModels
{
    public class CreateVacation 
    {
        public int PersonId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime VacationFrom { get; set; }

        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime VacationTo { get; set; }

        [Required]
        public string Reason { get; set; }
    }
}
