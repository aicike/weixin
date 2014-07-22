using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business;

namespace ProjectShow.Controllers
{
    //前端展示控制器
    public class ProjectShowController : Controller
    {
        //
        // GET: /ProjectShow/
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int EID)
        {
            //var s = Newtonsoft.Json.JsonConvert.SerializeObject("a");
            //Newtonsoft.Json.JsonConvert.DeserializeObject(s);
            //企业基本信息
            EnterpriseInfoModel enterpriseInfoModel = new EnterpriseInfoModel();
            var enterpriseInfo = enterpriseInfoModel.GetInfo_byEnterpriseID(EID);
            //title 标题
            ViewBag.Title = enterpriseInfo.ShareTitle;
            //分享信息
            ViewBag.BgImage = enterpriseInfo.BgImage;
            ViewBag.ShareTitle = enterpriseInfo.ShareTitle;
            ViewBag.ShareRemark = enterpriseInfo.ShareRemark;
            ViewBag.EID = EID;
            return View(enterpriseInfo);
        }

        /// <summary>
        /// 项目/分类页
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectClass(int EID)
        {
            EnterpriseInfoModel enterpriseInfoModel = new EnterpriseInfoModel();
            var enterpriseInfo = enterpriseInfoModel.GetInfo_byEnterpriseID(EID);
            //企业简称
            ViewBag.SName = enterpriseInfo.SName;
            //title 标题
            ViewBag.Title = enterpriseInfo.SName;
            ProjectModel projectModel = new ProjectModel();
            var projectlist = projectModel.GetProjectByEID(EID);
            //分享信息
            ViewBag.BgImage = "";// projectlist.FirstOrDefault().ImageInfos.FirstOrDefault().Path; ;
            ViewBag.ShareTitle = enterpriseInfo.SName + " 项目展示";
            ViewBag.ShareRemark = enterpriseInfo.SName + " 项目展示";
            ViewBag.EID = EID;
            if (projectlist != null)
            {
                //如果企业只有一个项目/产品 则直接跳转到该项目/产品列表下
                if (projectlist.Count() == 1)
                {
                    return RedirectToAction("ProjectClassList", "ProjectShow", new { PID = projectlist.FirstOrDefault().ID, EID = EID });
                }
            }
            return View(projectlist.ToList());
        }

        /// <summary>
        /// 项目/分类列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectClassList(int PID, int EID)
        {
            ProjectModel projectModel = new ProjectModel();
            var project = projectModel.Get(PID);
            ViewBag.Title = project.PName;
            //分享信息
            ViewBag.BgImage = "";
            ViewBag.ShareTitle = project.PName;
            ViewBag.ShareRemark = project.PName;
            //菜单信息
            ViewBag.Phone = project.Phone;
            ViewBag.Lat = project.Lat;
            ViewBag.Lng = project.Lng;
            ViewBag.PName = project.PName;
            ViewBag.MapAddress = project.MapAddress;

            ViewBag.PID = PID;
            ViewBag.EID = EID;
            return View();
        }

        /// <summary>
        /// 详细信息展示页
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectInfo(int AID, int PID, int EID)
        {
            //分享信息
            ViewBag.BgImage = "";
            ViewBag.ShareTitle = "";
            ViewBag.ShareRemark = "";

            ViewBag.PID = PID;
            ViewBag.EID = EID;
            return View();
        }

        /// <summary>
        /// 客户提交信息页面
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="EID"></param>
        /// <returns></returns>
        public ActionResult CustomerInfo(int PID, int EID)
        {
            EnterpriseInfoModel enterpriseInfoModel = new EnterpriseInfoModel();
            var enterpriseInfo = enterpriseInfoModel.GetInfo_byEnterpriseID(EID);
            //分享信息
            ViewBag.BgImage = enterpriseInfo.BgImage;
            ViewBag.ShareTitle =enterpriseInfo.SName+ "留下您的信息";
            ViewBag.ShareRemark = enterpriseInfo.SName + "留下您的信息";
            ProjectModel projectModel = new ProjectModel();
            var project = projectModel.Get(PID);
            ViewBag.Title = project.PName;

            CProblemModel cproblemModel = new CProblemModel();

            return View();
        
        }

    }
}
