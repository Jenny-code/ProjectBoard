namespace ProjectBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class includedNotificatInIdentityModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserProjects", newName: "ProjectApplicationUsers");
            DropPrimaryKey("dbo.ProjectApplicationUsers");
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        Opened = c.Boolean(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        AssignmentId = c.Int(),
                        ProjectId = c.Int(),
                        Notificationtype = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.ATasks", t => t.AssignmentId)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.AssignmentId)
                .Index(t => t.ProjectId);
            
            AddPrimaryKey("dbo.ProjectApplicationUsers", new[] { "Project_Id", "ApplicationUser_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Notifications", "AssignmentId", "dbo.ATasks");
            DropForeignKey("dbo.Notifications", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Notifications", new[] { "ProjectId" });
            DropIndex("dbo.Notifications", new[] { "AssignmentId" });
            DropIndex("dbo.Notifications", new[] { "ApplicationUserId" });
            DropPrimaryKey("dbo.ProjectApplicationUsers");
            DropTable("dbo.Notifications");
            AddPrimaryKey("dbo.ProjectApplicationUsers", new[] { "ApplicationUser_Id", "Project_Id" });
            RenameTable(name: "dbo.ProjectApplicationUsers", newName: "ApplicationUserProjects");
        }
    }
}
