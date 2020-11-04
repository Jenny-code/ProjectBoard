namespace ProjectBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notifcontroller : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Notifications", name: "AssignmentId", newName: "ATaskId");
            RenameIndex(table: "dbo.Notifications", name: "IX_AssignmentId", newName: "IX_ATaskId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Notifications", name: "IX_ATaskId", newName: "IX_AssignmentId");
            RenameColumn(table: "dbo.Notifications", name: "ATaskId", newName: "AssignmentId");
        }
    }
}
