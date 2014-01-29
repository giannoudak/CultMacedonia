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
    public class PointsAdminController : ApiController
    {



        /// <summary>
        /// Τhe DataBase context
        /// </summary>
        CultMacedoniaDBEntities db = new CultMacedoniaDBEntities();


        // GET api/<controller>
        public IEnumerable<PointGridViewModel> Get(byte? active = 1)
        {
            var q = from p in db.Point.Include("PointImage").Include("AspNetUsers")
                    join u in db.PointOfUser on p.PointId equals u.PointId 
                        
                    select new PointGridViewModel
                    {

                        
                        PointId = p.PointId,
                        PlaceName = p.PointName,
                        PlaceNomos = p.PointPlaceNomos,
                        PlaceAddress = p.PointAddress,
                        OneImageName = p.PointImage.FirstOrDefault().ImageFileName,
                        OneImagePath = p.PointImage.FirstOrDefault().ImageFilePath,

                        
                        PointCategoryId = p.PointCategoryId,
                        category = p.Category.CategoryName,

                        PointEraId = p.PointEraId,
                        era = p.Era.EraName,

                        PointEthnologicalId = p.PointEthnologicalId,
                        ethnological = p.Ethnological.EthnologicalName,

                        PointPropertyId = p.PointPropertyId,
                        property = p.Property.PropertyName,

                        PointProtectionId = p.PointProtectionId,
                        protection = p.ProtectionLevel.ProtectionName,

                        PointReligionId = p.PointReligionId,
                        religion = p.Religion.ReligionName,

                        
                        year = p.PointYear,

                        isActivate = u.isActivated,
                        dateActivated = u.DateActivated,
                        dateAdded  = u.DateAdded,
                        UserName = u.AspNetUsers.UserName
                        

                        
                    };

            // by default επιστρέφουμε εκεινα τα points που ειναι activate
            if (active == 1) 
                q = q.Where(p => p.isActivate == 1);
            else 
                q = q.Where(p => p.isActivate == 0);

      

            return q;
        }

        public IEnumerable<PointGridViewModel> Get(string term, string addr, int? year,byte? active)
        {
            var q = from p in db.Point.Include("PointImage").Include("AspNetUsers")
                    join u in db.PointOfUser on p.PointId equals u.PointId

                    select new PointGridViewModel
                    {


                        PointId = p.PointId,
                        PlaceName = p.PointName,
                        PlaceNomos = p.PointPlaceNomos,
                        PlaceAddress = p.PointAddress,
                        OneImageName = p.PointImage.FirstOrDefault().ImageFileName,
                        OneImagePath = p.PointImage.FirstOrDefault().ImageFilePath,


                        PointCategoryId = p.PointCategoryId,
                        category = p.Category.CategoryName,

                        PointEraId = p.PointEraId,
                        era = p.Era.EraName,

                        PointEthnologicalId = p.PointEthnologicalId,
                        ethnological = p.Ethnological.EthnologicalName,

                        PointPropertyId = p.PointPropertyId,
                        property = p.Property.PropertyName,

                        PointProtectionId = p.PointProtectionId,
                        protection = p.ProtectionLevel.ProtectionName,

                        PointReligionId = p.PointReligionId,
                        religion = p.Religion.ReligionName,


                        year = p.PointYear,
                        isActivate = u.isActivated,
                        dateActivated = u.DateActivated,
                        dateAdded = u.DateAdded,
                        UserName = u.AspNetUsers.UserName

                    };

            if (active != null)
            {
                q = q.Where(p => p.isActivate == active);
            }

            if (!string.IsNullOrEmpty(term))
            {
                q = q.Where(p => p.PlaceName.Contains(term));
            }

            if (!string.IsNullOrEmpty(addr))
            {
                q = q.Where(p => p.PlaceAddress.Contains(addr));
            }

            return q;
        }






        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
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