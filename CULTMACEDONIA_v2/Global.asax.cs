using CULTMACEDONIA_v2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApiContrib.Formatting.Jsonp;

namespace CULTMACEDONIA_v2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected async void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add(new JsonpMediaTypeFormatter(new JsonMediaTypeFormatter()));

            // Καλούμε το task για την δημιουργία του Διαχειριστή
            bool x = await AddRoleAndUser();
        }


        /// <summary>
        /// Async Task που δημιουργεί τον Διαχειριστή του συστήματος
        /// Ο διαχειριστής ειναι ένα αντικείμενο CultMacedoniaUser
        /// </summary>
        /// <returns></returns>
        async Task<bool> AddRoleAndUser()
        {

            UserManager<CultMacedoniaUser> UserManager = new UserManager<CultMacedoniaUser>(new UserStore<CultMacedoniaUser>(new CultMacedoniaUserDbContext()));

            // Δημιουργούμε έναν CultMacedoniaUser και τον αποθηκεύουμε
            CultMacedoniaUser admUser = new CultMacedoniaUser() { UserName = "cultmacedsysadmin" };
            var result = await UserManager.CreateAsync(admUser, "d@tAforc3!987");
            if (result.Succeeded)
            {
                // αν γίνει η αποθήκευση τότε του κάνουμε assign τον Ρολο του admin
                var rstRole = await UserManager.AddToRoleAsync(admUser.Id, "CultMacedoniaAdmin");
                if (rstRole.Succeeded)
                {
                    return rstRole.Succeeded;
                }
            }

            return false;


        }

        /// <summary>
        /// Μέθοδος για να set-άρουμε το Language Culture στην εφαρμογή
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null)
            {
                CultureInfo cultureInfo = (CultureInfo)this.Session["Culture"];
                if (cultureInfo == null)
                {
                    string langName = "en";

                    if (HttpContext.Current.Request.UserLanguages != null && HttpContext.Current.Request.UserLanguages.Length != 0)
                        langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);

                    cultureInfo = new CultureInfo(langName);
                    this.Session["Culture"] = cultureInfo;
                }

                //Finally setting culture for each request
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
            }
        }

    }
}
