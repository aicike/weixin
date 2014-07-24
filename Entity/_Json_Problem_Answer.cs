using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    /// <summary>
    /// 客户提交信息Json
    /// </summary>
    public class Json_Problem_Answer
    {
        /// <summary>
        /// 问题ID
        /// </summary>
        public int CProblemID { get; set; }
        /// <summary>
        /// 问题类型
        /// </summary>
        public int CPType { get; set; }
        /// <summary>
        /// 选项ID
        /// </summary>
        public int COptionID { get; set; }
        /// <summary>
        /// 文本内容
        /// </summary>
        public string Content { get; set; }
    }
}
