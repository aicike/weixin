using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;
using System.Web;
using Entity.Session;

namespace Business
{
    public class EUserModel : BaseModel<EUser>
    {

        /// <summary>
        /// 企业用户登录
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        public Result Login(string Name, string Pwd)
        {
            Result result = new Result();
            var Euser = List().Where(a => a.Name.Contains(Name) && a.PassWord == Pwd).FirstOrDefault();
            if (Euser != null)
            {
                AccountSession assion = new AccountSession();
                assion.EnterpriseID = Euser.Enterprise.ID;
                assion.EnterpriseName = Euser.Enterprise.EName;
                assion.UserID = Euser.ID;
                assion.UserName = Euser.Name;
                assion.EnterpriseCreateDate = Euser.Enterprise.CreteDate;
                assion.Permanent = Euser.Enterprise.Permanent;
                assion.ClosingDate = Euser.Enterprise.ClosingDate;
                HttpContext.Current.Session[SystemConst.Session.LoginAccount] = assion;
                return result;
            }
            else { 
                result.HasError=true;
                result.Error="账号密码错误";
                return result;
            }
        }

        /// <summary>
        /// 根据企业ID 删除用户
        /// </summary>
        /// <param name="EID"></param>
        /// <returns></returns>
        public Result Delte_byEID(int EID)
        {
            Result result = new Result();
            string sql = "delete EUser where EnterpriseID=" + EID;
            int cnt = base.SqlExecute(sql);
            if (cnt <= 0)
            {
                result.HasError = true;
                result.Error = "删除该企业下的用户失败! 请稍后再试";
            }
            return result;
        }


    }
}
