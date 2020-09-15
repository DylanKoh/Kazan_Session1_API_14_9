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

        public AssetTransferLogsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // POST: AssetTransferLogs
        [HttpPost]
        public ActionResult Index()
        {
            var assetTransferLogs = db.AssetTransferLogs;
            return Json(assetTransferLogs.ToList());
        }

        // POST: AssetTransferLogs/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,AssetID,TransferDate,FromAssetSN,ToAssetSN,FromDepartmentLocationID,ToDepartmentLocationID")] AssetTransferLog assetTransferLog)
        {
            if (ModelState.IsValid)
            {
                db.AssetTransferLogs.Add(assetTransferLog);
                db.SaveChanges();
                return Json("Completed Transfer!");
            }

            return Json("Unable to transfer Asset! Please check and try again!");
        }

        // POST: AssetTransferLogs/GetCustomHistory?AssetID={}
        [HttpPost]
        public ActionResult GetCustomHistory(long AssetID)
        {
            var findHistory = (from x in db.AssetTransferLogs
                               where x.AssetID == AssetID
                               select new
                               {
                                   TransferDate = x.TransferDate,
                                   OldDepartment = db.Departments.Where(z => z.ID == (db.DepartmentLocations.Where(y => y.ID == x.FromDepartmentLocationID).Select(y => y.DepartmentID)).FirstOrDefault()).Select(z => z.Name).FirstOrDefault(),
                                   OldAssetSN = x.FromAssetSN,
                                   NewDepartment = db.Departments.Where(z => z.ID == (db.DepartmentLocations.Where(y => y.ID == x.ToDepartmentLocationID).Select(y => y.DepartmentID)).FirstOrDefault()).Select(z => z.Name).FirstOrDefault(),
                                   NewAssetSN = x.ToAssetSN
                               }).ToList();
            return Json(findHistory);
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
