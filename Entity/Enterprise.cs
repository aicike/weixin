using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Enterprise : BaseEntity
    {
        public int ID { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        [Display(Name = "企业名称")]
        [Required(ErrorMessage = "企业名称不能为空")]
        [StringLength(100, ErrorMessage = "字数不能大于100")]
        public string EName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        public bool Status { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreteDate { get;set;}

        /// <summary>
        /// 永久使用
        /// </summary>
        [Display(Name = "永久使用")]
        public bool Permanent { get; set; }

        /// <summary>
        /// 使用截止日期
        /// </summary>
        [Display(Name = "使用截止日期")]
        public DateTime? ClosingDate { get; set; }

        #region----------------- 子表 --------------------

        public virtual ICollection<EnterpriseInfo> EnterpriseInfo { get; set; }
        public virtual ICollection<Project> Project { get; set; }
        public virtual ICollection<EUser> EUser { get; set; }
        public virtual ICollection<CProblem> CProblem { get; set; }
        public virtual ICollection<COption> COption { get; set; }
        public virtual ICollection<CustomerInfo> CustomerInfo { get; set; }


        #endregion
    }
}
