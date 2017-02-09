using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using pip_air.Models;
using PagedList;
using System.Collections.Generic;

namespace pip_air.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private Entities db = new Entities();

        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        // Получение json объекта 
        public ActionResult GetUsers()
        {
            // возвращаем коллекцию анонимных объектов (в Select) в формате json
            return Json(db.AspNetUsers.Select(item => new
            {
                id = item.Id,
                email = item.Email,
                userName = item.UserName,
                role = item.AspNetRoles.FirstOrDefault().Name
            }), JsonRequestBehavior.AllowGet);
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AspNetUsers aspNetUser)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(aspNetUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspNetUser);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }

            ViewBag.roles = db.AspNetRoles.ToList();

            return View(aspNetUser);
        }

        // POST: Users/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AspNetUsers aspNetUser)
        {
            if (ModelState.IsValid)
            {
                var role = Request.Form["Role"].ToString();
                db.Entry(aspNetUser).State = EntityState.Modified;
                db.Database.ExecuteSqlCommand($@"
                    UPDATE [AspNetUserRoles]
                    SET [AspNetUserRoles].[RoleId] = (
                        SELECT [Id]
                        FROM [AspNetRoles]
                        WHERE [AspNetRoles].[Name] = '{role}'
                    )
                    WHERE [AspNetUserRoles].[UserId] = '{aspNetUser.Id}'
                ");
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetUser);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUsers aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUsers aspNetUser = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUser);
            db.SaveChanges();
            return RedirectToAction("Index");

           // AspNetUsers aspNetUser = db.AspNetUsers.Find(id);
//db.AspNetUsers.Remove(aspNetUser);
          //  db.SaveChanges();
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