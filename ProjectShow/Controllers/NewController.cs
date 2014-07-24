using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectShow.Controllers.BaseCon;
using Business;
using Entity;
using System.IO;
using Business.Common;
using System.Drawing.Drawing2D;

namespace ProjectShow.Controllers
{
    public class NewController : EnterpriseBaseController
    {
        public ActionResult Index(int projectID)
        {
            NewModel newModel = new NewModel();
            var list = newModel.GetNewByProjectID(projectID).OrderByDescending(a => a.Sort).ToList();
            ViewBag.Menu = 2;

            if (list != null && list.Count() > 0)
            {
                ViewBag.MaxSort = list.Max(a => a.Sort);
            }
            else
            {
                ViewBag.MaxSort = 0;
            }
            ViewBag.PID = projectID;
            return View(list);
        }

        public ActionResult Add(int PID)
        {
            ViewBag.Menu = 2;
            ViewBag.PID = PID;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(New entity,int PID, int w, int h, int x1, int y1, int tw, int th)
        {
            NewModel newModel = new NewModel();
            entity.ProjectID = PID;
            entity.ViewCount = 0;
            entity.Sort = newModel.GetMaxSort(PID);
            string fileName = Request.Form["hidImage"];
            var result= newModel.Add(entity, fileName, LoginAccount.EnterpriseID, w, h, x1, y1, tw, th);
            if (result.HasError)
            {
                return Alert(result);
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "New", new { projectID=PID }) + "'");
        }

        [HttpPost]
        public JsonResult UpImg()
        {
            var files = Request.Files;
            if (Request.Files.Count > 0)
            {
                string savePath = null;
                bool isOk = ToolImage.SuperGetPicThumbnail(Request.Files[0], ref savePath, 100, 800, 0, SmoothingMode.HighQuality, CompositingQuality.HighQuality, InterpolationMode.High);
                if (isOk)
                {
                    return Json(new { jsonrpc = "2.0", result = savePath, fileName = Request.Files[0].FileName });
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


        public ActionResult Edit(int id)
        {
            ViewBag.Menu = 2;
            NewModel newModel = new NewModel();
            var newinfo = newModel.Get(id);
            return View(newinfo);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(New entity,string oldImage, int w, int h, int x1, int y1, int tw, int th)
        {
            NewModel newModel = new NewModel();
            string fileName = Request.Form["hidImage"];
            Result result = newModel.Edit(entity, fileName, oldImage, LoginAccount.EnterpriseID, w, h, x1, y1, tw, th);
            if (result.HasError)
            {
                return Alert(result);
            }
            return RedirectToAction("Index", "AppAdvertorial");
        }
    }
}
