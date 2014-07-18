using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectShow.Controllers.BaseCon;
using Entity;
using Business;

namespace ProjectShow.Controllers.System
{
    public class S_DefaultController : SysetemBaseController
    {
        //
        // GET: /S_Default/

        public ActionResult Index()
        {
            EnterpriseModel em = new EnterpriseModel();
            var list = em.List();
            return View(list.ToList());
        }


        public ActionResult Add()
        {
            ViewBag.Title = "Imtimely-企业管理-添加企业信息";
            return View();
        }

        [HttpPost]
        public ActionResult Add(Enterprise enterprise)
        {
            EnterpriseModel em = new EnterpriseModel();
            enterprise.CreteDate = DateTime.Now;
            var result = em.Add(enterprise);
            if (result.HasError)
            {
                return JavaScript("JMessage(" + result.Error + ")");
            }
            else
            {
                return JavaScript("window.location.href='" + Url.Action("Index", "S_Default") + "'");
            }
        }

        public ActionResult Edit(int ID)
        {
            ViewBag.Title = "Imtimely-企业管理-修改企业信息";
            EnterpriseModel em = new EnterpriseModel();
            Enterprise enterprise = em.Get(ID);
            return View(enterprise);
        }

        [HttpPost]
        public ActionResult Edit(Enterprise enterprise)
        {
            EnterpriseModel em = new EnterpriseModel();
            var result = em.Add(enterprise);
            if (result.HasError)
            {
                return JavaScript("JMessage(" + result.Error + ")");
            }
            else
            {
                return JavaScript("window.location.href='" + Url.Action("Index", "S_Default") + "'");
            }
        }

    }
}
