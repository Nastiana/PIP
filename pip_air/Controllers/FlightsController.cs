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


namespace pip_air.Controllers
{
    [Filters.ActionFilter]
    [Filters.ExceptionFilter]
    public class FlightsController : Controller
    {
        private AirportsEntities db = new AirportsEntities();

        // GET: Flights
        [Authorize]
        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            List<Flight> flight = db.Flight.ToList();
            return View(flight.ToPagedList(pageNumber, pageSize));
        }
      /*  public ActionResult Index()
        {   
            return View(db.Flight.ToList());
        }*/

        // GET: Flights/Details/5
        [Authorize(Roles = "Manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new Exception("400");
            }
            Flight flight = db.Flight.Find(id);
            if (flight == null)
            {
                throw new Exception("404");
            }
            return View(flight);
        }

        // GET: Flights/Create
        [Authorize (Roles="Manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult Create([Bind(Include = "Num_flight,Airliner,Name_airport")] Flight flight)
        {
           try
                {
                    if (ModelState.IsValid)
                    {
                        db.Flight.Add(flight);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(flight);
                }
                catch (Exception ex)
                {
                    throw new Exception("410");
                }
            }
        

        // GET: Flights/Edit/5
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new Exception("400");
            }
            Flight flight = db.Flight.Find(id);
            if (flight == null)
            {
                throw new Exception("404");
            }
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult Edit([Bind(Include = "Num_flight,Airliner,Name_airport")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flight);
        }

        // GET: Flights/Delete/5
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new Exception("400");
            }
            Flight flight = db.Flight.Find(id);
            if (flight == null)
            {
                throw new Exception("404");
            }
            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            try {
            Flight flight = db.Flight.Find(id);
            db.Flight.Remove(flight);
            db.SaveChanges();
            return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw new Exception("403");
            }

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
