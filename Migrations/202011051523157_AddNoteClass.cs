namespace ProjectBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNoteClass : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notes", "ATaskId", "dbo.ATasks");
            DropIndex("dbo.Notes", new[] { "ATaskId" });
            DropTable("dbo.Notes");
        }
    }
}
