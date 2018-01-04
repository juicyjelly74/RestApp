using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestApp.Models;

namespace RestApp.Controllers
{
    public class DishesController : Controller
    {
        private DishDBContext db = new DishDBContext();

        // GET: Dishes
        public ActionResult Index()
        {
            return View(db.Dishes.ToList());
        }
        public ActionResult GetImage(int id)
        {
            Dish dish = db.Dishes
               .FirstOrDefault(d => d.ID == id);
           return File(dish.Image, "image/png");
        }
        public ActionResult Index(string sortOrder, string currentFilter, string searchProducerString,
string searchModelString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            List<SelectListItem> carModels = new List<SelectListItem>();

            foreach (var model in db.Models)
            {
                var p = db.Producers.Where(c => c.IdProducer == model.IdModelProducer);
                foreach (var i in p)
                {
                    carModels.Add(new SelectListItem() { Text = model.NameModel + i.NameProducer });
                }

            }
            ViewBag.CarModels = db.Models;

            ViewBag.ProducerModels = carModels;


            var cars = db.Cars.Include(c => c.Models).Include(c => c.Producers);
            if (searchProducerString != null || searchModelString != null)
            {
                page = 1;
            }
            else
            {
                searchProducerString = currentFilter;
            }

            ViewBag.CurrentFilter = searchProducerString;
            if (!String.IsNullOrEmpty(searchProducerString))
            {

                cars = cars.Where(c => c.Producers.NameProducer.Contains(searchProducerString));
            }

            if (!String.IsNullOrEmpty(searchModelString))
            {

                cars = cars.Where(c => c.Models.NameModel.Contains(searchModelString));
            }

            switch (sortOrder)
            {
                case "price_desc":
                    cars = cars.OrderByDescending(s => s.Price);
                    break;
                default:
                    cars = cars.OrderBy(s => s.Price);
                    break;
            }

            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(cars.ToPagedList(pageNumber, pageSize));
        }
        // GET: Dishes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // GET: Dishes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dishes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Price,Image,Category")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                db.Dishes.Add(dish);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dish);
        }

        // GET: Dishes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // POST: Dishes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Price,Image,Category")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dish).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dish);
        }

        // GET: Dishes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dish dish = db.Dishes.Find(id);
            if (dish == null)
            {
                return HttpNotFound();
            }
            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dish dish = db.Dishes.Find(id);
            db.Dishes.Remove(dish);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
