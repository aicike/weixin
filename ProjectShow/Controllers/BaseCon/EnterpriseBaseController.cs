using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using Entity.Session;

namespace ProjectShow.Controllers.BaseCon
{
    public class EnterpriseBaseController : Controller
    {
        //Session
        protected AcctionSession LoginAcction
        {
            get
            {
                var acctionsession = Session[SystemConst.Session.LoginAccount] as AcctionSession;
                return acctionsession;
            }
            set { Session[SystemConst.Session.LoginAccount] = value; }
        }


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (LoginAcction == null)
            {
                Response.Redirect("http://" + SystemConst.WebUrl+"/Login");
            }
            base.OnActionExecuting(filterContext);
        }

    }
}
