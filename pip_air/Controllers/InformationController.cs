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
    public class InformationController : Controller
    {
        private AirportsEntities db = new AirportsEntities();

        // GET: Information
        [Authorize]
        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var information = db.Information.Include(i => i.Flight);
            //List<Information> flight = db.Information.ToList();
            return View((information.ToList()).ToPagedList(pageNumber, pageSize));
        }
        /*
        public ActionResult Index()
        {
            var information = db.Information.Include(i => i.Flight);
            return View(information.ToList());
        }*/

        // GET: Information/Details/5
        [Authorize(Roles = "Manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new Exception("400");
            }
            Information information = db.Information.Find(id);
            if (information == null)
            {
                throw new Exception("404");
            }
            return View(information);
        }

        // GET: Information/Create
        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            ViewBag.Num_flight = new SelectList(db.Flight, "Num_flight", "Num_flight");
            return View();
        }

        // POST: Information/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Num_flight,Departure_place,Arrival_place,Time_departure,Time_arrival")] Information information)
        {
           
            if (ModelState.IsValid)
            {
                db.Information.Add(information);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Num_flight = new SelectList(db.Flight, "Num_flight", "Num_flight", information.Num_flight);
            return View(information);

}

        // GET: Information/Edit/5
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new Exception("400");
            }
            Information information = db.Information.Find(id);
            if (information == null)
            {
                throw new Exception("404");
            }
            ViewBag.Num_flight = new SelectList(db.Flight, "Num_flight", "Num_flight", information.Num_flight);
            return View(information);
        }

        // POST: Information/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Num_flight,Departure_place,Arrival_place,Time_departure,Time_arrival")] Information information)
        {
            if (ModelState.IsValid)
            {
                db.Entry(information).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Num_flight = new SelectList(db.Flight, "Num_flight", "Num_flight", information.Num_flight);
            return View(information);
        }

        // GET: Information/Delete/5
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new Exception("400");
            }
            Information information = db.Information.Find(id);
            if (information == null)
            {
                throw new Exception("404");
            }
            return View(information);
        }

        // POST: Information/Delete/5
        [Authorize(Roles = "Manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Information information = db.Information.Find(id);
            db.Information.Remove(information);
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
