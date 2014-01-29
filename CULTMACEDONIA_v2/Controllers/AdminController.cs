using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CULTMACEDONIA_v2.Models.CultMacedoniaModel;

namespace CULTMACEDONIA_v2.Controllers
{
    public class AdminController : Controller
    {
        private CultMacedoniaDBEntities db = new CultMacedoniaDBEntities();

        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Admin/
        public ActionResult Points(bool enabled = true)
        {
            if (enabled) return View();
            else return View("Points_new");
        }

        public ActionResult settings()
        {
            return View();
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
