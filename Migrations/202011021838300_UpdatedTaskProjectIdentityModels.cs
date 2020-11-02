namespace ProjectBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedTaskProjectIdentityModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TaskUsers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TaskUsers", "ATask_Id", "dbo.ATasks");
            DropForeignKey("dbo.ATasks", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectUsers", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectUsers", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Projects", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ProjectUsers", new[] { "ProjectId" });
            DropIndex("dbo.ProjectUsers", new[] { "ApplicationUserId" });
            DropIndex("dbo.ATasks", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TaskUsers", new[] { "ApplicationUserId" });
            DropIndex("dbo.TaskUsers", new[] { "ATask_Id" });
            CreateTable(
                "dbo.ApplicationUserProjects",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Project_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Project_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.Project_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.ATaskApplicationUsers",
                c => new
                    {
                        ATask_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ATask_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.ATasks", t => t.ATask_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.ATask_Id)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.AspNetUsers", "DailySalary", c => c.Double(nullable: false));
            DropColumn("dbo.Projects", "ApplicationUser_Id");
            DropColumn("dbo.ATasks", "ApplicationUser_Id");
            DropTable("dbo.ProjectUsers");
            DropTable("dbo.TaskUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TaskUsers",
                c => new
                    {
                        AssignmentId = c.Int(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        ATask_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.AssignmentId, t.ApplicationUserId });
            
            CreateTable(
                "dbo.ProjectUsers",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.ApplicationUserId });
            
            AddColumn("dbo.ATasks", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Projects", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.ATaskApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ATaskApplicationUsers", "ATask_Id", "dbo.ATasks");
            DropForeignKey("dbo.ApplicationUserProjects", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.ApplicationUserProjects", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ATaskApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ATaskApplicationUsers", new[] { "ATask_Id" });
            DropIndex("dbo.ApplicationUserProjects", new[] { "Project_Id" });
            DropIndex("dbo.ApplicationUserProjects", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "DailySalary");
            DropTable("dbo.ATaskApplicationUsers");
            DropTable("dbo.ApplicationUserProjects");
            CreateIndex("dbo.TaskUsers", "ATask_Id");
            CreateIndex("dbo.TaskUsers", "ApplicationUserId");
            CreateIndex("dbo.ATasks", "ApplicationUser_Id");
            CreateIndex("dbo.ProjectUsers", "ApplicationUserId");
            CreateIndex("dbo.ProjectUsers", "ProjectId");
            CreateIndex("dbo.Projects", "ApplicationUser_Id");
            AddForeignKey("dbo.ProjectUsers", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProjectUsers", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ATasks", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.TaskUsers", "ATask_Id", "dbo.ATasks", "Id");
            AddForeignKey("dbo.TaskUsers", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Projects", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
