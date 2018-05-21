using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using TravelAgency.Models.DB;

namespace TravelAgency.Models.ViewModel
{
    public class TripModel
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Type of the trip")]
        public string Type { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Trip Start Date")]
        [DataType(DataType.Date)]
        public DateTime Start_Date { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Trip End Date")]
        [DataType(DataType.Date)]
        public DateTime End_Date { get; set; }

        [Required(ErrorMessage = "*")]
        [Range(10,45, ErrorMessage ="Minimum number of seats is 10, maximum is 45")]
        [Display(Name = "Number of seats")]
        public int Num_Of_Seats { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Destination")]
        public string Destination { get; set; }

        public int TourGuide_ID { get; set; }

        [Display(Name = "Tour Guide")]
        public string TourGuide_Name { get; set; }
        public List<TicketModel> Tickets { get; set; }
        public int Num_Of_Tickets { get; set; }

    }

    public class TripDataView
    {
        public IEnumerable<Trip> UserProfile { get; set; }
        public IEnumerable<TicketModel> ticketProfile { get; set; }


    }
}