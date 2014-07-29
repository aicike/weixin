using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business;
using Entity;
using ProjectShow.Controllers.BaseCon;
using Business.Common;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing;

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
            var list = pmodel.GetProjectByEID(LoginAccount.EnterpriseID).OrderByDescending(a => a.sort).ToList();
            ViewBag.Menu = 2;

            if (list != null && list.Count() > 0)
            {
                ViewBag.MaxSort = list.Max(a => a.sort);
            }
            else
            {
                ViewBag.MaxSort = 0;
            }
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

            var hidImages = Request.Form["hidImages"];
            if (!string.IsNullOrEmpty(hidImages))
            {
                string[] images = hidImages.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (images != null)
                {
                    List<ImageInfo> imageList = new List<ImageInfo>();
                    for (int i = 0; i < images.Length; i++)
                    {
                        ImageInfo ii = new ImageInfo();
                        ii.Path = images[i];
                        imageList.Add(ii);
                    }
                    project.ImageInfos = imageList;
                }
            }
            project.sort = pmodel.GetMaxSort(LoginAccount.EnterpriseID);
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
            var hidImages = Request.Form["hidImages"];
            List<ImageInfo> imageList = new List<ImageInfo>();
            if (!string.IsNullOrEmpty(hidImages))
            {
                string[] images = hidImages.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (images != null)
                {
                    for (int i = 0; i < images.Length; i++)
                    {
                        ImageInfo ii = new ImageInfo();
                        ii.ProjectID = project.ID;
                        ii.Path = images[i];
                        imageList.Add(ii);
                    }
                }
            }
            var result = pmodel.Edit(project, imageList);
            if (result.HasError)
            {
                return Alert(result);
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "Project") + "'");
        }

        /// <summary>
        /// 排序 上移
        /// </summary>
        /// <param name="PID">项目ID</param>
        /// <param name="CPID">问题ID</param>
        /// <returns></returns>
        public ActionResult SortUP(int sort,  int PID)
        {
            ProjectModel cpmodel = new ProjectModel();
            cpmodel.SortUpOrDown(1, sort,LoginAccount.EnterpriseID, PID);
            return RedirectToAction("Index", "Project");
        }

        /// <summary>
        /// 排序 下移
        /// </summary>
        /// <param name="PID">项目ID</param>
        /// <param name="CPID">问题ID</param>
        /// <returns></returns>
        public ActionResult SortDown(int sort,int PID)
        {
            ProjectModel cpmodel = new ProjectModel();
            cpmodel.SortUpOrDown(2, sort, LoginAccount.EnterpriseID, PID);
            return RedirectToAction("Index", "Project");
        }


        [HttpPost]
        public JsonResult UpImg()
        {
            var files = Request.Files;
            if (Request.Files.Count > 0)
            {
                var f = Request.Files[0];
                string path = "/File/Enterprise/" + LoginAccount.EnterpriseID + "/" + DateTime.Now.ToString("yyyy-MM-dd");
                string fileName = "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + f.FileName;
                string TempPath = Server.MapPath(path);
                if (Directory.Exists(TempPath) == false)
                {
                    Directory.CreateDirectory(TempPath);
                }
                string showPath = path + fileName;
                string savePath = TempPath + fileName;
                bool isOk = ToolImage.SuperGetPicThumbnail(f, ref savePath, 70, 800, 0, SmoothingMode.HighQuality, CompositingQuality.HighQuality, InterpolationMode.High);
                if (isOk)
                {
                    return Json(new { jsonrpc = "2.0", result = showPath });
                }
                else
                {
                    return Json(new { jsonrpc = "2.0", error = "{'code': 103, 'message': '文件上传失败。'}" });
                }
            }
            else
            {
                return Json(new { jsonrpc = "2.0", error = "{'code': 103, 'message': '文件上传失败。'}" });
            }
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="CPID"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            ProjectModel pmodel = new ProjectModel();
            pmodel.Delete(id, LoginAccount.EnterpriseID);
            return RedirectToAction("Index", "Project");
        }
    }
}
