using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business;
using Entity;

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

            //预览次数
            enterpriseInfoModel.UpPreviewCnt(EID);
            PreviewCntModel previewCntModel = new PreviewCntModel();
            previewCntModel.CreateOrAddCnt(EID);

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
            ViewBag.BgImage = "http://www." + SystemConst.WebUrl + Url.Content(projectlist.FirstOrDefault().ImageInfos.FirstOrDefault().Path);
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
            //预览次数
            PreviewCntModel previewCntModel = new PreviewCntModel();
            previewCntModel.CreateOrAddCnt(EID);
            return View(projectlist.ToList());
        }

        /// <summary>
        /// 项目/分类列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectClassList(int PID, int EID)
        {
            EnterpriseInfoModel enterpriseInfoModel = new EnterpriseInfoModel();
            var enterpriseInfo = enterpriseInfoModel.GetInfo_byEnterpriseID(EID);
            //企业简称
            ViewBag.SName = enterpriseInfo.SName;

            ProjectModel projectModel = new ProjectModel();
            var project = projectModel.Get(PID);
            //获取项目图片
            var projectimgs = project.ImageInfos.ToList();
            ViewBag.projectimgs = projectimgs;
            ViewBag.Title = project.PName;
            //分享信息
            ViewBag.BgImage =  "http://www."+SystemConst.WebUrl+ Url.Content(projectimgs.FirstOrDefault().Path);
            ViewBag.ShareTitle = project.PName;
            ViewBag.ShareRemark = project.PName;
            //菜单信息
            ViewBag.Phone = project.Phone;
            ViewBag.Lat = project.Lat;
            ViewBag.Lng = project.Lng;
            ViewBag.PName = project.PName;
            ViewBag.MapAddress = project.MapAddress;
            //软文信息
            NewModel newModel = new NewModel();
            var newList = newModel.GetNewByProjectID(PID).ToList();

            ViewBag.PID = PID;
            ViewBag.EID = EID;
            //预览次数
            PreviewCntModel previewCntModel = new PreviewCntModel();
            previewCntModel.CreateOrAddCnt(EID);

            return View(newList);
        }

        /// <summary>
        /// 详细信息展示页
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectInfo(int NID, int PID, int EID)
        {
            EnterpriseInfoModel enterpriseInfoModel = new EnterpriseInfoModel();
            var enterpriseInfo = enterpriseInfoModel.GetInfo_byEnterpriseID(EID);
            //企业简称
            ViewBag.SName = enterpriseInfo.SName;
            //软文信息
            NewModel newModel = new NewModel();
            var news = newModel.Get(NID);
            //分享信息
            ViewBag.BgImage = "http://www." + SystemConst.WebUrl + Url.Content(news.Image);
            ViewBag.ShareTitle = news.Title;
            ViewBag.ShareRemark = news.Title;
            //菜单信息
            ProjectModel projectModel = new ProjectModel();
            var project = projectModel.Get(PID);
            ViewBag.Phone = project.Phone;
            ViewBag.Lat = project.Lat;
            ViewBag.Lng = project.Lng;
            ViewBag.PName = project.PName;
            ViewBag.MapAddress = project.MapAddress;
            

            ViewBag.PID = PID;
            ViewBag.EID = EID;

            //预览次数
            PreviewCntModel previewCntModel = new PreviewCntModel();
            previewCntModel.CreateOrAddCnt(EID);
            return View(news);
        }

        /// <summary>
        /// 客户提交信息页面
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="EID"></param>
        /// <returns></returns>
        public ActionResult CustomerInfo(int PID, int EID)
        {
            ViewBag.PID = PID;
            ViewBag.EID = EID;
            EnterpriseInfoModel enterpriseInfoModel = new EnterpriseInfoModel();
            var enterpriseInfo = enterpriseInfoModel.GetInfo_byEnterpriseID(EID);
            //企业简称
            ViewBag.SName = enterpriseInfo.SName;
            //分享信息
            ViewBag.BgImage = enterpriseInfo.BgImage;
            ViewBag.ShareTitle =enterpriseInfo.SName+ "留下您的信息";
            ViewBag.ShareRemark = enterpriseInfo.SName + "留下您的信息";
            ProjectModel projectModel = new ProjectModel();
            var project = projectModel.Get(PID);
            ViewBag.Title = project.PName;
            //获取问题列表
            CProblemModel cproblemModel = new CProblemModel();
            var cproblem = cproblemModel.GetList_ByPID(PID,EID);
            //预览次数
            PreviewCntModel previewCntModel = new PreviewCntModel();
            previewCntModel.CreateOrAddCnt(EID);
            return View(cproblem.ToList());
        
        }
        /// <summary>
        /// 客户提交信息页面
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="EID"></param>
        /// <param name="Answer"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CustomerInfo(int PID, int EID, string Answer)
        {
            var Json = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Json_Problem_Answer>>(Answer);
            CustomerInfoModel customerinfoModel = new CustomerInfoModel();
            customerinfoModel.AddInfo(PID, EID, Json);
            return JavaScript("");
        }

    }
}
