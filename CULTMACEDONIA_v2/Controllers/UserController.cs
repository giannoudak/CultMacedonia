using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CULTMACEDONIA_v2.Controllers
{
    [RoutePrefix("user")]
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }


       
        
        //
        // GET: /User/favorites
        [Route("favorites")]
        [Authorize(Roles="CultMacedoniaUser")]
        public ActionResult UserFavorites()
        {
            return View();
        }


        [Route("favorite_remove")]
        [Authorize(Roles="CultMacedoniaUser")]
        public ActionResult RemoveUserFavoriteSight(int pointId, string username, int userId, string pointName)
        {
            bool success = false;
            string message = string.Empty;


            return Json(new { success = success, message = message });
        }



      


	}
}