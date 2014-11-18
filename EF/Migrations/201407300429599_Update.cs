namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EnterpriseInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EnterpriseID = c.Int(nullable: false),
                        SName = c.String(maxLength: 10),
                        Logo = c.String(),
                        ShareTitle = c.String(maxLength: 50),
                        ShareRemark = c.String(maxLength: 50),
                        BgImage = c.String(),
                        ButtonName = c.String(maxLength: 8),
                        PreviewCnt = c.Int(nullable: false),
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
                        Status = c.Boolean(nullable: false),
                        CreteDate = c.DateTime(nullable: false),
                        Permanent = c.Boolean(nullable: false),
                        ClosingDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.COption",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EnterpriseID = c.Int(nullable: false),
                        CProblemID = c.Int(nullable: false),
                        Option = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CProblem", t => t.CProblemID, cascadeDelete: true)
                .ForeignKey("dbo.Enterprise", t => t.EnterpriseID)
                .Index(t => t.EnterpriseID)
                .Index(t => t.CProblemID);
            
            CreateTable(
                "dbo.CProblem",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EnterpriseID = c.Int(nullable: false),
                        ProjectID = c.Int(nullable: false),
                        Problem = c.String(nullable: false, maxLength: 100),
                        ProblemType = c.Int(nullable: false),
                        sort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Enterprise", t => t.EnterpriseID)
                .ForeignKey("dbo.Project", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.EnterpriseID)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.CustomerInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EnterpriseID = c.Int(nullable: false),
                        ProjectID = c.Int(nullable: false),
                        CProblemID = c.Int(nullable: false),
                        COptionID = c.Int(),
                        Identification = c.String(maxLength: 100),
                        ProblemText = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.COption", t => t.COptionID)
                .ForeignKey("dbo.CProblem", t => t.CProblemID, cascadeDelete: true)
                .ForeignKey("dbo.Enterprise", t => t.EnterpriseID)
                .ForeignKey("dbo.Project", t => t.ProjectID)
                .Index(t => t.EnterpriseID)
                .Index(t => t.ProjectID)
                .Index(t => t.CProblemID)
                .Index(t => t.COptionID);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PName = c.String(nullable: false, maxLength: 100),
                        MapAddress = c.String(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 100),
                        Lng = c.String(),
                        Lat = c.String(),
                        IsEnabled = c.Boolean(nullable: false),
                        EnterpriseID = c.Int(nullable: false),
                        sort = c.Int(nullable: false),
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
                .ForeignKey("dbo.Project", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.New",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjectID = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 20),
                        Image = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        IsEnabled = c.Boolean(nullable: false),
                        Sort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Project", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.EUser",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EnterpriseID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 20),
                        PassWord = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Enterprise", t => t.EnterpriseID)
                .Index(t => t.EnterpriseID);
            
            CreateTable(
                "dbo.PreviewCnt",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EID = c.Int(nullable: false),
                        Cnt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
            DropForeignKey("dbo.EUser", "EnterpriseID", "dbo.Enterprise");
            DropForeignKey("dbo.EnterpriseInfo", "EnterpriseID", "dbo.Enterprise");
            DropForeignKey("dbo.COption", "EnterpriseID", "dbo.Enterprise");
            DropForeignKey("dbo.COption", "CProblemID", "dbo.CProblem");
            DropForeignKey("dbo.CProblem", "ProjectID", "dbo.Project");
            DropForeignKey("dbo.CProblem", "EnterpriseID", "dbo.Enterprise");
            DropForeignKey("dbo.CustomerInfo", "ProjectID", "dbo.Project");
            DropForeignKey("dbo.New", "ProjectID", "dbo.Project");
            DropForeignKey("dbo.ImageInfo", "ProjectID", "dbo.Project");
            DropForeignKey("dbo.Project", "EnterpriseID", "dbo.Enterprise");
            DropForeignKey("dbo.CustomerInfo", "EnterpriseID", "dbo.Enterprise");
            DropForeignKey("dbo.CustomerInfo", "CProblemID", "dbo.CProblem");
            DropForeignKey("dbo.CustomerInfo", "COptionID", "dbo.COption");
            DropIndex("dbo.EUser", new[] { "EnterpriseID" });
            DropIndex("dbo.New", new[] { "ProjectID" });
            DropIndex("dbo.ImageInfo", new[] { "ProjectID" });
            DropIndex("dbo.Project", new[] { "EnterpriseID" });
            DropIndex("dbo.CustomerInfo", new[] { "COptionID" });
            DropIndex("dbo.CustomerInfo", new[] { "CProblemID" });
            DropIndex("dbo.CustomerInfo", new[] { "ProjectID" });
            DropIndex("dbo.CustomerInfo", new[] { "EnterpriseID" });
            DropIndex("dbo.CProblem", new[] { "ProjectID" });
            DropIndex("dbo.CProblem", new[] { "EnterpriseID" });
            DropIndex("dbo.COption", new[] { "CProblemID" });
            DropIndex("dbo.COption", new[] { "EnterpriseID" });
            DropIndex("dbo.EnterpriseInfo", new[] { "EnterpriseID" });
            DropTable("dbo.SystemUser");
            DropTable("dbo.PreviewCnt");
            DropTable("dbo.EUser");
            DropTable("dbo.New");
            DropTable("dbo.ImageInfo");
            DropTable("dbo.Project");
            DropTable("dbo.CustomerInfo");
            DropTable("dbo.CProblem");
            DropTable("dbo.COption");
            DropTable("dbo.Enterprise");
            DropTable("dbo.EnterpriseInfo");
        }
    }
}
