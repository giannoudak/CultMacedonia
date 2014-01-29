using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace CULTMACEDONIA_v2.Helpers
{
    public static class HtmlHelperExtensions
    {
        

            public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper, string rawHtml, string action, string controller, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
            {
                //string anchor = ajaxHelper.ActionLink("##holder##", action, controller, routeValues, ajaxOptions, htmlAttributes).ToString();
                //return MvcHtmlString.Create(anchor.Replace("##holder##", rawHtml));
                /* Updated code to use a generated guid as from the suggestion of Phillip Haydon */
                string holder = Guid.NewGuid().ToString();
                string anchor = ajaxHelper.ActionLink(holder, action, controller, routeValues, ajaxOptions, htmlAttributes).ToString();
                return MvcHtmlString.Create(anchor.Replace(holder, rawHtml));
            }

            

        


    }
}