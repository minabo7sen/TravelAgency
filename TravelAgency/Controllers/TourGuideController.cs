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
    public class TourGuideController : Controller
    {
        public ActionResult AddTourGuide()
        {
            var tourViewModel = new TourGuideModel();
            return View("AddTourGuide", tourViewModel);

        }

        public ActionResult ManageTourGuide()
        {
            List<TourGuideModel> profiles = new List<TourGuideModel>();

            using (AgencyDBEntities db = new AgencyDBEntities())
            {
                TourGuideModel UPV;
                var users = db.TourGuides.ToList();

                foreach (TourGuide u in db.TourGuides)
                {
                    UPV = new TourGuideModel();
                    UPV.ID = u.ID;
                    UPV.Name = u.Name;
                    UPV.PhoneNO = u.Phone_Number;
                    UPV.BirthDate = u.Date_Of_Birth;
                    UPV.BirthDate.ToOADate();
                    UPV.Language1 = u.Language1;
                    UPV.Language2 = u.Language2;
                    UPV.Language3 = u.Language3;
                    UPV.Num_Trips = u.Number_Of_Trips;


                    //In order not to display the defaul NONE tourguide who is automatically
                    //assigned to new trips.
                    if (UPV.ID != 1)
                    profiles.Add(UPV);
                }
            }

            TourGuideDataView UDV = new TourGuideDataView();
            UDV.UserProfile = profiles;
            return PartialView(UDV);

        }

        public ActionResult Edit(int id)
        {
            using (AgencyDBEntities db = new AgencyDBEntities())
            {
                var tourguide = db.TourGuides.SingleOrDefault(c => c.ID == id);

                if (tourguide == null)
                    return HttpNotFound();

                var tourViewModel = new TourGuideModel
                {
                    ID = tourguide.ID,
                    Name = tourguide.Name,
                    PhoneNO = tourguide.Phone_Number,
                    BirthDate = tourguide.Date_Of_Birth,
                    Language1 = tourguide.Language1,
                    Language2 = tourguide.Language2,
                    Language3 = tourguide.Language3,
                    Num_Trips = tourguide.Number_Of_Trips

                };

                return View("AddTourGuide", tourViewModel);
            }
        }

        public ActionResult Delete(int id)
        {
            using (AgencyDBEntities db = new AgencyDBEntities())
            {
                var tourguide = db.TourGuides.SingleOrDefault(c => c.ID == id);

                if (tourguide == null)
                    return HttpNotFound();

                foreach (var trip in db.Trips)
                {
                    if (trip.Tour_Guide_ID == id)
                    {
                        trip.Tour_Guide_ID = 5; //Assign the trip to "None Tourguide"
                    }
                }

                db.TourGuides.Remove(tourguide);
                db.Entry(tourguide).State = EntityState.Deleted;
                db.SaveChanges();

                return RedirectToAction("ManageTourGuide", "TourGuide");

            }
        }

        [HttpPost]
        public ActionResult Update(TourGuideModel tourViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("AddTourGuide", tourViewModel);
            }

            tourViewModel = SortLanguages(tourViewModel); //Sorting languages of the tourguide

            using (AgencyDBEntities db = new AgencyDBEntities())
            {
                if (tourViewModel.ID == 0)
                {
                    var tour = new TourGuide
                    {
                        Name = tourViewModel.Name,
                        Phone_Number = tourViewModel.PhoneNO,
                        Date_Of_Birth = tourViewModel.BirthDate,
                        Language1 = tourViewModel.Language1,
                        Language2 = tourViewModel.Language2,
                        Language3 = tourViewModel.Language3,
                        Number_Of_Trips = 0,
                        
                    };
                    db.TourGuides.Add(tour);
                }
                else
                {
                    var tour = db.TourGuides.Single(c => c.ID == tourViewModel.ID);
                    tour.Name = tourViewModel.Name;
                    tour.Phone_Number = tourViewModel.PhoneNO;
                    tour.Date_Of_Birth = tourViewModel.BirthDate;
                    tour.Language1 = tourViewModel.Language1;
                    tour.Language2 = tourViewModel.Language2;
                    tour.Language3 = tourViewModel.Language3;
                    tour.Number_Of_Trips = tourViewModel.Num_Trips;
                }



                db.SaveChanges();

                return RedirectToAction("ManageTourGuide", "TourGuide");
            }
        }

        //TODO:- The following function sorts the languages of the tourguide alphabetically.
        public TourGuideModel SortLanguages(TourGuideModel tourguideModel)
        {
            List<string> tmp = new List<string>();
            tmp.Add(tourguideModel.Language1);
            tmp.Add(tourguideModel.Language2);
            tmp.Add(tourguideModel.Language3);
            tmp.Sort();

            tourguideModel.Language1 = tmp[0];
            tourguideModel.Language2 = tmp[1];
            tourguideModel.Language3 = tmp[2];


            return tourguideModel;
        }

        
    }
}