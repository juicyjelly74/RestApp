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
    public class CardsController : Controller
    {
        private RestaurantUserDatabaseEntities db = new RestaurantUserDatabaseEntities();

        // GET: Cards
        public ActionResult Index()
        {
            var cards = db.Card.Include(c => c.Dishes).Include(c => c.Restaurant_Users);

            if (Session["UserID"] != null)
            {
                int usid = (int)Session["UserID"];
                cards = cards.Where(s => s.UserID == usid);
            }
            return View(cards);
        }

        // GET: Cards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Card.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // GET: Cards/Create
        public ActionResult Create()
        {
            ViewBag.DishID = new SelectList(db.Dishes, "ID", "Name");
            ViewBag.UserID = new SelectList(db.Restaurant_Users, "Id", "Username");
            return View();
        }

        // POST: Cards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DishID,UserID,Status,Ammount")] Card card)
        {
            if (ModelState.IsValid)
            {
                db.Card.Add(card);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DishID = new SelectList(db.Dishes, "ID", "Name", card.DishID);
            ViewBag.UserID = new SelectList(db.Restaurant_Users, "Id", "Username", card.UserID);
            return View(card);
        }

        // GET: Cards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Card.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            ViewBag.DishID = new SelectList(db.Dishes, "ID", "Name", card.DishID);
            ViewBag.UserID = new SelectList(db.Restaurant_Users, "Id", "Username", card.UserID);
            return View(card);
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DishID,UserID,Status,Ammount")] Card card)
        {
            if (ModelState.IsValid)
            {
                db.Entry(card).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DishID = new SelectList(db.Dishes, "ID", "Name", card.DishID);
            ViewBag.UserID = new SelectList(db.Restaurant_Users, "Id", "Username", card.UserID);
            return View(card);
        }

        // GET: Cards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.Card.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Card card = db.Card.Find(id);
            db.Card.Remove(card);
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
