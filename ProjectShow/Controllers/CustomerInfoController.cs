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
            return View();
        }

        /// <summary>
        /// 导出excel
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public ActionResult ExportExcel(int PID)
        {
            DataTable dt = new DataTable();
            List<DataTable> dts = new List<DataTable>();
            dt.Columns.Add("www");
            DataRow row = dt.NewRow();
            row["www"] = "aaaaaaaa";
            dt.Rows.Add(row);
            dts.Add(dt);
            Common com = new Common();
            string file = Server.MapPath("/File/Excel/")+"sss.xml";
            com.Export(dts, "测试文件", file);
            return View();
        }

    }
}
