using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Entity;

namespace EF.Mapping
{
    public class COptionMap : EntityTypeConfiguration<COption>
    {
        public COptionMap()
        {
            this.HasRequired(a => a.CProblem)
            .WithMany(a => a.COption)
            .HasForeignKey(a => a.CProblemID).WillCascadeOnDelete(true);
        }
    }
}
