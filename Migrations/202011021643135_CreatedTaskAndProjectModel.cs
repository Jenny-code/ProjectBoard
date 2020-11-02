namespace ProjectBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedTaskAndProjectModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        Deadline = c.DateTime(nullable: false),
                        Budget = c.Double(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        Priority = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ProjectUsers",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.ApplicationUserId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.ATasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        Deadline = c.DateTime(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        CompletionPerc = c.Int(nullable: false),
                        Priority = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ProjectId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.TaskUsers",
                c => new
                    {
                        AssignmentId = c.Int(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        ATask_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.AssignmentId, t.ApplicationUserId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.ATasks", t => t.ATask_Id)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.ATask_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectUsers", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectUsers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ATasks", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TaskUsers", "ATask_Id", "dbo.ATasks");
            DropForeignKey("dbo.TaskUsers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ATasks", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.TaskUsers", new[] { "ATask_Id" });
            DropIndex("dbo.TaskUsers", new[] { "ApplicationUserId" });
            DropIndex("dbo.ATasks", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ATasks", new[] { "ProjectId" });
            DropIndex("dbo.ProjectUsers", new[] { "ApplicationUserId" });
            DropIndex("dbo.ProjectUsers", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "ApplicationUser_Id" });
            DropTable("dbo.TaskUsers");
            DropTable("dbo.ATasks");
            DropTable("dbo.ProjectUsers");
            DropTable("dbo.Projects");
        }
    }
}
