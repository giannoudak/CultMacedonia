using CULTMACEDONIA_v2.Models;
using CULTMACEDONIA_v2.Models.CultMacedoniaModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CULTMACEDONIA_v2.Controllers
{
    public class PointsController : Controller
    {


        /// <summary>
        /// Τhe DataBase context
        /// </summary>
        CultMacedoniaDBEntities db = new CultMacedoniaDBEntities();

        /// <summary>
        /// The temp path.
        /// </summary>
        //private const string TempPath = @"C:\Temp\pipes";
        private string TempPath = System.Web.HttpContext.Current.Server.MapPath(@"~/PointImages");
        static System.Globalization.NumberFormatInfo ni = null;


        //
        // GET: /Points/
        public ActionResult Index()
        {
            int BlockSize = 5;
            int startIndex = (1) * BlockSize;

            var points = (from p in db.Point
                     select p).Take(BlockSize).ToList();

            return View(points);
        }

        [ChildActionOnly]
        public ActionResult PointList(List<Point> Model)
        {
            return PartialView(Model);
        }

        [HttpPost]
        public ActionResult InfinateScroll(int BlockNumber)
        {
            //////////////// demo καθυστερησηηη ///////////////
            //System.Threading.Thread.Sleep(1000);
            ////////////////////////////////////////////////////////////////////////////////////////

            int BlockSize = 5;
            int startIndex = (BlockNumber - 1) * BlockSize;

            var points = (from p in db.Point
                          select p).OrderByDescending(y=>y.PointName).Skip(startIndex).Take(BlockSize).ToList();


            

            JsonModel jsonModel = new JsonModel();
            jsonModel.NoMoreData = points.Count < BlockSize;
            jsonModel.HTMLString = RenderPartialViewToString("PointList", points);

            return Json(jsonModel);
        }

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        public ActionResult EditNew()
        {
            return View();
        }

        //
        // GET: /Points/View/5
        public ActionResult View(int? id)
        {
            bool isUserFavorite = false;
            PointFullViewModel point = null;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Αν έχουμε συνδεδμένο χρήστη τότε
            if (Request.IsAuthenticated)
            {
                // Διαβάζουμε το όνομα του συνδεδεμένου χρήστη
                var currUser = User.Identity.Name;
                
                // Ελέγχος αν το σημείο ειναι favorite για τον συγκεκριμένο χρήστη
                isUserFavorite = (from uf in db.UserFavorites
                        where uf.PointId == id && uf.AspNetUsers.UserName == currUser
                        select uf).Any();

            }


            point = (from p in db.Point.Include("PointImage")
                        where p.PointId == id
                        select new PointFullViewModel
                        {
                            PointId = p.PointId,
                            PointName = p.PointName,
                            PointEmail = p.PointEmail,
                            PointPhone = p.PointPhone,
                            PointPlaceNomos = p.PointPlaceNomos,
                            PointWeb = p.PointWeb,
                            PointImage = p.PointImage,
                            PointDescription = p.PointDescription,

                            PointCategoryId = p.PointCategoryId,
                            Category = p.Category,

                            PointEraId = p.PointEraId,
                            Era = p.Era,

                            PointEthnologicalId = p.PointEthnologicalId,
                            Ethnological = p.Ethnological,

                            PointPropertyId = p.PointPropertyId,
                            Property = p.Property,

                            PointReligionId = p.PointReligionId,
                            Religion = p.Religion,

                            PointProtectionId = p.PointProtectionId,
                            ProtectionLevel = p.ProtectionLevel,

                            PointX = p.PointX,
                            PointY = p.PointY,

                            PointYear = p.PointYear,
                            
                            PointAddress = p.PointAddress,
                            isUserFavorite = isUserFavorite
                        }).SingleOrDefault();

            
            
          
            if (point == null)
            {
                return HttpNotFound();
            }
            return View(point);


        }

        //
        // GET: /Points/GetPointList
        // Επιστρεφει ενα Json με αντικειμενα PointViewModel και με paging
        // πληροφορίες
        public ActionResult GetPointList(int? page, int pageSize = 10)
        {

            var pageNumber = page ?? 1;

            var q = from p in db.Point.Include("PointImage")
                    orderby p.PointId
                    select new PointListViewModel
                    {

                        PointId = p.PointId,
                        PointName = p.PointName,
                        PointAddress = p.PointAddress,
                        PointX = p.PointX,
                        PointY = p.PointY,
                        PointCategory = p.Category.CategoryName,
                        pointImages = (from l in db.PointImage.Where(i => i.PointId == p.PointId).Take(1)
                                       select new PointImageViewModel
                                       {
                                           ImageId = l.ImageId,
                                           ImagePath = l.ImageFilePath,
                                           ImageName = l.ImageFileName

                                       }).ToList()
                    };

            var pipes = q.ToList();

            var itemList = new PagedList<PointListViewModel>(q, pageNumber, pageSize);

            
            return Json(new { items = itemList, metaData = itemList.GetMetaData() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Επιστρεφει ένα partial view με όλα τα points πάνω σε χάρτη
        /// </summary>
        /// <returns></returns>
        public PartialViewResult vMapPartialnew()
        {
            List<PointMapViewModel> pnts = new List<PointMapViewModel>();
            
            // Διαβάζουμε την λίστα με όλες τις κατηγορίες άξιοθέατων
            pnts = (from p in db.Point
                    select new PointMapViewModel

                    {
                        PlaceName = p.PointName,
                        GeoLong = p.PointX,
                        GeoLat = p.PointY,
                        PlaceAddress = p.PointAddress
                    }).Take(15).ToList();




            // Περνάμε την λίστα στο partial view
            return PartialView(pnts);

        }

        public ActionResult UserFavoritePointPartial(int pointId, bool isFavorite)
        {
            ViewBag.PointId = pointId;
            return PartialView("_PointUserFavoritePartial", isFavorite);
        }

        // post to favorites
        public ActionResult Favorite(int id)
        {

            // εξομοίωση καθυστέρησης για να προλάβει να φανεί το loading...
            Thread.Sleep(100);
            ViewBag.PointId = id;

            // Διαβάζουμε το id του τρέχον χρήστη
            // Αν έχουμε συνδεδμένο χρήστη τότε
            if (Request.IsAuthenticated)
            {
                // Διαβάζουμε το όνομα του συνδεδεμένου χρήστη
                var currUser = User.Identity.Name;

                // Διαβάζουμε το uid του χρήστη
                var uid = (from u in db.AspNetUsers
                           where u.UserName == currUser
                           select u.Id).SingleOrDefault();

                // προσθέτουμε το συγκεκριμένο point στα Favorites του χρήστη
                db.UserFavorites.Add(new UserFavorites
                {
                    Id = uid,
                    PointId = id
                });

                // κάνουμε αποθήκευση των αλλαγών
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }

                return PartialView("_PointUserFavoritePartial", true);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            
        }


        /// <summary>
        /// Επιστρεφει 15 PointViewModel αντικείμενα σε Json
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPoints()
        {
            CultMacedoniaDBEntities db = new CultMacedoniaDBEntities();

            // Διαβάζουμε την λίστα με όλες τις κατηγορίες άξιοθέατων
            var q = (from p in db.Point
                     select new PointMapViewModel

                     {
                         PlaceName = p.PointName,
                         
                         GeoLong = p.PointX,
                         GeoLat = p.PointY,
                         PlaceAddress = p.PointAddress
                     }).Take(15).ToList();

            return Json(q, JsonRequestBehavior.AllowGet);

        }

        #region Ενεργοποιηση Monument

        // POST
        // EnablePoint 
        public ActionResult EnablePoint(int pointId, string username,string pointName)
        {

            bool success = false;
            string message = string.Empty;

            // Διαβάζουμε το uid του χρήστη
            var uid = (from u in db.AspNetUsers
                       where u.UserName == username
                       select u.Id).SingleOrDefault();

            PointOfUser pointOfUser = db.PointOfUser.Where(p => p.PointId == pointId && p.Id == uid).SingleOrDefault();

            if (pointOfUser != null)
            {
                pointOfUser.isActivated = 1;
                pointOfUser.DateActivated = DateTime.Now;
                try {
                    db.SaveChanges();
                    success = true;
                    message = string.Format(CultResources.Admin.EnableMonumentSuccess, pointName);
                }
                catch (Exception e)
                {
                    success = false;
                    message = string.Format(CultResources.Admin.EnableMonumentFailure, pointName);
                }
                
            }

            return Json(new { success = success, message = message });
        }

        #endregion

        #region Καταχώρηση GET-POST
        // GET: /Points/New
        [Authorize()]
        public ActionResult New()
        {
            ViewBag.PointCategoryId = new SelectList(db.Category, "CategoryId", "CategoryName");
            ViewBag.PointEraId = new SelectList(db.Era, "EraId", "EraName");
            ViewBag.PointEthnologicalId = new SelectList(db.Ethnological, "EthnologicalId", "EthnologicalName");
            ViewBag.PointPropertyId = new SelectList(db.Property, "PropertyId", "PropertyName");
            ViewBag.PointProtectionId = new SelectList(db.ProtectionLevel, "ProtectionId", "ProtectionName");
            ViewBag.PointReligionId = new SelectList(db.Religion, "ReligionId", "ReligionName");
            return View();
        }


        // POST: /Points/New
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize()]
        [ValidateAntiForgeryToken]
        public ActionResult New(PointCreateViewModel point)
        {
            string vpath = string.Empty;
            string message = string.Empty;
            string redirectTo= string.Empty;
            decimal? p_x = null;
            decimal? p_y = null;


            // Διαβάζουμε το id του τρέχον χρήστη
            // Αν έχουμε συνδεδμένο χρήστη τότε
            if (Request.IsAuthenticated)
            {


                System.Globalization.CultureInfo ci = System.Globalization.CultureInfo.InstalledUICulture;
                ni = (System.Globalization.NumberFormatInfo) ci.NumberFormat.Clone();
                ni.NumberDecimalSeparator = ".";

                if (point.PointX != null)
                {
                    p_x = decimal.Parse(point.PointX, ni);
                }
                
                if (point.PointY != null)
                {
                    p_y = decimal.Parse(point.PointY,ni);
                }

                
                string lang = string.Empty;
                

                // Αν δεν έχουμε σφάλματα
                if (ModelState.IsValid)
                {

                    // Διαβάζουμε το Culture του session ώστε να ξέρουμε σε ποια γλώσσα θα
                    // κάνουμε save το νέο Point
                    CultureInfo cultinfo = (Session["Culture"] as CultureInfo);
                    if (cultinfo != null) lang = cultinfo.Name.Replace("-","");

                    // Δημιουργία Point Entity Item
                    Point p = new Point
                    {
                        PointName = point.PointName,
                        PointDescription = point.PointDescription,

                        PointCategoryId = point.PointCategoryId,
                        PointEthnologicalId = point.PointEthnologicalId,
                        PointPropertyId = point.PointPropertyId,
                        PointProtectionId = point.PointProtectionId,
                        PointReligionId = point.PointReligionId,
                        PointEraId = point.PointEraId,
                        
                        PointYear = Convert.ToInt32 (point.PointYear),
                        PointPhone = point.PointPhone,
                        PointPlaceNomos = point.PointPlaceNomos,
                        PointAddress = point.PointAddress,
                        PointWeb = point.PointWeb,
                        PointEmail = point.PointEmail,


                        PointLocalization = lang,
                        PointX = p_x,
                        PointY = p_y
                    };


                    // Διαβάζουμε το όνομα του συνδεδεμένου χρήστη
                    var currUser = User.Identity.Name;

                    // Διαβάζουμε το uid του χρήστη
                    var uid = (from u in db.AspNetUsers
                               where u.UserName == currUser
                               select u.Id).SingleOrDefault();


                    // και προσθετουμε την εγγραφή οτι το point ανηκει στον τρέχον login user
                    db.PointOfUser.Add(new PointOfUser
                    {
                        DateAdded = DateTime.Now,
                        Point = p,
                        Id = uid,
                        isActivated = (byte)0,  // αρχικά το point δεν ειναι activate
                        DateActivated = null    // και η ημερομηνία activate ειναι κενη
                    });


                    // Ελέγχουμε αν ο χρήστης έχει κάνει upload εικόνες
                    if (!string.IsNullOrEmpty(point.images))
                    {


                        // Αν ναι, για κάθε μία από τις εικόνες δημιουργούεμε τόσα PointImage Entities
                        string[] imgs = (point.images).Split(',');
                        foreach (string s in imgs)
                        {
                            if (!Request.IsLocal) //vpath = "/deploy"; 
                                vpath = "";
                            

                            // Δημιουργία του PointImage Entity
                            PointImage pointImg = new PointImage
                            {
                                Point = p,
                                ImageFileName = s,
                                ImageFilePath = Request.Url.GetLeftPart(UriPartial.Authority) + vpath + "/PointImages/" + s,
                                ImageTitle = "title"
                            };

                            // προσθήκη του PointImage Entity στο context
                            db.PointImage.Add(pointImg);
                        }

                    }

                    // Προσθήκη του νέου αντικειμενου Point στην Βάση
                    db.Point.Add(p);


                    try
                    {
                        db.SaveChanges();
                        message = string.Format(CultResources.View.CreateResultSuccess, p.PointName);
                        redirectTo = Url.Action("View", "Points", new { id = p.PointId });
                    }
                    catch (Exception e)
                    {
                        var s = e.InnerException.Message;
                        message = CultResources.View.CreateResultFailure; 
                    }


                    // create Point Here and do save
                    return Json(new { success = true, message = message, redirectTo = redirectTo });
                    //return RedirectToAction("Index");


                }else
                {
                    ViewBag.PointCategoryId = new SelectList(db.Category, "CategoryId", "CategoryName", point.PointCategoryId);
                    ViewBag.PointEraId = new SelectList(db.Era, "EraId", "EraName", point.PointEraId);
                    ViewBag.PointEthnologicalId = new SelectList(db.Ethnological, "EthnologicalId", "EthnologicalName", point.PointEthnologicalId);
                    ViewBag.PointPropertyId = new SelectList(db.Property, "PropertyId", "PropertyName", point.PointPropertyId);
                    ViewBag.PointProtectionId = new SelectList(db.ProtectionLevel, "ProtectionId", "ProtectionName", point.PointProtectionId);
                    ViewBag.PointReligionId = new SelectList(db.Religion, "ReligionId", "ReligionName", point.PointReligionId);

                    var modelStateErrors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                    message = CultResources.View.CreateFailure + Environment.NewLine;

                    message += "<ul>";
                    foreach (var modelStateError in modelStateErrors)
                    {
                        message += "<li>" + modelStateError.ErrorMessage + Environment.NewLine + "</li>";
                    }
                    message += "</ul>";
                    return Json(new { success = false, response = message });


                }


            }// end if is authenticated
            return Json(new { success = false, response = "403" });

        }

        #endregion

        #region Επεξεργασία GET-POST

        // GET: /Points/Edit/5
        public ActionResult Edit(int? id)
        {
            byte isActivated = (byte)0;
            PointEditViewModel point = null;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Αν έχουμε συνδεδμένο χρήστη τότε
            if (Request.IsAuthenticated)
            {
                // Διαβάζουμε το όνομα του συνδεδεμένου χρήστη
                var currUser = User.Identity.Name;

                // Ελέγχος αν το ειναι enabled
                
                isActivated = (from uf in db.PointOfUser
                                  where uf.PointId == id && uf.AspNetUsers.UserName == currUser
                                  select uf.isActivated).SingleOrDefault();

            }


            point = (from p in db.Point.Include("PointImage")
                     where p.PointId == id
                     select new PointEditViewModel
                     {
                         PointId = p.PointId,
                         PointName = p.PointName,
                         PointEmail = p.PointEmail,
                         PointPhone = p.PointPhone,
                         PointPlaceNomos = p.PointPlaceNomos,
                         PointWeb = p.PointWeb,
                         PointImage = p.PointImage,
                         PointDescription = p.PointDescription,

                         PointCategoryId = p.PointCategoryId,
                         Category = p.Category,

                         PointEraId = p.PointEraId,
                         Era = p.Era,

                         PointEthnologicalId = p.PointEthnologicalId,
                         Ethnological = p.Ethnological,

                         PointPropertyId = p.PointPropertyId,
                         Property = p.Property,

                         PointReligionId = p.PointReligionId,
                         Religion = p.Religion,

                         PointProtectionId = p.PointProtectionId,
                         ProtectionLevel = p.ProtectionLevel,

                         PointX = p.PointX,
                         PointY = p.PointY,

                         PointYear = p.PointYear,

                         PointAddress = p.PointAddress,
                         isEnabled = (isActivated == (byte)0)
                     }).SingleOrDefault();




            if (point == null)
            {
                return HttpNotFound();
            }


            ViewBag.PointCategoryId = new SelectList(db.Category, "CategoryId", "CategoryName",point.PointCategoryId);
            ViewBag.PointEraId = new SelectList(db.Era, "EraId", "EraName",point.PointEraId);
            ViewBag.PointEthnologicalId = new SelectList(db.Ethnological, "EthnologicalId", "EthnologicalName",point.PointEthnologicalId);
            ViewBag.PointPropertyId = new SelectList(db.Property, "PropertyId", "PropertyName",point.PointPropertyId);
            ViewBag.PointProtectionId = new SelectList(db.ProtectionLevel, "ProtectionId", "ProtectionName",point.PointProtectionId);
            ViewBag.PointReligionId = new SelectList(db.Religion, "ReligionId", "ReligionName",point.PointReligionId);

            return View(point);
        }

        // POST: /Point/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PointEditViewModel point, string originalImageList, string toDeleteImageList, string toInsertImageList, string test)
        {
            bool success = false;
            string message = string.Empty;
            string redirectTo = string.Empty;

            // if the model is valid, we procced with the update operation
            if (ModelState.IsValid)
            {
                try
                {

                    // @@@@@@ Ρουτίνα Επεξεργασίας Αξιοθέατου @@@@@@

                    // 1] Ανάκτηση Αξιοθέατου από την Βάση
                    Point dbPoint = db.Point.Find(point.PointId);
                    if (dbPoint != null)
                    {

                        // 2] Για κάθε μια από τις φωτογραφίες που θέλουμε να διαγράψουμε (αν υπάρχουν)
                        if (!string.IsNullOrEmpty(toDeleteImageList))
                        {
                            string[] toDeleteArray = toDeleteImageList.Split(',');
                            for (int i = 0; i < toDeleteArray.Length; i++)
                            {
                                // διαβάζουμε το αντικείμενο PointImage
                                PointImage pImageDel = db.PointImage.Find(Convert.ToInt32(toDeleteArray[i]));

                                // Αν βρέθηκε η Εικόνα
                                if (pImageDel != null)
                                {
                                    // παίρνουμε το filename
                                    string fname = pImageDel.ImageFileName;

                                    // 2.1] Γίνεται διαγραφή του record από την Βάση
                                    db.PointImage.Remove(pImageDel);


                                    // 2.2] Γίνεται διαγραφή του αρχείου από τον server



                                }
                                else
                                {
                                    // αλλιώς...
                                    // todo sthing!
                                }

                            }// end loop για κάθε εικόνα προς διαγραφή

                        }// end if imageDeleteList is empty
                        


                        // 3] Καταχώρηση Νέων εικόνων (αν υπάρχουν)
                        if (!string.IsNullOrEmpty(toInsertImageList))
                        {
                            string[] toInsertArray = toInsertImageList.Split(',');
                            for (int j = 0; j < toInsertArray.Length; j++)
                            {

                            }
                        }



                        // 4] παιρνάω τις νέες τιμές στο Point
                        db.Entry(dbPoint).State = EntityState.Modified;

                        dbPoint.PointName = point.PointName;
                        dbPoint.PointDescription = point.PointDescription;

                        dbPoint.PointPhone = point.PointPhone;
                        dbPoint.PointPlaceNomos = point.PointPlaceNomos;
                        dbPoint.PointWeb = point.PointWeb;
                        dbPoint.PointYear = point.PointYear;
                        dbPoint.PointEmail = point.PointEmail;

                        dbPoint.PointAddress = point.PointAddress;
                        dbPoint.PointX = point.PointX;
                        dbPoint.PointY = point.PointY;

                        dbPoint.PointPropertyId = point.PointPropertyId;
                        dbPoint.PointProtectionId = point.PointProtectionId;
                        dbPoint.PointReligionId = point.PointReligionId;
                        dbPoint.PointCategoryId = point.PointCategoryId;
                        dbPoint.PointEraId = point.PointEraId;
                        dbPoint.PointEthnologicalId = point.PointEthnologicalId;



                        db.SaveChanges();
                        success = true;
                        message = string.Format(CultResources.View.EditResultSuccess, point.PointName); 
                        
                        //"H enhmerwsh toy mnheiou '" + point.PointName + "' egine epityxws!. Κλείστε τον διάλογο και θα μεταφερθείτε αυτόματα στην σελίδα του αξιοθέταου!";
                        redirectTo = Url.Action("View", "Points", new { id = point.PointId });



                    }
                    else
                    {
                        success = false;
                        message = "Point for edit not found!";
                    }



                    
                }
                catch (Exception e)
                {
                    success = false;
                    message = e.InnerException.Message;
                }


                return Json(new { redirectTo = redirectTo, success = success, message = message });

                //return RedirectToAction("Index");
            }
            else
            {
                // the model is not valid. update operation is false!   

                success = false;

                // logging errors
                var modelStateErrors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                message = CultResources.View.EditFailure + Environment.NewLine;

                message += "<ul>";
                foreach (var modelStateError in modelStateErrors)
                {
                    message += "<li>" + modelStateError.ErrorMessage + Environment.NewLine + "</li>";
                }
                message += "</ul>";

                // setup viewbag properties for lut values as well as their selected values
                ViewBag.PointCategoryId = new SelectList(db.Category, "CategoryId", "CategoryName", point.PointCategoryId);
                ViewBag.PointEraId = new SelectList(db.Era, "EraId", "EraName", point.PointEraId);
                ViewBag.PointEthnologicalId = new SelectList(db.Ethnological, "EthnologicalId", "EthnologicalName", point.PointEthnologicalId);
                ViewBag.PointPropertyId = new SelectList(db.Property, "PropertyId", "PropertyName", point.PointPropertyId);
                ViewBag.PointProtectionId = new SelectList(db.ProtectionLevel, "ProtectionId", "ProtectionName", point.PointProtectionId);
                ViewBag.PointReligionId = new SelectList(db.Religion, "ReligionId", "ReligionName", point.PointReligionId);

                // return the json error response
                return Json(new { pointId = point.PointId, success = success, message = message });
            }

            
        }

        #endregion

        #region Files Upload READ-DELETE
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
           
            var s = System.Web.HttpContext.Current.Server.MapPath("/");
            var s1 = System.Web.HttpContext.Current.Server.MachineName;
            var s3 = System.Web.HttpContext.Current.Server.ToString();
            bool isUploaded = false;
            string message = string.Empty;

            

            foreach (HttpPostedFileBase file in files)
            {
                string filePath = Path.Combine(TempPath, file.FileName);
                try
                {
                    System.IO.File.WriteAllBytes(filePath, this.ReadData(file.InputStream));
                    message = "OK!";
                    isUploaded = true;
                }
                catch (Exception e)
                {
                    var ss = "saved in folder  " + filePath + e.Message;
                    message = "failed! " + ss;
                    isUploaded = false;
                }

            }


            return Json(new { isUploaded = isUploaded, message = message }, "application/json");
        }

        public ActionResult UploadFilesForEdit(IEnumerable<HttpPostedFileBase> files,int pointId)
        {

            bool isUploaded = false;
            bool isInserted = false;

            string UploadMessage = string.Empty;
            string insertMessage = string.Empty;
            string filePath = string.Empty;

            int newPointImageId = -1;
            HttpPostedFileBase myFile = null;

            // Αλγοριθμος Για Upload Φωτογραφιων στο Edit Mode
            // 1] Σε κάθε image drop κάνω Upload Κανονικά την φωτογραφία όπως στο New Action
            #region Normally Image Upload work!
            


            foreach (HttpPostedFileBase file in files)
            {
                myFile = file;
                filePath = Path.Combine(TempPath, file.FileName);
                try
                {
                    System.IO.File.WriteAllBytes(filePath, this.ReadData(file.InputStream));
                    UploadMessage = "upload OK!";
                    isUploaded = true;
                }
                catch (Exception e)
                {
                    var ss = "saved in folder  " + filePath + e.Message;
                    UploadMessage = "upload failed! " + ss;
                    isUploaded = false;
                }

            }
            #endregion

            #region Δημιουργια Record στον PointImages
            // HACK! υπήρχε το /deploy αν ηταν local request
            // 2] Αν το upload γινει επιτυχώς πρέπει να δημιουργήσουμε εγγραφή στον PointImages
            if (isUploaded)
            {
                try
                {
                    // δημιουργουμε ενα νέο PointImage 
                    PointImage pImg = new PointImage
                    {
                        PointId = pointId,
                        ImageFileName = myFile.FileName,
                        ImageTitle = myFile.FileName,
                        ImageFilePath = Request.Url.GetLeftPart(UriPartial.Authority) + (Request.IsLocal ? "":"") + "/PointImages/" + myFile.FileName,

                    };

                    // και το Αποθηκεύουμε στο context
                    db.PointImage.Add(pImg);

                    // κάνουμε αποθήκευση των αλλαγών
                    db.SaveChanges();

                    // αν ολα εγιναν σωστά
                    isInserted = true;
                    newPointImageId = pImg.ImageId;
                    insertMessage = "inserted ok!";
                }
                catch (Exception ex)
                {
                    isInserted = false;
                    newPointImageId = -1;
                    insertMessage = "record in pointImage failed: " + ex.Message;
                }
                
                
            }

            // για να δημιουργησω το record θέλω το pointId

            #endregion

            return Json(new { 
                isUploaded = isUploaded, 
                UploadMessage = UploadMessage, 
                isInserted = isInserted, 
                newPointImageId = newPointImageId,
                InsertMessage = insertMessage
            }, "application/json");
        }


        private byte[] ReadData(Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }


        }


        // GET
        // Points/DeleteFile
        /// <summary>
        /// Deletes a file 
        /// </summary>
        /// <param name="id">file name</param>
        /// <returns></returns>
        public ActionResult DeleteFile(string id)
        {
            var filename = id;
            var filePath = Path.Combine(TempPath, filename);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                
                return Json(new { success = true, response = "Το αρχειο διεγράφει επιτυχώς" },JsonRequestBehavior.AllowGet);
            }else{
                return Json(new { success = false, response = "Δεν βρέθηκε το αρχείο προς διαγραφή" }, JsonRequestBehavior.AllowGet);
            }

            
        }



        #endregion

        #region Αναζήτησης
        public ActionResult Search()
        {

            ViewBag.PointCategoryId = new SelectList(db.Category, "CategoryId", "CategoryName");
            ViewBag.PointEraId = new SelectList(db.Era, "EraId", "EraName");
            ViewBag.PointEthnologicalId = new SelectList(db.Ethnological, "EthnologicalId", "EthnologicalName");
            ViewBag.PointPropertyId = new SelectList(db.Property, "PropertyId", "PropertyName");
            ViewBag.PointProtectionId = new SelectList(db.ProtectionLevel, "ProtectionId", "ProtectionName");
            ViewBag.PointReligionId = new SelectList(db.Religion, "ReligionId", "ReligionName");

            return View();
        }

        [HttpPost]
        public PartialViewResult Search(PointSearchCriteriaViewModel criteria)
        {

            List<Point> points = new List<Point>();

            var q = from p in db.Point
                    select p;

            // @@@@@@@@@ Apply search criteria @@@@@@@@@

            // 1] By Name
            if (!string.IsNullOrEmpty(criteria.title))
                q = q.Where(y => y.PointName.Contains(criteria.title));



            

            // 2] By Year
            // 0-before, 1-after
            if (!string.IsNullOrEmpty(criteria.year))
            {
                var filtered = from p in points
                               select p;

                if (criteria.yearWhen == '0')
                    filtered = filtered.Where(y => y.PointYear <= Convert.ToInt32(criteria.year));
                else if (criteria.yearWhen == '1')
                    filtered = filtered.Where(y => y.PointYear >= Convert.ToInt32(criteria.year));

                
                points = filtered.ToList();
            }


            points = q.ToList();


            return PartialView("_pointsSearchResultListPartial", points);
        }
        #endregion

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