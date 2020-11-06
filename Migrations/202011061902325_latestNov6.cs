namespace ProjectBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class latestNov6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Projects", "Deadline");
            DropColumn("dbo.Projects", "Priority");
            DropColumn("dbo.ATasks", "Deadline");
            DropColumn("dbo.ATasks", "Priority");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ATasks", "Priority", c => c.Int(nullable: false));
            AddColumn("dbo.ATasks", "Deadline", c => c.DateTime(nullable: false));
            AddColumn("dbo.Projects", "Priority", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "Deadline", c => c.DateTime(nullable: false));
        }
    }
}
