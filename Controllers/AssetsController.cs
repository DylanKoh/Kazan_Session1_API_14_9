using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Kazan_Session1_API_14_9;

namespace Kazan_Session1_API_14_9.Controllers
{
    public class AssetsController : Controller
    {
        private Session1Entities db = new Session1Entities();

        public AssetsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }
        // POST: Assets
        [HttpPost]
        public ActionResult Index()
        {
            var assets = db.Assets;
            return Json(assets.ToList());
        }

        // POST: Assets/Details/5
        [HttpPost]
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return Json(asset);
        }

        // POST: Assets/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,AssetSN,AssetName,DepartmentLocationID,EmployeeID,AssetGroupID,Description,WarrantyDate")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                var findAsset = (from x in db.Assets
                                 where x.AssetName == asset.AssetName && x.DepartmentLocationID == asset.DepartmentLocationID
                                 select x).FirstOrDefault();
                if (findAsset != null)
                {
                    return Json("Asset already exist in the location of choice!");
                }
                else
                {
                    db.Assets.Add(asset);
                    db.SaveChanges();
                    return Json("Created Asset!");
                }
                
            }

            return Json("Unable to create Asset! Please check and try again!");
        }

        // POST: Assets/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,AssetSN,AssetName,DepartmentLocationID,EmployeeID,AssetGroupID,Description,WarrantyDate")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asset).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Successfully edited Asset!");
            }
            return Json("An error occured while editing Asset! Please check and try again!");
        }

        // POST: Assests/GetCustomView
        [HttpPost]
        public ActionResult GetCustomView()
        {
            var getAssetsCustom = (from x in db.Assets
                                   join y in db.AssetGroups on x.AssetGroupID equals y.ID
                                   join z in db.DepartmentLocations on x.DepartmentLocationID equals z.ID
                                   where z.EndDate == null
                                   join a in db.Departments on z.DepartmentID equals a.ID
                                   select new
                                   {
                                       AssetID = x.ID,
                                       AssetName = x.AssetName,
                                       DepartmentName = a.Name,
                                       AssetSN = x.AssetSN,
                                       WarrantyDate = x.WarrantyDate,
                                       AssetGroup = y.Name
                                   });
            return Json(getAssetsCustom.ToList());
        }

        // POST: Assets/GetAllSN
        [HttpPost]
        public ActionResult GetAllSN()
        {
            var listOfSN = new List<string>();
            listOfSN = (from x in db.Assets
                        select x.AssetSN).ToList();
            listOfSN.AddRange((from x in db.AssetTransferLogs
                               select x.FromAssetSN).ToList());
            listOfSN.AddRange((from x in db.AssetTransferLogs
                               select x.ToAssetSN).ToList());
            return Json(listOfSN.Distinct());
        }

        // POST: Assets/GetAssetID?AssetSN={}
        [HttpPost]
        public ActionResult GetAssetID(string AssetSN)
        {
            var getAssetID = (from x in db.Assets
                              where x.AssetSN == AssetSN
                              select x.ID).FirstOrDefault();
            return Json(getAssetID);
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
