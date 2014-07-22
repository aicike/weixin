using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;

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
            string sql = string.Format("Delete CustomerInfo where CProblemID={0} and EnterpriseID={1}",CPID,EID);
            int cnt = base.SqlExecute(sql);
            return result;
        }
    }
}
