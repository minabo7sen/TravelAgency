using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelAgency.Models.DB;
using TravelAgency.Models.ViewModel;
using System.Web.Mvc;
using System.Data.Entity;


namespace TravelAgency.Controllers
{
    public class TripController : Controller
    {
        public ActionResult AddTrip()
        {
            var tripViewModel = new Trip();
            return View("AddTrip", tripViewModel);

        }

        public ActionResult ManageTrips()
        {

            using (AgencyDBEntities db = new AgencyDBEntities())
            {

                var listOfTrips = db.Trips.Include(t => t.TourGuide).ToList();

                foreach (Trip t in listOfTrips)
                {
                   

                    if (t.Number_Of_Seats < 0)
                    {

                        listOfTrips.Remove(t);

                    }
                }

                TripDataView UDV = new TripDataView();
                UDV.UserProfile = listOfTrips;
                return PartialView(UDV);
            }
        
        }

        public ActionResult Edit(int id)
        {
            using (AgencyDBEntities db = new AgencyDBEntities())
            {
                var trip = db.Trips.SingleOrDefault(c => c.ID == id);

                if (trip == null)
                    return HttpNotFound();

                var tripViewModel = new Trip
                {
                    ID = trip.ID,
                    Type = trip.Type,
                    Start_Date = trip.Start_Date,
                    End_Date = trip.End_Date,
                    Number_Of_Seats = trip.Number_Of_Seats,
                    Tour_Guide_ID = trip.Tour_Guide_ID,
                    Destination = trip.Destination,
                    Langauge1 = trip.Langauge1,
                    Langauge2 = trip.Langauge2,
                    Language3 = trip.Language3,
                    Price = trip.Price


                };

                
                return View("AddTrip", tripViewModel);
            }
        }

        public ActionResult Delete(int id)
        {
            using (AgencyDBEntities db = new AgencyDBEntities())
            {
                var trip = db.Trips.SingleOrDefault(c => c.ID == id);

                foreach (Ticket u in db.Tickets)
                {
                    if (u.Trip_ID == id)
                    {
                        db.Tickets.Remove(u);
                        var customer = db.Customers.SingleOrDefault(c => c.ID == u.Customer_ID);
                        customer.Number_Of_Trips--;
                    }

                }
                        var tourguide = db.TourGuides.SingleOrDefault(c => c.ID == trip.Tour_Guide_ID);
                        tourguide.Number_Of_Trips--;

                if (trip == null)
                    return HttpNotFound();

                db.Trips.Remove(trip);
                db.Entry(trip).State = EntityState.Deleted;
                db.SaveChanges();

                return RedirectToAction("ManageTrips", "Trip");

            }
        }

        [HttpPost]
        public ActionResult Update(Trip tripViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("AddTrip", tripViewModel);
            }

            if (tripViewModel.Start_Date>tripViewModel.End_Date)
            {
                ViewBag.DateError = "The trip can't end before it starts!";
                return View("AddTrip", tripViewModel);
            }

            tripViewModel = SortLanguages(tripViewModel);

            using (AgencyDBEntities db = new AgencyDBEntities())
            {

                if (tripViewModel.ID == 0)
                {

                    var trip = new Trip
                    {
                        Type = tripViewModel.Type,
                        Start_Date = tripViewModel.Start_Date,
                        End_Date = tripViewModel.End_Date,
                        Number_Of_Seats = tripViewModel.Number_Of_Seats,
                        Tour_Guide_ID = 1, //The ID of the NONE tour guide, the default tourguide 
                                           //assigned when creating a new trip
                        Destination = tripViewModel.Destination,
                        Number_Of_Tickets = 0,
                        Langauge1 = tripViewModel.Langauge1,
                        Langauge2 = tripViewModel.Langauge2,
                        Language3 = tripViewModel.Language3,
                        Price = tripViewModel.Price,


                    };
                    db.Trips.Add(trip);
                    db.Entry(trip).State = EntityState.Added;


                }
                else
                {
                    var trip = db.Trips.Single(c => c.ID == tripViewModel.ID);

                    trip.Type = tripViewModel.Type;
                    trip.Start_Date = tripViewModel.Start_Date;
                    trip.End_Date = tripViewModel.End_Date;
                    trip.Number_Of_Seats = tripViewModel.Number_Of_Seats;
                    trip.Destination = tripViewModel.Destination;
                    trip.Langauge1 = tripViewModel.Langauge1;
                    trip.Langauge2 = tripViewModel.Langauge2;
                    trip.Language3 = tripViewModel.Language3;
                    trip.Price = tripViewModel.Price;


                    db.Entry(trip).State = EntityState.Modified;
                }


                db.SaveChanges();

                return RedirectToAction("ManageTrips", "Trip");
            }
        }

        public ActionResult SelectTrip(int id)
        {
            ViewBag.customerID = id;
            using (AgencyDBEntities db = new AgencyDBEntities())
            {

                var listOfTrips = db.Trips.Include(t => t.TourGuide).ToList();

                foreach (Trip t in listOfTrips)
                {

                    if (t.Number_Of_Seats < 0)
                    {

                        listOfTrips.Remove(t);

                    }
                }

                TripDataView UDV = new TripDataView();
                UDV.UserProfile = listOfTrips;
                return PartialView(UDV);
            }
        }

        public ActionResult BookTrip(int CustomerID, int TripID)
        {
            using (AgencyDBEntities db = new AgencyDBEntities())
            {

                var trip = db.Trips.Single(c => c.ID == TripID);

                var tourguide = db.TourGuides.Single(c => c.ID == trip.Tour_Guide_ID);

                var customer = db.Customers.Single(c => c.ID == CustomerID);

                if (customer.Language == tourguide.Language1 || customer.Language == tourguide.Language2 || customer.Language == tourguide.Language3)
                {
                    customer.Number_Of_Trips++;
                    trip.Number_Of_Tickets++;

                    var ticket = new Ticket
                    {
                        Seat_Number = trip.Number_Of_Seats,
                        Customer_ID = CustomerID,
                        Trip_ID = TripID,
                        Type ="Gold"
                        

                    };
                    db.Tickets.Add(ticket);
                    db.Entry(ticket).State = EntityState.Added;

                }

                else
                {
                    return RedirectToAction("Error", "Home");
                }
                db.SaveChanges();
                return RedirectToAction("ManageTrips", "Trip");
            }
        }

        public Trip SortLanguages(Trip tourguideModel)
        {
            List<string> tmp = new List<string>();
            tmp.Add(tourguideModel.Langauge1);
            tmp.Add(tourguideModel.Langauge2);
            tmp.Add(tourguideModel.Language3);
            tmp.Sort();

            tourguideModel.Langauge1 = tmp[0];
            tourguideModel.Langauge2 = tmp[1];
            tourguideModel.Language3 = tmp[2];


            return tourguideModel;
        }


    }
}
