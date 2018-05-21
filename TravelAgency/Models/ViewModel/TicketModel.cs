using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using TravelAgency.Models.DB;

namespace TravelAgency.Models.ViewModel
{
    public class TicketModel
    {
        [Key]
        public int ID { get; set; }
        public int Seat_Num { get; set; }
        public int Customer_ID { get; set; }
        public int Trip_ID { get; set; }
        public string Type { get; set; }
        public string number { get; set; }


    }

    public class TicketDataView
    {
        public IEnumerable<Ticket> TicketProfile { get; set; }


    }
}