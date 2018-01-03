using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestApp.Models;

namespace RestApp.Controllers
{
    public class HomeController : Controller
    {
        private RestaurantUserDatabaseEntities db = new RestaurantUserDatabaseEntities();
        private Restaurant_Users currentUser = null;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page.";

            return View();
        }

        public ActionResult Menu()
        {
            ViewBag.Message = "Menu.";

            return View();
        }

        public ActionResult Restaurants()
        {
            ViewBag.Message = "Restaurants.";

            return View();
        }
        public ActionResult Cart()
        {
            ViewBag.Message = "Cart page.";

            return View();
        }
        public ActionResult Reserve()
        {
            ViewBag.Message = "Reserve page.";

            return View();
        }

        public ActionResult Thank()
        {
            ViewBag.Message = "Thank you page.";

            return View();
        }

        public ActionResult ReservationCompleted()
        {
            ViewBag.Message = "Reservation page.";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Login page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Username,Password")] Restaurant_Users user)
        {
            if (ModelState.IsValid)
            {
                var obj = db.Restaurant_Users.Where(a => a.Username.Equals(user.Username)
                            && a.Password.Equals(user.Password)).FirstOrDefault();
                if (obj != null)
                {
                    currentUser = user;
                    Session["User"] = user.Username.ToString();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.User = user;
            return View(user);
        }

        public ActionResult Logout()
        {
            currentUser = null;
            Session["User"] = null;
                   
            ViewBag.User = null;
            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            ViewBag.Message = "Register page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Username,Password,Email")] Restaurant_Users user)
        {
            if (ModelState.IsValid)
            {
                user.RegDate = DateTime.Now;
                db.Restaurant_Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }
    }
}