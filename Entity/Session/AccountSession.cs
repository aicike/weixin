using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Session
{
     [Serializable]
    public class AccountSession
    {
         /// <summary>
         /// 企业ID
         /// </summary>
         public int EnterpriseID { get; set; }

         /// <summary>
         /// 企业名称
         /// </summary>
         public string  EnterpriseName { get; set; }

         /// <summary>
         /// 企业创建日期
         /// </summary>
         public DateTime EnterpriseCreateDate { get; set; }

         /// <summary>
         /// 是否永久使用
         /// </summary>
         public bool Permanent { get; set; }

         /// <summary>
         /// 企业截止使用日期
         /// </summary>
         public DateTime? ClosingDate { get; set; }

        
         /// <summary>
         /// 用户ID
         /// </summary>
         public int UserID { get; set; }

         /// <summary>
         /// 用户名称
         /// </summary>
         public string UserName { get; set; }
    }
}
