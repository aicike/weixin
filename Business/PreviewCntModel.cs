using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entity;

namespace Business
{
    public class PreviewCntModel : BaseModel<PreviewCnt>
    {
        //打开次数统计
        public Result CreateOrAddCnt(int EID)
        {
            Result result = new Result();
            string sql = "";
            var list = List().Where(a=>a.EID==EID);
            if (list != null)
            {
                //增加
                if (list.Count() > 0)
                {
                    sql = "update PreviewCnt set [Cnt]= ([Cnt]+1) where EID = " + EID;
                }
                else
                {
                    sql = string.Format("insert into  PreviewCnt (EID,Cnt) values({0},0)" ,EID);
                }
            }
            else
            {
                sql = string.Format("insert into  PreviewCnt (EID,Cnt) values({0},0)", EID);
            }
            base.SqlExecute(sql); 
            return result;
        }

        public int GetPreviewCnt(int EID)
        {
            var item = List().Where(a=>a.EID==EID).FirstOrDefault().Cnt;
            return item;
        }
    }
}
