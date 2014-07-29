namespace EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ImageInfo", "ProjectID", "dbo.Project");
            DropForeignKey("dbo.New", "ProjectID", "dbo.Project");
            AddForeignKey("dbo.ImageInfo", "ProjectID", "dbo.Project", "ID", cascadeDelete: true);
            AddForeignKey("dbo.New", "ProjectID", "dbo.Project", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.New", "ProjectID", "dbo.Project");
            DropForeignKey("dbo.ImageInfo", "ProjectID", "dbo.Project");
            AddForeignKey("dbo.New", "ProjectID", "dbo.Project", "ID");
            AddForeignKey("dbo.ImageInfo", "ProjectID", "dbo.Project", "ID");
        }
    }
}
