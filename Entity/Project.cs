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
        /// 项目名称
        /// </summary>
        [Display(Name = "项目名称")]
        [Required(ErrorMessage = "项目名称不能为空")]
        [StringLength(100, ErrorMessage = "字数不能大于100")]
        public string PName { get; set; }

        [Display(Name = "售楼地图定位地址")]
        public string MapAddress { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [Display(Name = "项目电话")]
        [Required(ErrorMessage = "项目电话不能为空")]
        [StringLength(100, ErrorMessage = "字数不能大于100")]
        public string Phone { get; set; }

        [Display(Name = "纬度")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string Lng { get; set; }

        [Display(Name = "经度")]
        [RegularExpression("^((?!<!).)*", ErrorMessage = "{0}中含有非法字符。")]
        public string Lat { get; set; }

        /// <summary>
        /// 启用，禁用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 企业表
        /// </summary>
        public int EnterpriseID { get; set; }
        public virtual Enterprise Enterprise { get; set; }



        public virtual ICollection<ImageInfo> ImageInfos { get; set; }


    }
}
