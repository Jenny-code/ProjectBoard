namespace ProjectBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImproveATask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ATasks", "Body", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ATasks", "Body");
        }
    }
}
