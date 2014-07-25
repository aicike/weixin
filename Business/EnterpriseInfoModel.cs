using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using System.Web;
using Business.Common;

namespace Business
{
    public class EnterpriseInfoModel : BaseModel<EnterpriseInfo>
    {
        /// <summary>
        /// 根据企业ID 获取信息
        /// </summary>
        /// <param name="EID"></param>
        /// <returns></returns>
        public EnterpriseInfo GetInfo_byEnterpriseID(int EID)
        {
            var enter = List().Where(a => a.EnterpriseID == EID).FirstOrDefault();
            return enter;
        }

        /// <summary>
        /// 根据企业ID 删除用户
        /// </summary>
        /// <param name="EID"></param>
        /// <returns></returns>
        public Result Delte_byEID(int EID)
        {
            Result result = new Result();
            string sql = "delete EnterpriseInfo where EnterpriseID=" + EID;
            int cnt = base.SqlExecute(sql);
            if (cnt <= 0)
            {
                result.HasError = true;
                result.Error = "删除该企业信息失败! 请稍后再试";
            }
            return result;
        }

        /// <summary>
        /// 修改Logo
        /// </summary>
        /// <param name="enterpriseInfo"></param>
        /// <param name="LogoImagePathFile"></param>
        /// <returns></returns>
        public Result EditLogo(EnterpriseInfo enterpriseInfo, HttpPostedFileBase LogoImagePathFile)
        {
            Result result = new Result();
            if (LogoImagePathFile != null)
            {
                Common.Common com = new Common.Common();
                var LastName = com.CreateRandom("", 5) + LogoImagePathFile.FileName.GetFileSuffix();
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                var imageName = string.Format("{0}_{1}", token, LastName);

                //var FIleTempPath = HttpContext.Current.Server.MapPath(SystemConst.Business.FileTempPath);
                //var imageTempPath = string.Format("{0}/{1}", FIleTempPath, imageName);

                var FilePath = string.Format(SystemConst.Business.FilePath, enterpriseInfo.EnterpriseID);
                var imagePath = string.Format("{0}/{1}", HttpContext.Current.Server.MapPath(FilePath), imageName);

                var imageBCPath = string.Format("{0}/{1}", FilePath, imageName);
                LogoImagePathFile.SaveAs(imagePath);
                //ToolImage.SuperGetPicThumbnail(imageTempPath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
                enterpriseInfo.Logo = imageBCPath;
            }
            result = base.Edit(enterpriseInfo);
            return result;
        }

        /// <summary>
        /// 修改背景图片
        /// </summary>
        /// <param name="enterpriseInfo"></param>
        /// <param name="LogoImagePathFile"></param>
        /// <returns></returns>
        public Result EditBGimg(EnterpriseInfo enterpriseInfo, HttpPostedFileBase BGImagePathFile)
        {
            Result result = new Result();
            if (BGImagePathFile != null)
            {
                Common.Common com = new Common.Common();
                var LastName = com.CreateRandom("", 5) + BGImagePathFile.FileName.GetFileSuffix();
                var token = DateTime.Now.ToString("yyyyMMddHHmmss");
                var imageName = string.Format("{0}_{1}", token, LastName);

                var FIleTempPath = HttpContext.Current.Server.MapPath(SystemConst.Business.FileTempPath);
                var imageTempPath = string.Format("{0}/{1}", FIleTempPath, imageName);

                var FilePath = string.Format(SystemConst.Business.FilePath, enterpriseInfo.EnterpriseID);
                var imagePath = string.Format("{0}/{1}", HttpContext.Current.Server.MapPath(FilePath), imageName);

                var imageBCPath = string.Format("{0}/{1}", FilePath, imageName);
                BGImagePathFile.SaveAs(imageTempPath);
                ToolImage.SuperGetPicThumbnail(imageTempPath, imagePath, 70, 640, 0, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.High);
                enterpriseInfo.BgImage = imageBCPath;
            }
            result = base.Edit(enterpriseInfo);
            return result;
        }

        /// <summary>
        /// 更改预览次数
        /// </summary>
        /// <param name="EID"></param>
        /// <returns></returns>
        public Result UpPreviewCnt(int EID)
        {
            Result result = new Result();
            string sql = "";
            var list = List().Where(a => a.EnterpriseID == EID).FirstOrDefault();
            if (list != null)
            {

                sql = "update EnterpriseInfo set [PreviewCnt]= ([PreviewCnt]+1) where EnterpriseID = " + EID;
                base.SqlExecute(sql);

            }

            return result;
        }
    }
}
