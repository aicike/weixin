using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business;
using Entity;
using ProjectShow.Controllers.BaseCon;

namespace ProjectShow.Controllers
{
    /// <summary>
    /// 项目管理页面
    /// </summary>
    public class ProjectController : EnterpriseBaseController
    {
        public ActionResult Index(int? id)
        {
            ProjectModel pmodel = new ProjectModel();
            var list = pmodel.GetProjectByEID(LoginAccount.EnterpriseID).ToPagedList(id ?? 1, 15);
            ViewBag.Menu = 2;
            return View(list);
        }

        public ActionResult Add()
        {
            ViewBag.Menu = 2;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Project project)
        {
            ProjectModel pmodel = new ProjectModel();

            project.EnterpriseID = LoginAccount.EnterpriseID;
            var result = pmodel.Add(project);
            if (result.HasError)
            {
                return Alert(result);
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "Project") + "'");
        }

        public ActionResult Edit(int id)
        {
            ProjectModel pmodel = new ProjectModel();
            var obj = pmodel.Get(id);
            ViewBag.Menu = 2;
            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(Project project)
        {
            ProjectModel pmodel = new ProjectModel();
            project.EnterpriseID = LoginAccount.EnterpriseID;
            var result = pmodel.Edit(project);
            if (result.HasError)
            {
                return Alert(result);
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "Project") + "'");
        }
    }
}
