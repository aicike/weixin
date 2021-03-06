﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
using Entity.Session;
using Business;

namespace ProjectShow.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        
        /// <summary>
        /// 企业登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 企业登陆
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string Name, string Pwd)
        {
            EUserModel euserModel = new EUserModel();
            var result = euserModel.Login(Name, DESEncrypt.Encrypt(Pwd));
            if (result.HasError)
            {
                return JavaScript("alert('账号密码错误！')");
            }
            else
            {
                return JavaScript("window.location.href='" + Url.Action("Index", "Default") + "'");
            }
        }
        /// <summary>
        /// 平台登陆
        /// </summary>
        /// <returns></returns>
        public ActionResult SystemLogin()
        {
            return View();
        }

        /// <summary>
        /// 平台登陆
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SystemLogin(string Name,string Pwd)
        {
            if (Name == "Admin" && Pwd == "pass123!")
            {
                SystemSession ss = new SystemSession();
                ss.SUserID = 0;
                ss.SUserName = "Admin";
                Session[SystemConst.Session.LoginSystem] = ss;
                return JavaScript("window.location.href='" + Url.Action("Index", "S_Default") + "'");
            }
            else
            {
                 
                return JavaScript("alert('账号密码错误！')");
            }
        }

    }
}
