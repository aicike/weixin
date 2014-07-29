using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Entity;

namespace EF.Mapping
{
    public class CProblemMap : EntityTypeConfiguration<CProblem>
    {
        public CProblemMap()
        {
            this.HasRequired(a => a.Project)
            .WithMany(a => a.CProblem)
            .HasForeignKey(a => a.ProjectID).WillCascadeOnDelete(true);
        }
    }
}
