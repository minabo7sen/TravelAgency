using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models.ViewModel
{
    public class PersonModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNO { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}