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
    public class AssetPhotoesController : Controller
    {
        private Session1Entities db = new Session1Entities();

        public AssetPhotoesController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }
        // POST: AssetPhotoes
        [HttpPost]
        public ActionResult Index()
        {
            var assetPhotos = db.AssetPhotos;
            return Json(assetPhotos.ToList());
        }

        
        // POST: AssetPhotoes/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,AssetID,AssetPhoto1")] AssetPhoto assetPhoto)
        {
            if (ModelState.IsValid)
            {
                var findPhoto = (from x in db.AssetPhotos
                                 where x.AssetID == assetPhoto.AssetID && x.AssetPhoto1 == assetPhoto.AssetPhoto1
                                 select x).FirstOrDefault();
                if (findPhoto != null)
                {
                    return Json("Photo(s) saved successfully!");
                }
                else
                {
                    db.AssetPhotos.Add(assetPhoto);
                    db.SaveChanges();
                    return Json("Photo(s) saved successfully!");
                }
                
               
            }

            
            return Json("Unable to save photo(s)! Please try again later!");
        }


        // POST: AssetPhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            AssetPhoto assetPhoto = db.AssetPhotos.Find(id);
            db.AssetPhotos.Remove(assetPhoto);
            db.SaveChanges();
            return Json("Deleted photo!");
        }

        // POST: AssetPhotoes/GetPhotoAsset?AssetID={}
        [HttpPost]
        public ActionResult GetPhotoAsset(long AssetID)
        {
            var findAssetPhoto = (from x in db.AssetPhotos
                                  where x.AssetID == AssetID
                                  select x);
            return Json(findAssetPhoto.ToList());
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
