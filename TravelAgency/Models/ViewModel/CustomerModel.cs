using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models.ViewModel
{


    public class CustomerModel : PersonModel 
    {
        [Key]
        public int ID  { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Language")]
        public string Lang { get; set; }

        public int Num_Tickets { get; set; }
    }
    

    

    public class CustomerDataView
    {
        public IEnumerable<CustomerModel> UserProfile { get; set; }
        
        
    }

}