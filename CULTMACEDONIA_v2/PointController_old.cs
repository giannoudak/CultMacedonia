using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CULTMACEDONIA_v2.Models.CultMacedoniaModel;

namespace CULTMACEDONIA_v2
{
    public class PointController_old : Controller
    {
        private CultMacedoniaDBEntities db = new CultMacedoniaDBEntities();

        // GET: /Point/
        public ActionResult Index()
        {
            var point = db.Point.Include(p => p.Category).Include(p => p.Era).Include(p => p.Ethnological).Include(p => p.Property).Include(p => p.ProtectionLevel).Include(p => p.Religion);
            return View(point.ToList());
        }

        // GET: /Point/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Point point = db.Point.Find(id);
            if (point == null)
            {
                return HttpNotFound();
            }
            return View(point);
        }

        // GET: /Point/Create
        public ActionResult Create()
        {
            ViewBag.PointCategoryId = new SelectList(db.Category, "CategoryId", "CategoryName");
            ViewBag.PointEraId = new SelectList(db.Era, "EraId", "EraName");
            ViewBag.PointEthnologicalId = new SelectList(db.Ethnological, "EthnologicalId", "EthnologicalName");
            ViewBag.PointPropertyId = new SelectList(db.Property, "PropertyId", "PropertyName");
            ViewBag.PointProtectionId = new SelectList(db.ProtectionLevel, "ProtectionId", "ProtectionName");
            ViewBag.PointReligionId = new SelectList(db.Religion, "ReligionId", "ReligionName");
            return View();
        }

        // POST: /Point/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PointId,PointName,PointDescription,PointX,PointY,PointYear,PointPlaceNomos,PointAddress,PointEmail,PointPhone,PointProtectionId,PointCategoryId,PointEraId,PointPropertyId,PointEthnologicalId,PointReligionId")] Point point)
        {
            if (ModelState.IsValid)
            {
                db.Point.Add(point);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PointCategoryId = new SelectList(db.Category, "CategoryId", "CategoryName", point.PointCategoryId);
            ViewBag.PointEraId = new SelectList(db.Era, "EraId", "EraName", point.PointEraId);
            ViewBag.PointEthnologicalId = new SelectList(db.Ethnological, "EthnologicalId", "EthnologicalName", point.PointEthnologicalId);
            ViewBag.PointPropertyId = new SelectList(db.Property, "PropertyId", "PropertyName", point.PointPropertyId);
            ViewBag.PointProtectionId = new SelectList(db.ProtectionLevel, "ProtectionId", "ProtectionName", point.PointProtectionId);
            ViewBag.PointReligionId = new SelectList(db.Religion, "ReligionId", "ReligionName", point.PointReligionId);
            return View(point);
        }

        // GET: /Point/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Point point = db.Point.Find(id);
            if (point == null)
            {
                return HttpNotFound();
            }
            ViewBag.PointCategoryId = new SelectList(db.Category, "CategoryId", "CategoryName", point.PointCategoryId);
            ViewBag.PointEraId = new SelectList(db.Era, "EraId", "EraName", point.PointEraId);
            ViewBag.PointEthnologicalId = new SelectList(db.Ethnological, "EthnologicalId", "EthnologicalName", point.PointEthnologicalId);
            ViewBag.PointPropertyId = new SelectList(db.Property, "PropertyId", "PropertyName", point.PointPropertyId);
            ViewBag.PointProtectionId = new SelectList(db.ProtectionLevel, "ProtectionId", "ProtectionName", point.PointProtectionId);
            ViewBag.PointReligionId = new SelectList(db.Religion, "ReligionId", "ReligionName", point.PointReligionId);
            return View(point);
        }

        // POST: /Point/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PointId,PointName,PointDescription,PointX,PointY,PointYear,PointPlaceNomos,PointAddress,PointEmail,PointPhone,PointProtectionId,PointCategoryId,PointEraId,PointPropertyId,PointEthnologicalId,PointReligionId")] Point point)
        {
            if (ModelState.IsValid)
            {
                db.Entry(point).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PointCategoryId = new SelectList(db.Category, "CategoryId", "CategoryName", point.PointCategoryId);
            ViewBag.PointEraId = new SelectList(db.Era, "EraId", "EraName", point.PointEraId);
            ViewBag.PointEthnologicalId = new SelectList(db.Ethnological, "EthnologicalId", "EthnologicalName", point.PointEthnologicalId);
            ViewBag.PointPropertyId = new SelectList(db.Property, "PropertyId", "PropertyName", point.PointPropertyId);
            ViewBag.PointProtectionId = new SelectList(db.ProtectionLevel, "ProtectionId", "ProtectionName", point.PointProtectionId);
            ViewBag.PointReligionId = new SelectList(db.Religion, "ReligionId", "ReligionName", point.PointReligionId);
            return View(point);
        }

        // GET: /Point/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Point point = db.Point.Find(id);
            if (point == null)
            {
                return HttpNotFound();
            }
            return View(point);
        }

        // POST: /Point/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Point point = db.Point.Find(id);
            db.Point.Remove(point);
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
