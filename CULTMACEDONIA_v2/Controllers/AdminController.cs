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

        public ActionResult vCategoryListPartial()
        {
            var q = from c in db.Category
                    select new
                    {
                        c.CategoryId,
                        c.CategoryName
                    };

            return Json(q.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllProperties()
        {
            var q = from c in db.Property
                    select new
                    {
                        c.PropertyId,
                        c.PropertyName
                    };

            return Json(q.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllEras()
        {
            var q = from c in db.Era
                    select new
                    {
                        c.EraId,
                        c.EraName
                    };

            return Json(q.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllProtectionLevels()
        {
            var q = from c in db.ProtectionLevel
                    select new
                    {
                        c.ProtectionId,
                        c.ProtectionName
                    };

            return Json(q.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllEthnological()
        {
            var q = from c in db.Ethnological
                    select new
                    {
                        c.EthnologicalId,
                        c.EthnologicalName
                    };

            return Json(q.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllReligion()
        {
            var q = from c in db.Religion
                    select new
                    {
                        c.ReligionId,
                        c.ReligionName
                    };

            return Json(q.ToList(), JsonRequestBehavior.AllowGet);
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
