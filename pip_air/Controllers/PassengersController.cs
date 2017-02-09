using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using pip_air.Models;
using PagedList.Mvc;
using PagedList;
using System.Data.SqlClient;

namespace pip_air.Controllers
{
    [Filters.ActionFilter]
    [Filters.ExceptionFilter]
    public class PassengersController : Controller
    {
        private AirportsEntities db = new AirportsEntities();

        // GET: Passengers
        [Authorize(Roles = "Manager")]
        public ActionResult Index()
        {
            
            return View();
        }
        /* public ActionResult Index()
         {
             var passenger = db.Passenger.Include(p => p.Flight);
             return View(passenger.ToList());
         }
        // Получение json объекта 
        public ActionResult GetPassengers()
        {
            // возвращаем коллекцию анонимных объектов (в Select) в формате json
            return Json(db.Passenger.Select(item => new
            {
                Id = item.Id,
                Num_flight = item.Num_flight,
                FIO = item.FIO,
                Num_pasport = item.Num_pasport
            }), JsonRequestBehavior.AllowGet);
        }*/
        public ActionResult PassengersData(string id)
        {
            
            var passenger = db.Passenger.Include(p => p.Flight);
            return View((passenger.ToList()));
        }
        // GET: Passengers/Details/5
        [Authorize(Roles = "Manager")]
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
        public ActionResult Create(int? id)
        {
            ViewBag.flightId = id ?? null;
            ViewBag.Num_flight = new SelectList(db.Flight, "Num_flight", "Num_flight");
            return View();
        }

        // POST: Passengers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Num_flight,FIO,Num_pasport")] Passenger passenger)
        {try { 
            if (ModelState.IsValid)
            {
                db.Passenger.Add(passenger);
                db.SaveChanges();
                if (User.IsInRole("User")) { 
                return RedirectToAction("Index", "Flights");}
                else return RedirectToAction("Index");
            }
            
            ViewBag.Num_flight = new SelectList(db.Flight, "Num_flight", "Num_flight", passenger.Num_flight);
            return View(passenger);
        }
                catch (Exception ex)
                {
                    throw new Exception("410");
    }
}
      
        // GET: Passengers/Edit/5
        [Authorize(Roles = "Manager")]
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
        [Authorize(Roles = "Manager")]
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
        [Authorize(Roles = "Manager")]
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
        [Authorize(Roles = "Manager")]
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
