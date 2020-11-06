namespace ProjectBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriorityEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ATasks", "Priority", c => c.Int(nullable: false));
            DropColumn("dbo.ATasks", "Deadline");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ATasks", "Deadline", c => c.DateTime(nullable: false));
            DropColumn("dbo.ATasks", "Priority");
        }
    }
}
