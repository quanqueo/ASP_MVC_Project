using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VNPT_TTS_PROJECT.Models;

namespace VNPT_TTS_PROJECT.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            if(Session["username"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }


        //================LOGIN================
        public ActionResult Login()
        {
            if (Session["username"] != null)
            {
                return RedirectToAction("Index");
            }
            else { return View(); }
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            thuctapProjectEntities db = new thuctapProjectEntities();
            var result = db.LOGINs.SingleOrDefault(m=>m.username == username && m.password==password);
            if (result != null)
            {
                Session["username"] = username;
                if (result.mod == false)
                {
                    return Redirect("~/Home/BookYours?username="+username);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else{
                ViewBag.ErrorLogin = "Tài khoản hoặc mật khẩu không chính xác !";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Remove("username");
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(LOGIN model)
        {
            thuctapProjectEntities db = new thuctapProjectEntities();
            var cfPass = Request["cfPassword"];
            var checkUsername = db.LOGINs.Find(model.username);
            if(checkUsername == null)
            {
                if (cfPass == model.password)
                {
                    db.LOGINs.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.ErrorCfPass = "Mật khẩu xác nhận không trùng khớp !";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorUsername = "Tên đăng nhập đã tồn tại !";
                return View();
            }
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        //================END LOGIN================









        //================TOURS================
        public ActionResult ListTours()
        {
            return View();
        }

        public ActionResult AddTours()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTours(Tour model)
        {
            thuctapProjectEntities db = new thuctapProjectEntities();
            db.Tours.Add(model);
            db.SaveChanges();
            return RedirectToAction("ListTours");
        }

        public ActionResult EditTours(string id)
        {
            thuctapProjectEntities db = new thuctapProjectEntities();
            var tour = db.Tours.Find(id);
            return View(tour);
        }
        [HttpPost]
        public ActionResult EditTours(Tour model)
        {
            thuctapProjectEntities db = new thuctapProjectEntities();
            var editTour = db.Tours.Find(model.idTuor);
            if(editTour != null)
            {
                editTour.destination = model.destination;
                editTour.followers = model.followers;
                editTour.price_pPerson = model.price_pPerson;
                editTour.url = model.url;
                editTour.detailDeal = model.detailDeal;
                editTour.idP = model.idP;
                editTour.thumbCityUrl = model.thumbCityUrl;
                db.SaveChanges();
            }
            return RedirectToAction("ListTours");
        }

        public ActionResult DeleteTour(string id)
        {
            thuctapProjectEntities db = new thuctapProjectEntities();
            var delTour = db.Tours.Find(id);
            db.Tours.Remove(delTour);
            db.SaveChanges();
            return RedirectToAction("ListTours");
        }
        //================END TOURS================







        //================PLACE================
        public ActionResult ListPlaces()
        {
            return View();
        }

        public ActionResult AddPlace()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPlace(Place model)
        {
            thuctapProjectEntities db = new thuctapProjectEntities();
            var checkID = db.Places.Find(model.idP);

            if(checkID == null)
            {
                ViewBag.ErrorID = "ID đã tồn tại !";
                return View();
            }
            else
            {
                db.Places.Add(model);
                db.SaveChanges();
            }
            return RedirectToAction("ListPlaces");
        }

        public ActionResult EditPlace(string id)
        {
            thuctapProjectEntities db = new thuctapProjectEntities();
            var place = db.Places.Find(id);
            return View(place);
        }
        [HttpPost]
        public ActionResult EditPlace(Place model)
        {
            thuctapProjectEntities db = new thuctapProjectEntities();
            var editPlace = db.Places.Find(model.idP);
            if (editPlace != null)
            {
                editPlace.idP = model.idP;
                editPlace.title = model.title;
                editPlace.url = model.url;
                editPlace.description = model.description;
                editPlace.price = model.price;
                editPlace.area = model.area;
                editPlace.followers = model.followers;
                editPlace.idC = model.idC;
                db.SaveChanges();
            }
            return RedirectToAction("ListPlaces");
        }

        public ActionResult DeletePlace(string id)
        {
            thuctapProjectEntities db = new thuctapProjectEntities();
            var delPlace = db.Places.Find(id);
            db.Places.Remove(delPlace);
            db.SaveChanges();
            return RedirectToAction("ListPlaces");
        }
        //================END PLACE================
    }
}