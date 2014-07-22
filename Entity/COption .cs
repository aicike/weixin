using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    /// <summary>
    /// 问题选项表
    /// </summary>
    public class COption:BaseEntity
    {
        public int ID { get; set; }

        /// <summary>
        /// 企业表
        /// </summary>
        public int EnterpriseID { get; set; }
        public virtual Enterprise Enterprise { get; set; }

        /// <summary>
        /// 问题表
        /// </summary>
        public int CProblemID { get; set; }
        public virtual CProblem CProblem { get; set; }

        /// <summary>
        /// 选项
        /// </summary>
        [Display(Name = "选项")]
        [Required(ErrorMessage = "选项不能为空")]
        [StringLength(100, ErrorMessage = "字数不能大于100")]
        public string Option { get; set; }

        //----------------子表-----------------------

        public virtual ICollection<CustomerInfo> CustomerInfo { get; set; }
    }
}
