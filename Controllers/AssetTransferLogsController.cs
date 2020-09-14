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
    public class AssetTransferLogsController : Controller
    {
        private Session1Entities db = new Session1Entities();

        // GET: AssetTransferLogs
        public ActionResult Index()
        {
            var assetTransferLogs = db.AssetTransferLogs.Include(a => a.Asset).Include(a => a.DepartmentLocation).Include(a => a.DepartmentLocation1);
            return View(assetTransferLogs.ToList());
        }

        // GET: AssetTransferLogs/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetTransferLog assetTransferLog = db.AssetTransferLogs.Find(id);
            if (assetTransferLog == null)
            {
                return HttpNotFound();
            }
            return View(assetTransferLog);
        }

        // GET: AssetTransferLogs/Create
        public ActionResult Create()
        {
            ViewBag.AssetID = new SelectList(db.Assets, "ID", "AssetSN");
            ViewBag.FromDepartmentLocationID = new SelectList(db.DepartmentLocations, "ID", "ID");
            ViewBag.ToDepartmentLocationID = new SelectList(db.DepartmentLocations, "ID", "ID");
            return View();
        }

        // POST: AssetTransferLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AssetID,TransferDate,FromAssetSN,ToAssetSN,FromDepartmentLocationID,ToDepartmentLocationID")] AssetTransferLog assetTransferLog)
        {
            if (ModelState.IsValid)
            {
                db.AssetTransferLogs.Add(assetTransferLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssetID = new SelectList(db.Assets, "ID", "AssetSN", assetTransferLog.AssetID);
            ViewBag.FromDepartmentLocationID = new SelectList(db.DepartmentLocations, "ID", "ID", assetTransferLog.FromDepartmentLocationID);
            ViewBag.ToDepartmentLocationID = new SelectList(db.DepartmentLocations, "ID", "ID", assetTransferLog.ToDepartmentLocationID);
            return View(assetTransferLog);
        }

        // GET: AssetTransferLogs/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetTransferLog assetTransferLog = db.AssetTransferLogs.Find(id);
            if (assetTransferLog == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssetID = new SelectList(db.Assets, "ID", "AssetSN", assetTransferLog.AssetID);
            ViewBag.FromDepartmentLocationID = new SelectList(db.DepartmentLocations, "ID", "ID", assetTransferLog.FromDepartmentLocationID);
            ViewBag.ToDepartmentLocationID = new SelectList(db.DepartmentLocations, "ID", "ID", assetTransferLog.ToDepartmentLocationID);
            return View(assetTransferLog);
        }

        // POST: AssetTransferLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AssetID,TransferDate,FromAssetSN,ToAssetSN,FromDepartmentLocationID,ToDepartmentLocationID")] AssetTransferLog assetTransferLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assetTransferLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssetID = new SelectList(db.Assets, "ID", "AssetSN", assetTransferLog.AssetID);
            ViewBag.FromDepartmentLocationID = new SelectList(db.DepartmentLocations, "ID", "ID", assetTransferLog.FromDepartmentLocationID);
            ViewBag.ToDepartmentLocationID = new SelectList(db.DepartmentLocations, "ID", "ID", assetTransferLog.ToDepartmentLocationID);
            return View(assetTransferLog);
        }

        // GET: AssetTransferLogs/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssetTransferLog assetTransferLog = db.AssetTransferLogs.Find(id);
            if (assetTransferLog == null)
            {
                return HttpNotFound();
            }
            return View(assetTransferLog);
        }

        // POST: AssetTransferLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            AssetTransferLog assetTransferLog = db.AssetTransferLogs.Find(id);
            db.AssetTransferLogs.Remove(assetTransferLog);
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
