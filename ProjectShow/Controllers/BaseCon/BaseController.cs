using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using Poco;
using Entity;

namespace System.Web.Mvc
{
    public class BaseController : Controller
    {
        public ContentResult Alert(Result dialog)
        {
            string str = "";
            if (dialog.HasError)
            {
                str = string.Format("jMessage('{0}',false)", dialog.Error);
            }
            else
            {
                str = string.Format("jMessage('{0}')",dialog.Error);
            }
            return Content(str);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.RequestContext.HttpContext.Request;
            if (request != null && request.UrlReferrer != null && request.UrlReferrer.AbsolutePath != null)
            {
                ViewBag.RawUrl = filterContext.RequestContext.HttpContext.Request.UrlReferrer.AbsoluteUri;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
