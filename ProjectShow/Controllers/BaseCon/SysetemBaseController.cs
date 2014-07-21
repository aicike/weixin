using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity.Session;
using Entity;

namespace ProjectShow.Controllers.BaseCon
{
    public class SysetemBaseController : Controller
    {
        //Session
        protected SystemSession LoginSystem
        {
            get
            {
                var SystemSession = Session[SystemConst.Session.LoginSystem] as SystemSession;
                return SystemSession;
            }
            set { Session[SystemConst.Session.LoginSystem] = value; }
        }


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (LoginSystem == null)
            {
                Response.Redirect("http://" + SystemConst.WebUrl+"/login/systemLogin");
            }
            base.OnActionExecuting(filterContext);
        }

    }
}
