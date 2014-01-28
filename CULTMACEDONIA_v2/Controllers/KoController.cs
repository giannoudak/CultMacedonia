using CULTMACEDONIA_v2.Models.CultMacedoniaModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CULTMACEDONIA_v2.Controllers
{
    public class KoController : Controller
    {
        //
        // GET: /Ko/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserList(int? page, int pageSize = 10)
        {
            //Thread.Sleep(1000); //To demonstrate latency
            CultMacedoniaDBEntities db = new CultMacedoniaDBEntities();

            var pageNumber = page ?? 1;
            var itemList = new PagedList<Point>(db.Point.Include("PointImage").OrderBy(p=>p.PointId), pageNumber, pageSize);
            return Json(new { items = itemList, metaData = itemList.GetMetaData() }, JsonRequestBehavior.AllowGet);
        }
	}
}