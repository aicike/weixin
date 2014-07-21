using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using Entity.Session;
using System.Web.Routing;

namespace ProjectShow.Controllers.BaseCon
{
    public class EnterpriseBaseController : BaseController
    {
        //Session
        protected AccountSession LoginAccount
        {
            get
            {
                var acctionsession = Session[SystemConst.Session.LoginAccount] as AccountSession;
                return acctionsession;
            }
            set { Session[SystemConst.Session.LoginAccount] = value; }
        }


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (LoginAccount == null)
            {
                filterContext.Result = new RedirectToRouteResult("Default",
                    new RouteValueDictionary{
                        { "controller", "Login" },
                        { "action", "SystemLogin" }
                });
                return;
            }

            //上一次请求信息
            var request = filterContext.RequestContext.HttpContext.Request;
            if (request != null && request.UrlReferrer != null && request.UrlReferrer.AbsolutePath != null)
            {
                ViewBag.RawUrl = filterContext.RequestContext.HttpContext.Request.UrlReferrer.AbsoluteUri;
            }
        }

    }
}
