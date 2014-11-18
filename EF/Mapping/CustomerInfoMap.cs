using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Entity;

namespace EF.Mapping
{
    public class CustomerInfoMap : EntityTypeConfiguration<CustomerInfo>
    {
        public CustomerInfoMap()
        {
            this.HasRequired(a => a.Project)
            .WithMany(a => a.CustomerInfo)
            .HasForeignKey(a => a.ProjectID).WillCascadeOnDelete(false);

            this.HasRequired(a => a.CProblem)
            .WithMany(a => a.CustomerInfo)
            .HasForeignKey(a => a.CProblemID).WillCascadeOnDelete(true);
        }
    }
}
