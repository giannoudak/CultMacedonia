using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CULTMACEDONIA_v2.Models.CultMacedoniaModel;

public class CategoryVM
    {
        public int id { get; set; }
        public string name { get; set; }
        public string lang { get; set; }
       
    }

namespace CULTMACEDONIA_v2.Controllers
{
    

    public class AppController : Controller
    {

        /// <summary>
        /// Τhe DataBase context
        /// </summary>
        CultMacedoniaDBEntities db = new CultMacedoniaDBEntities();

        //
        // GET: /App/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllCategories()
        {

            var q = (from c in db.Category
                     select new CategoryVM
                     {
                         id = c.CategoryId,
                         name = c.CategoryName,
                         lang = c.Lang
                     }).Take(3).ToList();
            
            return Json(q, JsonRequestBehavior.AllowGet);
            
        }
            

       
	}

}