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
    public class EmployeesController : Controller
    {
        private Session1Entities db = new Session1Entities();

        public EmployeesController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // POST: Employees
        [HttpPost]
        public ActionResult Index()
        {
            return Json(db.Employees.ToList());
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
