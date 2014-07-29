using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Entity;

namespace EF.Mapping
{
    public class NewMap: EntityTypeConfiguration<New>
    {
        public NewMap()
        {
            this.HasRequired(a => a.Project)
            .WithMany(a => a.News)
            .HasForeignKey(a => a.ProjectID).WillCascadeOnDelete(true);
        }
    }
}
