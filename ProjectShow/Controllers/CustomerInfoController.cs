using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectShow.Controllers.BaseCon;
using Business;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Data;
using Business.Common;

namespace ProjectShow.Controllers
{
    public class CustomerInfoController : EnterpriseBaseController
    {
        //
        // GET: /CustomerInfo/

        public ActionResult Index(int PID)
        {
            ProjectModel projectmodel = new ProjectModel();
            var project = projectmodel.Get(PID);
            ViewBag.PID = PID;
            ViewBag.Title = "客户信息 - " + project.PName;
            ViewBag.ProjectName = project.PName;
            CustomerInfoModel customerInfoModel= new CustomerInfoModel();
            var dt = customerInfoModel.getDtInfo(PID,LoginAccount.EnterpriseID);


            return View(dt);
        }

        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public ActionResult ExportExcel(int PID)
        {
            ProjectModel projectmodel = new ProjectModel();
            var project = projectmodel.Get(PID);
            List<DataTable> dts = new List<DataTable>();
            CustomerInfoModel customerInfoModel = new CustomerInfoModel();
            var dt = customerInfoModel.getDtInfo(PID, LoginAccount.EnterpriseID);
            dts.Add(dt);
            Common com = new Common();
            string fileName = project.PName + "用户信息" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".xlsx";
            string file = Server.MapPath("/File/Excel/") + fileName;
            com.Export(dts, "用户信息", file);

        

            FileStream fs = new FileStream(file, FileMode.Open);

            byte[] bytes = new byte[(int)fs.Length];

            fs.Read(bytes, 0, bytes.Length);

            fs.Close();

            Response.ContentType = "application/octet-stream";

            //通知浏览器下载文件而不是打开 

            Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8));

            Response.BinaryWrite(bytes);

            Response.Flush();

            Response.End();

            Directory.Delete(file);
            


            return View();
        }

    }
}
