namespace ProjectBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeadlinePropertyToProjectAndTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Deadline", c => c.DateTime(nullable: false));
            AddColumn("dbo.ATasks", "Deadline", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ATasks", "Deadline");
            DropColumn("dbo.Projects", "Deadline");
        }
    }
}
