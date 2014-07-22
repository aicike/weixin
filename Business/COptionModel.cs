using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;

namespace Business
{
    public class COptionModel : BaseModel<COption>
    {
        /// <summary>
        /// 删除选项信息
        /// </summary>
        /// <param name="CPID">问题ID</param>
        /// <param name="EID">企业ID</param>
        /// <returns></returns>
        public Result DeleteInfo(int CPID, int EID)
        {
            Result result = new Result();
            string sql = string.Format("Delete COption where CProblemID={0} and EnterpriseID={1}", CPID, EID);
            int cnt = base.SqlExecute(sql);
            return result;
        }
    }
}
