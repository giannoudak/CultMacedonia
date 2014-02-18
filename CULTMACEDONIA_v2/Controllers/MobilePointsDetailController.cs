using CULTMACEDONIA_v2.Models;
using CULTMACEDONIA_v2.Models.CultMacedoniaModel;
using CULTMACEDONIA_v2.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;


namespace CULTMACEDONIA_v2.Controllers
{
    [RoutePrefix("api/sights")]
    public class MobilePointsDetailController : ApiController
    {

        /// <summary>
        /// Τhe DataBase context
        /// </summary>
        CultMacedoniaDBEntities db = new CultMacedoniaDBEntities();


        // GET api/<controller>
        [Route("")]
        public IQueryable<PointDetailDto> GetSights()
        {
            var sights = from p in db.Point.Include("PointImage").Include("PointVideo").Include("AspNetUsers")
                    join u in db.PointOfUser on p.PointId equals u.PointId
                    where u.isActivated == (byte)1
                    select new PointDetailDto {
                        PointId = p.PointId,
                        
                        PointName = p.PointName,
                        PointX = p.PointX,
                        PointY = p.PointY,
                        
                        PointYear = (p.PointYear == null) ? 0 : p.PointYear,
                        
                        PointPlaceNomos = (p.PointPlaceNomos == null) ? "-" : p.PointPlaceNomos,
                        PointDescription = p.PointDescription,
                        PointFoto = (p.PointImage.FirstOrDefault().ImageFilePath == null) ? "http://www.cult-macedonia.com/myData/img/default_picture.png" : p.PointImage.FirstOrDefault().ImageFilePath,
                        
                        PointCategoryId = p.Category.CategoryName,
                        PointEraId = p.Era.EraName,
                        PointEthnologicalId = p.Ethnological.EthnologicalName,
                        PointPropertyId = p.Property.PropertyName,
                        PointProtectionId = p.ProtectionLevel.ProtectionName,
                        PointReligionId = p.Religion.ReligionName,

                        PointVideo = (p.PointVideo.FirstOrDefault().VideoFilePath == null) ? "-" : p.PointVideo.FirstOrDefault().VideoFilePath,
                        PointAddress = (p.PointAddress == null) ? "-" : p.PointAddress,
                        PointWeb = (p.PointWeb == null) ? "-" : p.PointWeb,
                        PointEmail = (p.PointEmail == null) ? "-" : p.PointEmail,
                        PointPhone = (p.PointPhone == null) ? "-" : p.PointPhone,

                        PointUser = u.AspNetUsers.UserName,
                        PointLocalization = p.PointLocalization
                    
                    };

            return sights;

        }
        
        [Route("{id:int}")]
        [ResponseType(typeof(PointDetailDto))]
        public async Task<IHttpActionResult> GetSights(int id)
        {

            
            var sight = await (from p in db.Point.Include("PointImage").Include("PointVideo").Include("AspNetUsers")
                                    join u in db.PointOfUser on p.PointId equals u.PointId
                               where p.PointId == id && u.isActivated == (byte)1
                                    select new PointDetailDto
                                    {
                                        PointId = p.PointId,

                                        PointName = p.PointName,
                                        PointX = p.PointX,
                                        PointY = p.PointY,

                                        PointYear = (p.PointYear == null) ? 0 : p.PointYear,

                                        PointPlaceNomos = (p.PointPlaceNomos == null) ? "-" : p.PointPlaceNomos,
                                        PointDescription = p.PointDescription,
                                        PointFoto = (p.PointImage.FirstOrDefault().ImageFilePath == null) ? "http://www.cult-macedonia.com/myData/img/default_picture.png" : p.PointImage.FirstOrDefault().ImageFilePath,
                                        
                                        PointCategoryId = p.Category.CategoryName,
                                        PointEraId = p.Era.EraName,
                                        PointEthnologicalId = p.Ethnological.EthnologicalName,
                                        PointPropertyId = p.Property.PropertyName,
                                        PointProtectionId = p.ProtectionLevel.ProtectionName,
                                        PointReligionId = p.Religion.ReligionName,

                                        PointVideo = (p.PointVideo.FirstOrDefault().VideoFilePath == null) ? "-" : p.PointVideo.FirstOrDefault().VideoFilePath,
                                        PointAddress = (p.PointAddress == null) ? "-" : p.PointAddress,
                                        PointWeb = (p.PointWeb == null) ? "-" : p.PointWeb,
                                        PointEmail = (p.PointEmail == null) ? "-" : p.PointEmail,
                                        PointPhone = (p.PointPhone == null) ? "-" : p.PointPhone,
                                        PointUser = u.AspNetUsers.UserName,
                                        PointLocalization = p.PointLocalization

                                    }).FirstOrDefaultAsync();
            
          
            
            if (sight == null)
            {
                return NotFound();
            }

            return Ok(sight);

            


          
        }


        [Route("create")]
        [HttpPost]
        public PointActionResultDto CreateSight(PointCreateDto point)
        {

            PointActionResultDto result = new PointActionResultDto();
            result.success = 0;
            result.message = "failed";


            if (point.PointName == "new") 
                result.success = 1;
            



            return result;


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
