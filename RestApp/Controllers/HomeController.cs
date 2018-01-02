using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestApp.Controllers
{
    public class HomeController : Controller
    {
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

    }
}