using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models.ViewModel
{
    public class TourGuideModel : PersonModel  
    {
        [Key]
        public int ID { get; set; }

        

        [Required(ErrorMessage = "*")]
        [Display(Name = "First Langauge")]
        public string Language1 { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Second Language")]
        public string Language2 { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Third Language")]
        public string Language3 { get; set; }

        public List<TripModel> Trips { get; set; }

        public int Num_Trips { get; set; }

    }

    public class TourGuideDataView
    {
        public IEnumerable<TourGuideModel> UserProfile { get; set; }


    }

    
}