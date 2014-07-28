using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectShow.Controllers.BaseCon;
using Business;
using Business.Common;
using System.IO;
using Entity;

namespace ProjectShow.Controllers
{
    public class DefaultController : EnterpriseBaseController
    {
        //
        // GET: /Default/
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = LoginAccount.EnterpriseName;
            EnterpriseInfoModel einfoModel = new EnterpriseInfoModel();
            var einfo = einfoModel.GetInfo_byEnterpriseID(LoginAccount.EnterpriseID);
            //总预览次数
            PreviewCntModel previewCntModel = new PreviewCntModel();

            int CountCnt = previewCntModel.GetPreviewCnt(LoginAccount.EnterpriseID);
            ViewBag.Count = CountCnt.ToString("n");
            return View(einfo);
        }
        /// <summary>
        /// 获取二维码
        /// </summary>
        /// <returns></returns>
        public void GetQrCode()
        {
            QrCodes qrcode = new QrCodes();
            string url = "http://" + SystemConst.WebUrl + "/ProjectShow/index?EID=" + LoginAccount.EnterpriseID;
            MemoryStream ms = qrcode.Get_QrCode(url);
            if (null != ms)
            {
                Response.BinaryWrite(ms.ToArray());
            }
        }
        /// <summary>
        /// 修改企业信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpEnterpriseInfo(EnterpriseInfo enterpriseinfo, HttpPostedFileBase LogoImagePathFile)
        {
            EnterpriseInfoModel einfoModel = new EnterpriseInfoModel();
            var einfo = einfoModel.GetInfo_byEnterpriseID(LoginAccount.EnterpriseID);
            einfo.SName = enterpriseinfo.SName;
            einfoModel.EditLogo(einfo, LogoImagePathFile);
            return RedirectToAction("Index", "Default");
        }

        /// <summary>
        /// 修改分享信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpShareInfo(EnterpriseInfo enterpriseinfo)
        {
            EnterpriseInfoModel einfoModel = new EnterpriseInfoModel();
            var einfo = einfoModel.GetInfo_byEnterpriseID(LoginAccount.EnterpriseID);
            einfo.ShareRemark = enterpriseinfo.ShareRemark;
            einfo.ShareTitle = enterpriseinfo.ShareTitle;
            einfo.ButtonName = enterpriseinfo.ButtonName;
            einfoModel.Edit(einfo);
            return RedirectToAction("Index","Default");
        }

        /// <summary>
        /// 修改首页背景图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpBgImage(EnterpriseInfo enterpriseinfo, HttpPostedFileBase BGImagePathFile)
        {
            EnterpriseInfoModel einfoModel = new EnterpriseInfoModel();
            var einfo = einfoModel.GetInfo_byEnterpriseID(LoginAccount.EnterpriseID);
            einfoModel.EditBGimg(einfo, BGImagePathFile);
            return RedirectToAction("Index", "Default");
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        public ActionResult Outs()
        {
            Session[SystemConst.Session.LoginAccount] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}
