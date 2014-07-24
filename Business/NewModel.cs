using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using System.Web;
using Business.Common;
using System.IO;

namespace Business
{
    public class NewModel : BaseModel<New>
    {
        public IQueryable<New> GetNewByProjectID(int projectID)
        {
            return List().Where(a => a.ProjectID == projectID);
        }

        public Result Add(New newinfo, string fileName, int EnterpriseID, int w, int h, int x1, int y1, int tw, int th)
        {
            Result result = new Result();
            try
            {
                var imagePath = newinfo.Image;
                var lsImaFilePath = HttpContext.Current.Server.MapPath(imagePath);

                string path = "/File/Enterprise/" + EnterpriseID + "/" + DateTime.Now.ToString("yyyy-MM-dd");
                fileName = "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + fileName;
                string SavePath = HttpContext.Current.Server.MapPath(path);
                if (Directory.Exists(SavePath) == false)
                {
                    Directory.CreateDirectory(SavePath);
                }
                SavePath += fileName;
                bool isok = ToolImage.SuperGetPicThumbnailJT(lsImaFilePath, SavePath, 80, w, h, x1, y1, tw, th, System.Drawing.Drawing2D.SmoothingMode.HighQuality, System.Drawing.Drawing2D.CompositingQuality.HighQuality, System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic);
                if (isok)
                {
                    newinfo.Image = path + fileName;
                }
                else
                {
                    result.Error = "图片保存失败。";
                }
            }
            catch (Exception ex)
            {
                result.Error = "图片保存失败。";
            }
            if (result.HasError)
            {
                return result;
            }
            return base.Add(newinfo);
        }

        public Result Edit(New newinfo, string fileName, string oldImagePath,int EnterpriseID, int w, int h, int x1, int y1, int tw, int th)
        {
            return null;
        }

        /// <summary>
        /// 获取最大排序号+1
        /// </summary>
        public int GetMaxSort(int pid)
        {
            int sort = 0;
            var list = List().Where(a => a.ProjectID == pid);
            if (list != null)
            {
                if (list.Count() > 0)
                {
                    sort = list.Max(a => a.Sort);
                }
            }
            return sort + 1;
        }
    }
}
