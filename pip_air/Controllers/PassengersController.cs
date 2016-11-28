using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using pip_air.Models;

namespace pip_air.Controllers
{
    [Filters.ActionFilter]
    [Filters.ExceptionFilter]
    [Authorize(Roles = "Manager")]
    public class PassengersController : Controller
    {
        private AirportsEntities db = new AirportsEntities();

        // GET: Passengers
        public ActionResult Index()
        {
            var passenger = db.Passenger.Include(p => p.Flight);
            return View(passenger.ToList());
        }

        // GET: Passengers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new Exception("400");
            }
            Passenger passenger = db.Passenger.Find(id);
            if (passenger == null)
            {
                throw new Exception("404");
            }
            return View(passenger);
        }

        // GET: Passengers/Create
        public ActionResult Create()
        {
            ViewBag.Num_flight = new SelectList(db.Flight, "Num_flight", "Num_flight");
            return View();
        }

        // POST: Passengers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Num_flight,FIO,Num_pasport")] Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                db.Passenger.Add(passenger);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Num_flight = new SelectList(db.Flight, "Num_flight", "Num_flight", passenger.Num_flight);
            return View(passenger);
        }
        // GET: Passengers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new Exception("400");
            }
            Passenger passenger = db.Passenger.Find(id);
            if (passenger == null)
            {
                throw new Exception("404");
            }
            ViewBag.Num_flight = new SelectList(db.Flight, "Num_flight", "Num_flight", passenger.Num_flight);
            return View(passenger);
        }

        // POST: Passengers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Num_flight,FIO,Num_pasport")] Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                db.Entry(passenger).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Num_flight = new SelectList(db.Flight, "Num_flight", "Num_flight", passenger.Num_flight);
            return View(passenger);
        }

        // GET: Passengers/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new Exception("400");
            }
            Passenger passenger = db.Passenger.Find(id);
            if (passenger == null)
            {
                throw new Exception("404");
            }
            return View(passenger);
        }

        // POST: Passengers/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Passenger passenger = db.Passenger.Find(id);
            db.Passenger.Remove(passenger);
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
