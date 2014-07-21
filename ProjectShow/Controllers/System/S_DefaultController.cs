﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectShow.Controllers.BaseCon;
using Entity;
using Business;

namespace ProjectShow.Controllers.System
{
    public class S_DefaultController : SysetemBaseController
    {
        //
        // GET: /S_Default/

        public ActionResult Index()
        {
            EnterpriseModel em = new EnterpriseModel();
            var list = em.List();
            return View(list.ToList());
        }


        public ActionResult Add()
        {
            ViewBag.Title = "Imtimely-企业管理-添加企业信息";
            return View();
        }

        [HttpPost]
        public ActionResult Add(Enterprise enterprise, string EUserName, string EUserPwd)
        {
            //添加企业
            EnterpriseModel em = new EnterpriseModel();
            enterprise.CreteDate = DateTime.Now;
            var result = em.Add(enterprise);
            if (result.HasError)
            {
                return JavaScript("JMessage(" + result.Error + ")");
            }
            else
            {
                //添加账号
                EUser euser = new EUser();
                euser.EnterpriseID = enterprise.ID;
                euser.Name = EUserName;
                euser.PassWord = DESEncrypt.Encrypt(EUserPwd);
                EUserModel emodel = new EUserModel();
                result = emodel.Add(euser);
                if (result.HasError)
                {
                    return JavaScript("JMessage(" + result.Error + ")");
                }
                else
                {
                    //添加企业信息
                    EnterpriseInfoModel entinfoModel = new EnterpriseInfoModel();
                    EnterpriseInfo entinfo = new EnterpriseInfo();
                    entinfo.PreviewCnt = 0;
                    entinfo.EnterpriseID = enterprise.ID;
                    entinfoModel.Add(entinfo);
                }
                return JavaScript("window.location.href='" + Url.Action("Index", "S_Default") + "'");
            }


        }

        public ActionResult Edit(int ID)
        {
            ViewBag.Title = "Imtimely-企业管理-修改企业信息";
            EnterpriseModel em = new EnterpriseModel();
            Enterprise enterprise = em.Get(ID);
            ViewBag.EUserName = enterprise.EUser.FirstOrDefault().Name;
            ViewBag.EUserPwd = DESEncrypt.Decrypt(enterprise.EUser.FirstOrDefault().PassWord);
            ViewBag.UserID = enterprise.EUser.FirstOrDefault().ID;
            return View(enterprise);
        }

        [HttpPost]
        public ActionResult Edit(Enterprise enterprise, string EUserName, string EUserPwd, int UserID)
        {
            EnterpriseModel em = new EnterpriseModel();
            var result = em.Edit(enterprise);
            if (result.HasError)
            {
                return JavaScript("JMessage(" + result.Error + ")");
            }
            else
            {
                EUser euser = new EUser();
                euser.EnterpriseID = enterprise.ID;
                euser.Name = EUserName;
                euser.PassWord = DESEncrypt.Encrypt(EUserPwd);
                euser.ID = UserID;
                euser.EnterpriseID = enterprise.ID;
                EUserModel emodel = new EUserModel();
                result = emodel.Edit(euser);
                if (result.HasError)
                {
                    return JavaScript("JMessage(" + result.Error + ")");
                }
                return JavaScript("window.location.href='" + Url.Action("Index", "S_Default") + "'");
            }
        }
        public ActionResult Delete(int ID)
        {
            EnterpriseModel em = new EnterpriseModel();
            EUserModel emodel = new EUserModel();
            EnterpriseInfoModel entinfoModel = new EnterpriseInfoModel();
            //删除用户
            var result = emodel.Delte_byEID(ID);
            if (result.HasError)
            {
                return JavaScript("JMessage(" + result.Error + ")");
            }
            else
            {
                //删除企业信息
                result = entinfoModel.Delte_byEID(ID);
                if (result.HasError)
                {
                    return JavaScript("JMessage(" + result.Error + ")");
                }
                else
                {
                    //删除企业
                    result = em.Delete(ID);
                    if (result.HasError)
                    {
                        return JavaScript("JMessage(" + result.Error + ")");
                    }
                }
            }
            return JavaScript("window.location.href='" + Url.Action("Index", "S_Default") + "'");

        }

    }
}
