using CULTMACEDONIA_v2.Models.CultMacedoniaModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CULTMACEDONIA_v2.Controllers
{
    

    public class UnobtrusiveAjaxController : Controller
    {
        CultMacedoniaDBEntities db = new CultMacedoniaDBEntities();

        public ActionResult Index(int? page)
        {


            var listPaged = GetPagedNames(page); // GetPagedNames is found in BaseController
            if (listPaged == null)
                return HttpNotFound();

            ViewBag.Points = listPaged;
            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("UnobtrusiveAjax_Partial")
                : View();
        }





        protected IPagedList<Point> GetPagedNames(int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            // retrieve list from database/whereverand
            var listUnpaged = db.Point.Include("PointImage").OrderBy(p => p.PointId);

            // page the list
            const int pageSize = 6;
            var listPaged = listUnpaged.ToPagedList(page ?? 1, pageSize);

            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;

            return listPaged;
        }

    }
}