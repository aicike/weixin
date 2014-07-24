using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectShow.Controllers.BaseCon;
using Business;
using Entity;

namespace ProjectShow.Controllers
{
    public class CProblemController : EnterpriseBaseController
    {
        //
        // GET: /CProblem/
        /// <summary>
        /// 问题列表页
        /// </summary>
        /// <param name="PID">项目ID</param>
        /// <returns></returns>
        public ActionResult Index(int PID)
        {
            CProblemModel cpmodel = new CProblemModel();
            ProjectModel projectmodel = new ProjectModel();
            var croblelist = cpmodel.GetList_ByPID(PID, LoginAccount.EnterpriseID).ToList();

            var project = projectmodel.Get(PID);
            ViewBag.ProjectName = project.PName;
            ViewBag.Title = "项目/产品 - 留下用户哪些信息 - " + project.PName;
            ViewBag.PID = PID;
            if (croblelist != null && croblelist.Count()>0)
            {
                ViewBag.MaxSort = croblelist.Max(a => a.sort);
            }
            else
            {
                ViewBag.MaxSort = 0;
            }
            return View(croblelist);
        }

        /// <summary>
        /// 添加界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add(int PID)
        {
            ProjectModel projectmodel = new ProjectModel();
            var project = projectmodel.Get(PID);
            ViewBag.ProjectName = project.PName;
            ViewBag.Title = "项目/产品 - 留下用户哪些信息 - 添加问题 - " + project.PName;
            ViewBag.PID = PID;
            return View();
        }

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(CProblem cproblem, string Options)
        {
            CProblemModel cpmodel = new CProblemModel();
            var sort = cpmodel.GetMaxSort(cproblem.ProjectID, LoginAccount.EnterpriseID);
            cproblem.EnterpriseID = LoginAccount.EnterpriseID;
            cproblem.sort = sort;
            //添加选项
            if (cproblem.ProblemType == (int)Entity.Enum.EnumCProblemProblemType.Check || cproblem.ProblemType == (int)Entity.Enum.EnumCProblemProblemType.Radio)
            {
                var Json = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Json_Problem_Option>>(Options);
                cproblem.COption = new List<COption>();
                foreach (var item in Json)
                {
                    cproblem.COption.Add(new COption()
                   {
                       EnterpriseID = LoginAccount.EnterpriseID,
                       Option = item.Option
                   });
                }
            }

            var result = cpmodel.Add(cproblem);
            if (result.HasError)
            {
                return JavaScript("JMessage(" + result.Error + ")");
            }
            else
            {
                return JavaScript("window.location.href='" + Url.Action("Index", "CProblem", new { PID = cproblem.ProjectID }) + "'");
            }
        }

        /// <summary>
        /// 修改界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int PID, int CPID)
        {
            ProjectModel projectmodel = new ProjectModel();
            var project = projectmodel.Get(PID);
            ViewBag.ProjectName = project.PName;
            ViewBag.Title = "项目/产品 - 留下用户哪些信息 - 修改问题 - " + project.PName;
            ViewBag.PID = PID;
            CProblemModel cpmodel = new CProblemModel();
            var item = cpmodel.GetInfo(PID, CPID, LoginAccount.EnterpriseID);
            if (item.CustomerInfo.Count > 0)
            {
                //不可更改类型与选项
                ViewBag.IsUpdate = 1;
            }
            else
            {
                //可更改类型与选项
                ViewBag.IsUpdate = 0;
            }
            return View(item);
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(CProblem cproblem, string Options, int IsUpdate)
        {
            CProblemModel cpmodel = new CProblemModel();
            var Json = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Json_Problem_Option>>(Options);
            var result = cpmodel.UpdateInf(cproblem, Json, IsUpdate);
            if (result.HasError)
            {
                return JavaScript("JMessage(" + result.Error + ")");
            }
            else
            {
                return JavaScript("window.location.href='" + Url.Action("Index", "CProblem", new { PID = cproblem.ProjectID }) + "'");
            }
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="CPID"></param>
        /// <returns></returns>
        public ActionResult Delete(int CPID, int PID)
        {
            CProblemModel cpmodel = new CProblemModel();
            cpmodel.DeleteInfo(CPID, LoginAccount.EnterpriseID,PID);
            return RedirectToAction("Index", "CProblem", new { PID = PID });
        }

        /// <summary>
        /// 排序 上移
        /// </summary>
        /// <param name="PID">项目ID</param>
        /// <param name="CPID">问题ID</param>
        /// <returns></returns>
        public ActionResult SortUP(int sort, int CPID, int PID)
        { 
            CProblemModel cpmodel = new CProblemModel();
            cpmodel.SortUpOrDown(1, sort, CPID, PID, LoginAccount.EnterpriseID);
             return RedirectToAction("Index", "CProblem", new { PID = PID });
        }

        /// <summary>
        /// 排序 下移
        /// </summary>
        /// <param name="PID">项目ID</param>
        /// <param name="CPID">问题ID</param>
        /// <returns></returns>
        public ActionResult SortDown(int sort,int CPID,int PID)
        {
            CProblemModel cpmodel = new CProblemModel();
            cpmodel.SortUpOrDown(2, sort, CPID, PID, LoginAccount.EnterpriseID);
            return RedirectToAction("Index", "CProblem", new { PID = PID });
        }

    }
}
