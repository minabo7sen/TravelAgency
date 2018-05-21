using System;
using System.Web.Mvc;
using System.Web.Security;
using TravelAgency.Models.ViewModel;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using TravelAgency.Models.DB;


namespace TravelAgency.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Assign()
        {
            using (AgencyDBEntities db = new AgencyDBEntities())
            {
               

                var tourGuides = db.TourGuides.ToList();
                
                foreach (var tourGuide in tourGuides)
                {
                    var trips = db.Trips.OrderBy(t => t.End_Date)
                        .Where(t => t.Langauge1 == tourGuide.Language1 && t.Langauge2 == tourGuide.Language2 && t.Language3 == tourGuide.Language3 && t.Tour_Guide_ID == 1).ToList();

                    if (trips.Count() > 0)
                    {
                        DateTime lastTripEndDate = trips[0].End_Date;

                        for (var i = 0; i < trips.Count(); i++)
                        {
                            if (i == 0)
                            {
                                trips[i].Tour_Guide_ID = tourGuide.ID;
                                tourGuide.Number_Of_Trips++;
                            }
                            else
                            {
                                if (trips[i].Start_Date >= lastTripEndDate)
                                {
                                    trips[i].Tour_Guide_ID = tourGuide.ID;
                                    tourGuide.Number_Of_Trips++;
                                    lastTripEndDate = trips[i].End_Date;
                                }
                            }
                        }
                    }
                }
                db.SaveChanges();
            }
           return RedirectToAction("ManageTrips", "Trip");
        }


    }
}