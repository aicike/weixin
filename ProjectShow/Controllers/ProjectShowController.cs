using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }

        /// <summary>
        /// 项目/分类页
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectClass()
        {
            return View();
        }

        /// <summary>
        /// 项目/分类列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectClassList()
        {
            return View();
        }

        /// <summary>
        /// 详细信息展示页
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectInfo()
        {
            return View();
        }

    }
}
