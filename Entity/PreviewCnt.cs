using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    //预览总次数
    public class PreviewCnt : BaseEntity
    {
        public int ID { get; set; }

        public int EID { get; set; }

        public int Cnt { get; set; }

    }
}
