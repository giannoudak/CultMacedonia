using CULTMACEDONIA_v2.Models.CultMacedoniaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CULTMACEDONIA_v2.Controllers
{
    [RoutePrefix("user")]
    public class UserController : Controller
    {


        /// <summary>
        /// Τhe DataBase context
        /// </summary>
        CultMacedoniaDBEntities db = new CultMacedoniaDBEntities();

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
        public ActionResult RemoveUserFavoriteSight(int pointId, string username, string userId, string pointName)
        {
            bool success = false;
            string message = string.Empty;



            // Διαβάζουμε το id του τρέχον χρήστη
            // Αν έχουμε συνδεδμένο χρήστη τότε
            if (Request.IsAuthenticated)
            {
                
                // βρισκουμε το favorite που θέλουμε να διαγράψουμε
                UserFavorites userFav = (from uf in db.UserFavorites
                              where uf.Id == userId && uf.PointId == pointId
                              select uf).SingleOrDefault();

                // αν δεν ειναι null προχωράμε στην διαγραφή
                if (userFav!=null){
                    db.UserFavorites.Remove(userFav);

                    // κάνουμε αποθήκευση των αλλαγών
                    try
                    {
                        db.SaveChanges();
                        success = true;
                        message = CultResources.View.unFavoriteSuccess;
                    }
                    catch (Exception ex)
                    {
                        success = false;
                        message = CultResources.View.unFavoriteFailure;
                    }
                }
                else
                {
                    success = false;
                    message = CultResources.View.unFavoriteFailure;
                }
                

               
            }
            else
            {
                success = false;
                message = new HttpStatusCodeResult(HttpStatusCode.Unauthorized).StatusDescription;
            }



            return Json(new { success = success, message = message });
        }



      


	}
}