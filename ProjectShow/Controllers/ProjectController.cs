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
            var list = pmodel.GetProjectByEID(1).ToPagedList(id ?? 1, 15);
            return View(list);
        }

        public ActionResult Add()
        {
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
                return JavaScript(AlertJS_NoTag(new Dialog(result.Error)));
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "Project") + "'");
        }
    }
}
