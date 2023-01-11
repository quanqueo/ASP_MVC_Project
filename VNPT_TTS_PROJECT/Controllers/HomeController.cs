using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VNPT_TTS_PROJECT.Models;

namespace VNPT_TTS_PROJECT.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: Home
        public ActionResult Index()
        {
            thuctapProjectEntities db = new thuctapProjectEntities();
            List<Place> dsPlace = db.Places.ToList();
            return View(dsPlace);
        }

        public ActionResult About(string idP=null)
        {
            thuctapProjectEntities db = new thuctapProjectEntities();
            if(idP == null)
            {
                idP = "caribbean";
            }
            var place = db.Places.Find(idP);

            
            return View(place);
        }



        public ActionResult Deals(string idP)
        {
            thuctapProjectEntities db = new thuctapProjectEntities();
            var price = Request["price"];
            ViewBag.listPlaces = db.Places.ToList();
            if (price == null)
            {
                ViewBag.listToursSearch = db.Tours.ToList();
            }
            else
            {
                var arrPrice = Request["price"].Split('-');
                var priceFrom = Convert.ToInt32(arrPrice[0]);
                var priceTo = Convert.ToInt32(arrPrice[1]);
                ViewBag.listToursSearch = db.Tours.Where(m => (string.IsNullOrEmpty(idP) || m.idP == idP) && ((priceFrom == 0 && priceTo == 0) || (m.price_pPerson > priceFrom && m.price_pPerson < priceTo))).ToList();
            }
            
            return View();
        }


        public ActionResult Reservation(string idTour=null)
        {
            thuctapProjectEntities db = new thuctapProjectEntities();
            if(idTour == null)
            {
                idTour = "havana";
            }
            var tour = db.Tours.Find(idTour);
            return View(tour);
        }
        [HttpPost]
        public ActionResult Reservation(Reservation model)
        {
            thuctapProjectEntities db = new thuctapProjectEntities();
            db.Reservations.Add(model);
            db.SaveChanges();
            if(Session["username"] != null)
            {
                return RedirectToAction("BookYours");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        public ActionResult BookYours(string username)
        {
            thuctapProjectEntities db = new thuctapProjectEntities();
            List<Reservation> myBooking = db.Reservations.ToList();
            if (Session["username"] == null)
            {
                return Redirect("~/Admin/Home/Login");
            }
            else
            {
                return View(myBooking);
            }     
        }
    }
}