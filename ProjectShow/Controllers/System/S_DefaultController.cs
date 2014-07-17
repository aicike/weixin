using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectShow.Controllers.BaseCon;

namespace ProjectShow.Controllers.System
{
    public class S_DefaultController : SysetemBaseController
    {
        //
        // GET: /S_Default/

        public ActionResult Index()
        {
            return View();
        }

    }
}
