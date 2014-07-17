using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class SystemUser : BaseEntity
    {
        public int ID { get; set; }

        [Display(Name = "姓名")]
        [Required(ErrorMessage = "姓名不能为空")]
        [StringLength(20, ErrorMessage = "字数不能大于20")]
        public string Name { get; set; }

        [Display(Name = "电话(登陆用)")]
        [Required(ErrorMessage = "电话不能为空")]
        [StringLength(20, ErrorMessage = "字数不能大于20")]
        public string Phone { get; set; }

        [Display(Name = "邮箱(登陆用)")]
        [Required(ErrorMessage = "邮箱不能为空")]
        [StringLength(20, ErrorMessage = "字数不能大于20")]
        public string Email { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "邮箱不能为空")]
        [StringLength(20, ErrorMessage = "字数不能大于20")]
        public string PassWord { get; set; }

    }
}
