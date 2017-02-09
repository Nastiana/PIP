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
    [Authorize(Roles = "Manager")]
    public class TisketsController : Controller
    {
        private AirportsEntities db = new AirportsEntities();

        // GET: Tiskets
        [Authorize]
        public ActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var tiskets = db.Tiskets.Include(t => t.Flight);
            return View((tiskets.ToList()).ToPagedList(pageNumber, pageSize));
        }
        /*
        public ActionResult Index()
        {
            var tiskets = db.Tiskets.Include(t => t.Flight);
            return View(tiskets.ToList());

        }
        */
        // GET: Tiskets/Details/5
        [Authorize(Roles = "Manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new Exception("400");
            }
            Tiskets tiskets = db.Tiskets.Find(id);
            if (tiskets == null)
            {
                throw new Exception("404");
            }
            return View(tiskets);
        }

        // GET: Tiskets/Create
        [Authorize(Roles ="Manager")]
        public ActionResult Create()
        {
            ViewBag.Num_flight = new SelectList(db.Flight, "Num_flight", "Num_flight");
            return View();
        }

        // POST: Tiskets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult Create([Bind(Include = "Id,Num_flight,Sum_place,Sum_rezerved,Sum_bought")] Tiskets tiskets)
        {
            if (ModelState.IsValid)
            {
                db.Tiskets.Add(tiskets);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Num_flight = new SelectList(db.Flight, "Num_flight", "Num_flight", tiskets.Num_flight);
            return View(tiskets);
        }

        // GET: Tiskets/Edit/5
        [Authorize(Roles = "Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new Exception("400");
            }
            Tiskets tiskets = db.Tiskets.Find(id);
            if (tiskets == null)
            {
                throw new Exception("404");
            }
            ViewBag.Num_flight = new SelectList(db.Flight, "Num_flight", "Num_flight", tiskets.Num_flight);
            return View(tiskets);
        }

        // POST: Tiskets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult Edit([Bind(Include = "Id,Num_flight,Sum_place,Sum_rezerved,Sum_bought")] Tiskets tiskets)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiskets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Num_flight = new SelectList(db.Flight, "Num_flight", "Num_flight", tiskets.Num_flight);
            return View(tiskets);
        }

        // GET: Tiskets/Delete/5
        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new Exception("400");
            }
            Tiskets tiskets = db.Tiskets.Find(id);
            if (tiskets == null)
            {
                throw new Exception("404");
            }
            return View(tiskets);
        }

        // POST: Tiskets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            Tiskets tiskets = db.Tiskets.Find(id);
            db.Tiskets.Remove(tiskets);
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
