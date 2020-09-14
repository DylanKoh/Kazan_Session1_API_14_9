using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kazan_Session1_API_14_9;

namespace Kazan_Session1_API_14_9.Controllers
{
    public class DepartmentLocationsController : Controller
    {
        private Session1Entities db = new Session1Entities();

        // GET: DepartmentLocations
        public ActionResult Index()
        {
            var departmentLocations = db.DepartmentLocations.Include(d => d.Department).Include(d => d.Location);
            return View(departmentLocations.ToList());
        }

        // GET: DepartmentLocations/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentLocation departmentLocation = db.DepartmentLocations.Find(id);
            if (departmentLocation == null)
            {
                return HttpNotFound();
            }
            return View(departmentLocation);
        }

        // GET: DepartmentLocations/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name");
            ViewBag.LocationID = new SelectList(db.Locations, "ID", "Name");
            return View();
        }

        // POST: DepartmentLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DepartmentID,LocationID,StartDate,EndDate")] DepartmentLocation departmentLocation)
        {
            if (ModelState.IsValid)
            {
                db.DepartmentLocations.Add(departmentLocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name", departmentLocation.DepartmentID);
            ViewBag.LocationID = new SelectList(db.Locations, "ID", "Name", departmentLocation.LocationID);
            return View(departmentLocation);
        }

        // GET: DepartmentLocations/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentLocation departmentLocation = db.DepartmentLocations.Find(id);
            if (departmentLocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name", departmentLocation.DepartmentID);
            ViewBag.LocationID = new SelectList(db.Locations, "ID", "Name", departmentLocation.LocationID);
            return View(departmentLocation);
        }

        // POST: DepartmentLocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DepartmentID,LocationID,StartDate,EndDate")] DepartmentLocation departmentLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departmentLocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "ID", "Name", departmentLocation.DepartmentID);
            ViewBag.LocationID = new SelectList(db.Locations, "ID", "Name", departmentLocation.LocationID);
            return View(departmentLocation);
        }

        // GET: DepartmentLocations/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentLocation departmentLocation = db.DepartmentLocations.Find(id);
            if (departmentLocation == null)
            {
                return HttpNotFound();
            }
            return View(departmentLocation);
        }

        // POST: DepartmentLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            DepartmentLocation departmentLocation = db.DepartmentLocations.Find(id);
            db.DepartmentLocations.Remove(departmentLocation);
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
