namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EnterpriseInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SName = c.String(nullable: false, maxLength: 10),
                        Logo = c.String(nullable: false),
                        ShareTitle = c.String(nullable: false, maxLength: 50),
                        ShareRemark = c.String(maxLength: 50),
                        BgImage = c.String(),
                        ButtonName = c.String(maxLength: 8),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PName = c.String(nullable: false, maxLength: 100),
                        MapAddress = c.String(),
                        Phone = c.String(nullable: false, maxLength: 100),
                        Lng = c.String(),
                        Lat = c.String(),
                        IsEnabled = c.Boolean(nullable: false),
                        EnterpriseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Enterprise", t => t.EnterpriseID)
                .Index(t => t.EnterpriseID);
            
            CreateTable(
                "dbo.Enterprise",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EUser",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EnterpriseID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 20),
                        PassWord = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Enterprise", t => t.EnterpriseID)
                .Index(t => t.EnterpriseID);
            
            CreateTable(
                "dbo.ImageInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        ProjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Project", t => t.ProjectID)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.SystemUser",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 20),
                        PassWord = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImageInfo", "ProjectID", "dbo.Project");
            DropForeignKey("dbo.Project", "EnterpriseID", "dbo.Enterprise");
            DropForeignKey("dbo.EUser", "EnterpriseID", "dbo.Enterprise");
            DropIndex("dbo.ImageInfo", new[] { "ProjectID" });
            DropIndex("dbo.EUser", new[] { "EnterpriseID" });
            DropIndex("dbo.Project", new[] { "EnterpriseID" });
            DropTable("dbo.SystemUser");
            DropTable("dbo.ImageInfo");
            DropTable("dbo.EUser");
            DropTable("dbo.Enterprise");
            DropTable("dbo.Project");
            DropTable("dbo.EnterpriseInfo");
        }
    }
}
