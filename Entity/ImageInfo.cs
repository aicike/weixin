using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class ImageInfo:BaseEntity
    {
        public int ID { get; set; }

        public string Path { get; set; }

        public int ProjectID { get; set; }

        public virtual Project Project { get; set; }
    }
}
