using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Entity;

namespace EF.Mapping
{
    public class ImageInfoMap: EntityTypeConfiguration<ImageInfo>
    {
        public ImageInfoMap()
        {
            this.HasRequired(a => a.Project)
            .WithMany(a => a.ImageInfos)
            .HasForeignKey(a => a.ProjectID).WillCascadeOnDelete(true);
        }
    }
}
