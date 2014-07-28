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

        public Result Edit(New newinfo, string fileName, string oldImagePath, int EnterpriseID, int w, int h, int x1, int y1, int tw, int th)
        {
            Result result = new Result();
            if (newinfo.Image != oldImagePath)
            {
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
            }
            if (result.HasError)
            {
                return result;
            }
            return base.Edit(newinfo);
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

        /// <summary>
        /// 移动位置
        /// </summary>
        /// <param name="type">1 上移，2下移</param>
        /// <param name="sort">当前排序</param>
        /// <returns></returns>
        public Result SortUpOrDown(int type, int sort, int ProjectID, int newID)
        {
            Result result = new Result();
            string sql = "";
            string sql2 = "";
            //上移
            if (type == 1)
            {
                sql = string.Format("update New set sort = {0} where ProjectID={1} and sort={2} ", sort, ProjectID, sort + 1);
                sql2 = string.Format("update New set sort = {0} where  ID={1} ", sort + 1, newID);
                base.SqlExecute(sql + " " + sql2);
            }
            //下移
            else
            {
                sql = string.Format("update New set sort = {0} where ProjectID={1} and sort={2} ", sort, ProjectID, sort - 1);
                sql2 = string.Format("update New set sort = {0} where ID={1} ", sort - 1, newID);
                base.SqlExecute(sql + " " + sql2);
            }
            return result;
        }
    }
}
