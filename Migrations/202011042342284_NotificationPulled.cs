namespace ProjectBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationPulled : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        ATaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ATasks", t => t.ATaskId, cascadeDelete: true)
                .Index(t => t.ATaskId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        Opened = c.Boolean(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        ATaskId = c.Int(),
                        ProjectId = c.Int(),
                        Notificationtype = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .ForeignKey("dbo.ATasks", t => t.ATaskId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.ATaskId)
                .Index(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "ATaskId", "dbo.ATasks");
            DropForeignKey("dbo.Notifications", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Notifications", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notes", "ATaskId", "dbo.ATasks");
            DropIndex("dbo.Notifications", new[] { "ProjectId" });
            DropIndex("dbo.Notifications", new[] { "ATaskId" });
            DropIndex("dbo.Notifications", new[] { "ApplicationUserId" });
            DropIndex("dbo.Notes", new[] { "ATaskId" });
            DropTable("dbo.Notifications");
            DropTable("dbo.Notes");
        }
    }
}
