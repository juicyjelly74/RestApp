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
            ViewBag.SubjectID = new SelectList(db.Subject, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact([Bind(Include = "FirstName,LastName,Email,Message,SubjectID")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.ReviewSet.Add(review);
                db.SaveChanges();
                return RedirectToAction("Thank");
            }

            ViewBag.SubjectID = new SelectList(db.Subject, "Id", "Name", review.SubjectID);
            return View(review);
        }
        public ActionResult Menu(string dishType, string searchString)
        {
            ViewBag.Message = "Menu.";
            var TypeL = new List<string>();

            var TypeQry = from d in db.Dishes
                          orderby d.Category
                          select d.Category;

            TypeL.AddRange(TypeQry.Distinct());
            ViewBag.dishType = new SelectList(TypeL);
            var dishes = from d in db.Dishes
                         select d;

            if (!String.IsNullOrEmpty(searchString))
            {
                dishes = dishes.Where(s => s.Name.Contains(searchString) || s.Description.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(dishType))
            {
                dishes = dishes.Where(x => x.Category == dishType);
            }
            return View(dishes);
        }
        public ActionResult GetImageDish(int id)
        {
            Dishes dish = db.Dishes.FirstOrDefault(d => d.ID == id);
            if (dish.Image != null) return File(dish.Image, "image/png");
            return View();
        }
        public ActionResult Restaurants(string searchString)
        {
            ViewBag.Message = "Restaurants.";
            var rest = from m in db.Restaurant
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                rest = rest.Where(s => s.Name.Contains(searchString)|| s.Description.Contains(searchString));
            }
            return View(rest);
        }
        public ActionResult GetImageRest(int id)
        {
            Restaurant rest = db.Restaurant.FirstOrDefault(d => d.Id == id);
            if (rest.Image != null) return File(rest.Image, "image/png");
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