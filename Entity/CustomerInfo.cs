using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    /// <summary>
    /// 客户回答信息表
    /// </summary>
    public class CustomerInfo:BaseEntity
    {
        public int ID { get; set; }

        /// <summary>
        /// 企业表
        /// </summary>
        public int EnterpriseID { get; set; }
        public virtual Enterprise Enterprise { get; set; }

        /// <summary>
        /// 项目表
        /// </summary>
        public int ProjectID { get; set; }
        public virtual Project Project { get; set; }

        /// <summary>
        /// 问题表
        /// </summary>
        public int CProblemID { get; set; }
        public virtual CProblem CProblem { get; set; }

        /// <summary>
        /// 选项表
        /// </summary>
        public int? COptionID { get; set; }
        public virtual COption COption { get; set; }

        /// <summary>
        /// 用户标识
        /// </summary>
        [Display(Name = "用户标识")]
        [StringLength(100, ErrorMessage = "字数不能大于100")]
        public string Identification { get; set; }

        /// <summary>
        /// 用户回答文本内容
        /// </summary>
        [Display(Name = "用户回答文本内容")]
        [StringLength(500, ErrorMessage = "字数不能大于500")]
        public string ProblemText { get; set; }
    }
}
