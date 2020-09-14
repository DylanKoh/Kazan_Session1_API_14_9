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
    public class AssetGroupsController : Controller
    {
        private Session1Entities db = new Session1Entities();

        // GET: AssetGroups
        public ActionResult Index()
        {
            return View(db.AssetGroups.ToList());
        }

        // GET: AssetGroups/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetGroup assetGroup = db.AssetGroups.Find(id);
            if (assetGroup == null)
            {
                return HttpNotFound();
            }
            return View(assetGroup);
        }

        // GET: AssetGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AssetGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] AssetGroup assetGroup)
        {
            if (ModelState.IsValid)
            {
                db.AssetGroups.Add(assetGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assetGroup);
        }

        // GET: AssetGroups/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetGroup assetGroup = db.AssetGroups.Find(id);
            if (assetGroup == null)
            {
                return HttpNotFound();
            }
            return View(assetGroup);
        }

        // POST: AssetGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] AssetGroup assetGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assetGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assetGroup);
        }

        // GET: AssetGroups/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetGroup assetGroup = db.AssetGroups.Find(id);
            if (assetGroup == null)
            {
                return HttpNotFound();
            }
            return View(assetGroup);
        }

        // POST: AssetGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AssetGroup assetGroup = db.AssetGroups.Find(id);
            db.AssetGroups.Remove(assetGroup);
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
