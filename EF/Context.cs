using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Entity;


namespace EF
{
    public class Context : DbContext
    {
        public DbSet<SystemUser> SystemUser { get; set; }
        public DbSet<EnterpriseInfo> EnterpriseInfo { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<PreviewCnt> PreviewCnt { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

          
        }
    }
}
