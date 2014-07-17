using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class EnterpriseInfo : BaseEntity
    {
        public int ID { get; set; }

        /// <summary>
        /// 企业表
        /// </summary>
        public int EnterpriseID { get; set; }
        public virtual Enterprise Enterprise { get; set; }

        /// <summary>
        /// 企业简称
        /// </summary>
        [Display(Name = "企业简称")]
        [StringLength(10, ErrorMessage = "字数不能大于10")]
        public string SName { get; set; }

        /// <summary>
        /// 企业logo
        /// </summary>
        [Display(Name = "企业logo")]
        public string Logo { get; set; }


        /// <summary>
        /// 分享标题
        /// </summary>
        [Display(Name = "分享标题")]
        [StringLength(50, ErrorMessage = "字数不能大于50")]
        public string ShareTitle { get; set; }

        /// <summary>
        /// 分享备注
        /// </summary>
        [Display(Name = "分享备注")]
        [StringLength(50, ErrorMessage = "字数不能大于50")]
        public string ShareRemark { get; set; }

        /// <summary>
        /// 首页背景
        /// </summary>
        [Display(Name = "首页背景")]
        public string BgImage { get; set; }

        /// <summary>
        /// 按钮名称
        /// </summary>
        [Display(Name = "按钮名称")]
        [StringLength(8, ErrorMessage = "字数不能大于8")]
        public string ButtonName { get; set; }
    }
}
