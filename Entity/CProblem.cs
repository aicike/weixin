using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    /// <summary>
    /// 客户 问提主表
    /// </summary>
    public class CProblem : BaseEntity
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
        /// 问题
        /// </summary>
        [Display(Name = "问题")]
        [Required(ErrorMessage = "标题不能为空")]
        [StringLength(100, ErrorMessage = "字数不能大于100")]
        public string Problem { get; set; }

        /// <summary>
        /// 选项类型 Enum （EnumCProblemProblemType）
        /// </summary>
        [Display(Name = "选项类型")]
        public int ProblemType { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "选项类型")]
        public int sort { get; set; }

        //----------------子表-----------------------

        public virtual ICollection<COption> COption { get; set; }

        public virtual ICollection<CustomerInfo> CustomerInfo { get; set; }
    }

}
