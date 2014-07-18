namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EnterpriseInfo", "EnterpriseID", c => c.Int(nullable: false));
            AlterColumn("dbo.EnterpriseInfo", "SName", c => c.String(maxLength: 10));
            AlterColumn("dbo.EnterpriseInfo", "Logo", c => c.String());
            AlterColumn("dbo.EnterpriseInfo", "ShareTitle", c => c.String(maxLength: 50));
            AlterColumn("dbo.EUser", "Email", c => c.String(maxLength: 20));
            CreateIndex("dbo.EnterpriseInfo", "EnterpriseID");
            AddForeignKey("dbo.EnterpriseInfo", "EnterpriseID", "dbo.Enterprise", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EnterpriseInfo", "EnterpriseID", "dbo.Enterprise");
            DropIndex("dbo.EnterpriseInfo", new[] { "EnterpriseID" });
            AlterColumn("dbo.EUser", "Email", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.EnterpriseInfo", "ShareTitle", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.EnterpriseInfo", "Logo", c => c.String(nullable: false));
            AlterColumn("dbo.EnterpriseInfo", "SName", c => c.String(nullable: false, maxLength: 10));
            DropColumn("dbo.EnterpriseInfo", "EnterpriseID");
        }
    }
}
