using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;

namespace Business
{
    public class CProblemModel : BaseModel<CProblem>
    {
        /// <summary>
        /// 获取问题列表
        /// </summary>
        /// <param name="PID">项目ID</param>
        /// <param name="EID">企业ID</param>
        /// <returns></returns>
        public IQueryable<CProblem> GetList_ByPID(int PID, int EID)
        {
            var list = List().Where(a => a.EnterpriseID == EID && a.ProjectID == PID).OrderBy(a => a.sort);
            return list;
        }

        /// <summary>
        /// 获取最大排序号+1
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="EID"></param>
        /// <returns></returns>
        public int GetMaxSort(int PID, int EID)
        {
            int sort = 0;
            var list = List().Where(a => a.EnterpriseID == EID && a.ProjectID == PID);
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
        /// 
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="CPID"></param>
        /// <param name="EID"></param>
        /// <returns></returns>
        public CProblem GetInfo(int PID, int CPID, int EID)
        {
            var item = List().Where(a => a.ID == CPID && a.EnterpriseID == EID && a.ProjectID == PID).FirstOrDefault();
            return item;
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="cproblem"></param>
        /// <param name="options"></param>
        /// <param name="IsUpdate">是否有客户录入数据1 是 0否</param>
        public Result UpdateInf(CProblem cproblem, List<Json_Problem_Option> options, int IsUpdate)
        {
            Result result = new Result();
            base.Edit(cproblem);

            try
            {
                //没有客户录入数据
                if (IsUpdate == 0)
                {
                    string sql = string.Format("delete COption where CProblemID ={0}", cproblem.ID);
                    base.SqlExecute(sql);
                    if (options != null && options.Count > 0)
                    {


                        StringBuilder insertSql = new StringBuilder("INSERT INTO dbo.COption( EnterpriseID ,CProblemID ,[Option]) ");
                        foreach (var item in options)
                        {
                            insertSql.AppendFormat(" SELECT {0},{1},'{2}' UNION ALL", cproblem.EnterpriseID, cproblem.ID, item.Option);
                        }
                        var OptionSql = insertSql.ToString();
                        OptionSql = OptionSql.Remove(OptionSql.Length - " UNION ALL".Length);

                        base.SqlExecute(OptionSql);
                    }
                }
                else
                {
                    if (options != null && options.Count > 0)
                    {

                        StringBuilder updateSql = new StringBuilder("");
                        foreach (var item in options)
                        {
                            updateSql.AppendFormat(" update COption set [Option] = '{0}' where ID = {1} ", item.Option, item.ID);
                        }
                        base.SqlExecute(updateSql.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Error = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="EID"></param>
        /// <returns></returns>
        public Result DeleteInfo(int ID,int EID,int PID)
        {
            Result result = new Result();
            CustomerInfoModel customerModel = new CustomerInfoModel();
            customerModel.DeleteInfo(ID, EID);
            COptionModel optionModel = new COptionModel();
            optionModel.DeleteInfo(ID,EID);
            var cproblem = GetInfo(PID,ID,EID);
            string sql = string.Format("Delete CProblem where ID={0} and EnterpriseID={1}", ID, EID);
            string sql2 = string.Format("update CProblem set sort=sort-1 where  EnterpriseID={0} and ProjectID={1} and sort>{2}", EID, PID, cproblem.sort);
            int cnt = base.SqlExecute(sql+" "+sql2);


            return result;
        }

        /// <summary>
        /// 移动位置
        /// </summary>
        /// <param name="type">1 上移，2下移</param>
        /// <param name="sort">当前排序</param>
        /// <param name="PID">项目ID</param>
        /// <param name="CPID">问题ID</param>
        /// <returns></returns>
        public Result SortUpOrDown(int type, int sort, int CPID,int PID,int EID)
        {
            Result result = new Result();
            string sql = "";
            string sql2 = "";
            //上移
            if (type == 1)
            {
                sql = string.Format("update CProblem set sort = {0} where EnterpriseID={1} and ProjectID={2} and sort={3}",sort,EID,PID,sort-1);
                sql2 = string.Format("update CProblem set sort = {0} where EnterpriseID={1} and ProjectID={2} and ID={3}", sort - 1,EID,PID,CPID);
                base.SqlExecute(sql+" "+sql2);
            }
            //下移
            else {
                sql = string.Format("update CProblem set sort = {0} where EnterpriseID={1} and ProjectID={2} and sort={3}", sort, EID, PID, sort + 1);
                sql2 = string.Format("update CProblem set sort = {0} where EnterpriseID={1} and ProjectID={2} and ID={3}", sort + 1, EID, PID, CPID);
                base.SqlExecute(sql + " " + sql2);
            }
            return result;
        }

    }
}
