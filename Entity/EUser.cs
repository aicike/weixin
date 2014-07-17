using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class EUser: BaseEntity
    {
        public int ID { get; set; }

        /// <summary>
        /// 企业表
        /// </summary>
        public int EnterpriseID { get; set; }
        public virtual Enterprise Enterprise { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "账号")]
        [Required(ErrorMessage = "账号不能为空")]
        [StringLength(20, ErrorMessage = "字数不能大于20")]
        public string Name { get; set; }

        /// <summary>
        /// 电话(登陆用)
        /// </summary>
        [Display(Name = "电话(登陆用)")]
        [StringLength(20, ErrorMessage = "字数不能大于20")]
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱(登陆用)
        /// </summary>
        [Display(Name = "邮箱(登陆用)")]
        [StringLength(20, ErrorMessage = "字数不能大于20")]
        public string Email { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        [Required(ErrorMessage = "密码不能为空")]
        [StringLength(20, ErrorMessage = "字数不能大于20")]
        public string PassWord { get; set; }
    }
}
