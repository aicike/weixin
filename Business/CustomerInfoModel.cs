using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using System.Data;
using Entity.Enum;

namespace Business
{
    public class CustomerInfoModel : BaseModel<CustomerInfo>
    {
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="CPID">问题ID</param>
        /// <param name="EID">企业ID</param>
        /// <returns></returns>
        public Result DeleteInfo(int CPID, int EID)
        {
            Result result = new Result();
            string sql = string.Format("Delete CustomerInfo where CProblemID={0} and EnterpriseID={1}", CPID, EID);
            int cnt = base.SqlExecute(sql);
            return result;
        }

        /// <summary>
        /// 提交用户信息
        /// </summary>
        /// <param name="PID">项目ID</param>
        /// <param name="EID">企业ID</param>
        /// <param name="jpaList">问题</param>
        /// <returns></returns>
        public Result AddInfo(int PID, int EID, List<Json_Problem_Answer> jpaList)
        {
            Business.Common.Common com = new Business.Common.Common();
            Result result = new Result();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("EnterpriseID");
            dt.Columns.Add("ProjectID");
            dt.Columns.Add("CProblemID");
            dt.Columns.Add("COptionID");
            dt.Columns.Add("Identification");
            dt.Columns.Add("ProblemText");
            //用户标识
            string ProblemText = com.CreateRandom("", 6) + DateTime.Now.ToString("yyyyMMddss");
            foreach (var item in jpaList)
            {
                DataRow row = dt.NewRow();
                row["EnterpriseID"] = EID;
                row["ProjectID"] = PID;
                row["CProblemID"] = item.CProblemID;
                if (item.CPType == (int)EnumCProblemProblemType.Check || item.CPType == (int)EnumCProblemProblemType.Radio)
                {
                    row["COptionID"] = item.COptionID;
                }
                row["Identification"] = ProblemText;
                row["ProblemText"] = item.Content;
                dt.Rows.Add(row);
            }
            result = com.CopyDataTableToDB(dt, "CustomerInfo");

            return result;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="EID"></param>
        /// <returns></returns>
        public DataTable getDtInfo(int PID, int EID)
        {
            Business.Common.Common com = new Business.Common.Common();
            DataTable dt = new DataTable();
            CProblemModel cproblemModel = new CProblemModel();
            //表格标题
            var cproblelist = cproblemModel.GetList_ByPID(PID, EID).ToList();
            foreach (var item in cproblelist)
            {
                dt.Columns.Add(item.Problem);
            }
            //所有用户信息
            string sqlUserList = string.Format("select a.Identification,a.ProblemText,"
                                            + "(select Problem from CProblem where id=a.CproblemID) as CproblemName,"
                                            + "(select ProblemType from CProblem where id=a.CproblemID) as CproblemType,"
                                            + "(select [Option] from COption where id=a.COptionID) as COptionName from dbo.CustomerInfo a "
                                            + " where a.EnterpriseID={0} and ProjectID={1}", EID, PID);
            var customerInfo = com.SqlQuery<DB_CutomerInfoUserList>(sqlUserList).ToList();
            //所有用户标识
            string sqlUser = string.Format("select Identification,count(Identification) as cnt from dbo.CustomerInfo where EnterpriseID={0} and ProjectID={1}  group by Identification",EID,PID);
            var userlist = com.SqlQuery<DB_CutomerInfoUserBS>(sqlUser).ToList();
            int cnt = 0;
            //循环用户标识
            foreach (var item in userlist)
            {
                //循环用户列表
                DataRow row = dt.NewRow();
                foreach (var item2 in customerInfo)
                {
                    if (cnt == item.cnt )
                    {
                        cnt = 0;
                        break;
                    }

                    if (item2.Identification == item.Identification)
                    {

                        if (item2.CproblemType == (int)EnumCProblemProblemType.Check)
                        {

                            row[item2.CproblemName] = row[item2.CproblemName] + item2.COptionName + ",";
                        }
                        else if (item2.CproblemType == (int)EnumCProblemProblemType.Radio)
                        {
                            row[item2.CproblemName] = item2.COptionName;
                        }
                        else
                        {
                            row[item2.CproblemName] = item2.ProblemText;
                        }
                        cnt++;
                    }

                }
                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}
