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
