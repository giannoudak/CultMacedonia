using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using CULTMACEDONIA_v2.Models.CultMacedoniaModel;
using CULTMACEDONIA_v2.Models.DTOs;
using System.Data.Entity.Infrastructure;

namespace CULTMACEDONIA_v2.Controllers
{
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {

        /// <summary>
        /// Τhe DataBase context
        /// </summary>
        CultMacedoniaDBEntities db = new CultMacedoniaDBEntities();


        // GET api/<controller>
        [Route("")]
        public IEnumerable<LutCategoryViewModel> Get()
        {
            //return new string[] { "value1", "value2" };
            var q = (from c in db.Category
                    orderby c.CategoryId descending
                    where c.CategoryName != "-"
                    select new LutCategoryViewModel
                    {
                        id = c.CategoryId,
                        name = c.CategoryName,
                        lang = c.Lang
                    }).ToList();

            return q;
        }

        // GET api/<controller>/5
        public LutCategoryViewModel Get(int id)
        {
            return null;
        }

        // POST api/<controller>
        [RouteAttribute("POST")]
        [AcceptVerbs("POST")]
        [Route("new")]

        public HttpResponseMessage Post(LutCategoryViewModel category)
        {

            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            Category cat = new Category();

            cat.CategoryName = category.name;
            cat.Lang = category.lang;

            db.Category.Add(cat);

            try
            {
                db.SaveChanges();
                category.id = cat.CategoryId;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }


            return Request.CreateResponse(HttpStatusCode.OK, category);

            
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, LutCategoryViewModel category)
        {
            if (ModelState.IsValid && id == category.id)
            {
                
                db.Category.Find(id).CategoryName = category.name;

                try
                {
                   db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            Category category = db.Category.Find(id);
            if (category == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Category.Remove(category);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
            
        }
    }
}