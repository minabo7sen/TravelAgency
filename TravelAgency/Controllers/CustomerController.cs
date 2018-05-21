using System;
using System.Web.Mvc;
using System.Web.Security;
using System.Collections.Generic;
using System.Linq;
using TravelAgency.Models.ViewModel;
using TravelAgency.Models.DB;

namespace TravelAgency.Controllers
{
    public class CustomerController : Controller
    {

        public ActionResult SignUp()
        {
            var customerViewModel = new CustomerModel();
            return View("SignUp", customerViewModel);

        }

        public ActionResult ManageCustomers()
        {
            List<CustomerModel> profiles = new List<CustomerModel>();

            using (AgencyDBEntities db = new AgencyDBEntities())
            {
                CustomerModel UPV;
                var users = db.Customers.ToList();

                foreach (Customer u in db.Customers)
                {
                    UPV = new CustomerModel();
                    UPV.ID = u.ID;
                    UPV.Name = u.Name;
                    UPV.PhoneNO = u.Phone_Number;
                    UPV.BirthDate = u.Date_Of_Birth;
                    UPV.Lang = u.Language;
                    UPV.Num_Tickets = u.Number_Of_Trips;



                    profiles.Add(UPV);
                }
            }

            CustomerDataView UDV = new CustomerDataView();
            UDV.UserProfile = profiles;
            return PartialView(UDV);

        }

        public ActionResult Edit(int id)
        {
            using (AgencyDBEntities db = new AgencyDBEntities())
            {
                var customer = db.Customers.SingleOrDefault(c => c.ID == id);

                if (customer == null)
                    return HttpNotFound();

                var customerViewModel = new CustomerModel
                {
                    ID = customer.ID,
                    Name = customer.Name,
                    PhoneNO = customer.Phone_Number,
                    BirthDate = customer.Date_Of_Birth,
                    Lang = customer.Language,
                    Num_Tickets = customer.Number_Of_Trips

                };

                return View("SignUp", customerViewModel);
            }
        }

        public ActionResult Delete(int id)
        {
            using (AgencyDBEntities db = new AgencyDBEntities())
            {
                var customer = db.Customers.SingleOrDefault(c => c.ID == id);

                if (customer == null)
                    return HttpNotFound();

                db.Customers.Remove(customer);
                db.SaveChanges();

                return RedirectToAction("ManageCustomers", "Customer");

            }
        }

        [HttpPost]
        public ActionResult Update(CustomerModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SignUp", customerViewModel);
            }

            using (AgencyDBEntities db = new AgencyDBEntities())
            {
                if (customerViewModel.ID == 0)
                {
                    var customer = new Customer
                    {
                        Name = customerViewModel.Name,
                        Phone_Number = customerViewModel.PhoneNO,
                        Date_Of_Birth = customerViewModel.BirthDate,
                        Language = customerViewModel.Lang,
                        Number_Of_Trips = 0
                    };
                    db.Customers.Add(customer);
                }
                else
                {
                    var customer = db.Customers.Single(c => c.ID == customerViewModel.ID);
                    customer.Name = customerViewModel.Name;
                    customer.Phone_Number = customerViewModel.PhoneNO;
                    customer.Date_Of_Birth = customerViewModel.BirthDate;
                    customer.Language = customerViewModel.Lang;
                    customer.Number_Of_Trips = customerViewModel.Num_Tickets;
                }



                db.SaveChanges();

                return RedirectToAction("ManageCustomers", "Customer");
            }
        }

        public ActionResult Info(int id)
        {
            List<Ticket> profiles = new List<Ticket>();

            using (AgencyDBEntities db = new AgencyDBEntities())
            {

                Ticket UPV;
                var customer = db.Customers.SingleOrDefault(c => c.ID == id);
                ViewBag.customername = customer.Name;

                foreach (var ticket in db.Tickets)
                {
                    if (id == ticket.Customer_ID)
                    {

                        UPV = new Ticket();
                        UPV.ID = ticket.ID;
                        UPV.Seat_Number = ticket.Seat_Number;
                        UPV.Trip = ticket.Trip;
                        UPV.Type = ticket.Type;
                       
                        profiles.Add(UPV);
                    }


                }
            }

            TicketDataView TDV = new TicketDataView();
            TDV.TicketProfile = profiles;
            return PartialView(TDV);
        }

        public ActionResult CancelTrip(int id)
        {
            using (AgencyDBEntities db = new AgencyDBEntities())
            {
                var ticket = db.Tickets.SingleOrDefault(t => t.ID == id);
                var customer = db.Customers.SingleOrDefault(c => c.ID == ticket.Customer_ID);
                var trip = db.Trips.SingleOrDefault(t => t.ID == ticket.Trip_ID);

                

                trip.Number_Of_Tickets--;
                customer.Number_Of_Trips--;
                db.Tickets.Remove(ticket);
                db.SaveChanges();



                return RedirectToAction("Info", new {id = customer.ID });
            }

        }
    }
}