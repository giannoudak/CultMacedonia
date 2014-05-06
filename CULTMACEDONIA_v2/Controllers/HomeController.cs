using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

using CULTMACEDONIA_v2.Helpers;
using CULTMACEDONIA_v2.Models;
using CULTMACEDONIA_v2.Models.CultMacedoniaModel;

namespace CULTMACEDONIA_v2.Controllers
{
    //[RouteArea("Home")]
    //[Route("{action=Index}")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult test(string name, string password)
        {
            bool success = false;

            

            if (name == "giannoudak" && password == "123456")
                success = true;
            else
                success = false;

            return Json(new { success = success});
        }


        [Route("about")]
        public ActionResult About()
        {
            
            ViewBag.TItle = CultResources.Shared.About;

            return View();
        }

        [Route("contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        /// <summary>
        /// To Action που καλείται από τα action links για αλλαγή γλώσσας
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);
            ViewBag.CurrentCulture = lang;
            return Redirect(returnUrl);
        }


      

        

    }
}