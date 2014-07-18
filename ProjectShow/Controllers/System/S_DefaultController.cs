using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectShow.Controllers.BaseCon;
using Entity;

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


        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Enterprise enterprise)
        {

            return View();
        }

        public ActionResult Edit(int ID)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Enterprise enterprise)
        {
            return View();
        }

    }
}
