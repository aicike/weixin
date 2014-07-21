using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;

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
    }
}
