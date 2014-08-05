using CULTMACEDONIA_v2.Models;
using CULTMACEDONIA_v2.Models.CultMacedoniaModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CULTMACEDONIA_v2.Controllers
{

    [Authorize(Roles = "CultMacedoniaUser")]
    public class UserFavoritesController : ApiController
    {

        /// <summary>
        /// Τhe DataBase context
        /// </summary>
        CultMacedoniaDBEntities db = new CultMacedoniaDBEntities();
        // GET api/<controller>
        public IEnumerable<PointUserFavoriteViewModel> Get()
        {

            // get current user
            // Διαβάζουμε το όνομα του συνδεδεμένου χρήστη
            var currUser = User.Identity.Name;


            var q = from p in db.Point.Include("PointImage")
                    join uf in db.UserFavorites on p.PointId equals uf.PointId
                    where uf.AspNetUsers.UserName == currUser
                    
                    select new PointUserFavoriteViewModel
                    {

                        PointId = p.PointId,
                        PointName = p.PointName,
                        OneImageName = p.PointImage.FirstOrDefault().ImageFileName,
                        OneImagePath = p.PointImage.FirstOrDefault().ImageFilePath,
                        UserId = uf.Id,
                        UserName = uf.AspNetUsers.UserName
                    };

           

            return q;
        }
    }
}
