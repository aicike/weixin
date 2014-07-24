using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;

namespace Business
{
    public class ProjectModel : BaseModel<Project>
    {
        public IQueryable<Project> GetProjectByEID(int eid)
        {
            return List().Where(a => a.EnterpriseID == eid);
        }

        public Result Edit(Project entity, List<ImageInfo> images)
        {
            string sql = "DELETE dbo.ImageInfo WHERE ProjectID=" + entity.ID;
            if (images != null && images.Count > 0)
            {
                foreach (var item in images)
                {
                    sql +=string.Format( " INSERT INTO dbo.ImageInfo (Path, ProjectID) VALUES ('{0}',{1})",item.Path,entity.ID);
                }
            }
            base.SqlExecute(sql);
            return base.Edit(entity);
        }

        /// <summary>
        /// 获取最大排序号+1
        /// </summary>
        public int GetMaxSort(int EID)
        {
            int sort = 0;
            var list = List().Where(a => a.EnterpriseID == EID);
            if (list != null)
            {
                if (list.Count() > 0)
                {
                    sort = list.Max(a => a.sort);
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
        public Result SortUpOrDown(int type, int sort,int EnterpriseID, int PID)
        {
            Result result = new Result();
            string sql = "";
            string sql2 = "";
            //上移
            if (type == 1)
            {
                sql = string.Format("update Project set sort = {0} where EnterpriseID={1} and sort={2} ", sort, EnterpriseID, sort + 1);
                sql2 = string.Format("update Project set sort = {0} where  ID={1} ", sort + 1, PID);
                base.SqlExecute(sql + " " + sql2);
            }
            //下移
            else
            {
                sql = string.Format("update Project set sort = {0} where EnterpriseID={1} and sort={2} ", sort, EnterpriseID, sort - 1);
                sql2 = string.Format("update Project set sort = {0} where ID={1} ", sort -1, PID);
                base.SqlExecute(sql + " " + sql2);
            }
            return result;
        }
    }
}
