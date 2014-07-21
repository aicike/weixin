using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using System.Web.Routing;

namespace ProjectShow.Controllers.BaseCon
{
    public class EnterpriseBaseController : BaseController
    {
        protected EUser LoginAccount
        {
            get
            {
                var account = Session[SystemConst.Session.LoginAccount] as EUser;
                return account;
            }
            set { Session[SystemConst.Session.LoginAccount] = value; }
        }


        public ActionResult Index()
        {
            return View();
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LoginAccount = new EUser() { EnterpriseID = 1 };

            //if (LoginAccount == null)
            //{
            //    filterContext.Result = new RedirectToRouteResult("Default",
            //        new RouteValueDictionary{
            //            { "controller", "Login" },
            //            { "action", "SystemLogin" }
            //    });
            //    return;
            //}

            //上一次请求信息
            var request = filterContext.RequestContext.HttpContext.Request;
            if (request != null && request.UrlReferrer != null && request.UrlReferrer.AbsolutePath != null)
            {
                ViewBag.RawUrl = filterContext.RequestContext.HttpContext.Request.UrlReferrer.AbsoluteUri;
            }
        }
    }
}
