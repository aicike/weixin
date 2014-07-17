using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Project : BaseEntity
    {
        public int ID { get; set; }

        /// <summary>
        /// 企业表
        /// </summary>
        public int EnterpriseID { get; set; }
        public virtual Enterprise Enterprise { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [Display(Name = "项目名称")]
        [Required(ErrorMessage = "项目名称不能为空")]
        [StringLength(100, ErrorMessage = "字数不能大于100")]
        public string PName { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
        [Display(Name = "Logo")]
        [Required(ErrorMessage = "Logo不能为空")]
        public string PLogo { get; set; }

    }
}
