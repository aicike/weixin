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

       
        


        #region----------------- 子表 --------------------

        public virtual ICollection<Project> Project { get; set; }

        public virtual ICollection<EUser> EUser { get; set; }


        #endregion
    }
}
