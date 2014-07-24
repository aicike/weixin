using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class New:BaseEntity
    {
        public int ID { get; set; }

        public int ProjectID { set;get; }

        public virtual Project Project { get; set; }

        [Display(Name = "标题")]
        [Required(ErrorMessage = "标题不能为空")]
        [StringLength(20, ErrorMessage = "字数不能大于20")]
        public string Title { get; set; }

        [Required(ErrorMessage = "请上传图片")]
        public string Image { get; set; }

        [Display(Name = "内容")]
        [Required(ErrorMessage = "内容不能为空")]
        public string Content { get; set; }

        [Display(Name = "浏览次数")]
        public int ViewCount { get; set; }

        public bool IsEnabled { get; set; }

        public int Sort { get; set; }
    }
}
