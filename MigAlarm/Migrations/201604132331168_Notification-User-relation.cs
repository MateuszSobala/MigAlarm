namespace MigAlarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationUserrelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "UserId", "dbo.Users");
            DropIndex("dbo.Notifications", new[] { "UserId" });
            AlterColumn("dbo.Notifications", "UserId", c => c.Int());
            CreateIndex("dbo.Notifications", "UserId");
            AddForeignKey("dbo.Notifications", "UserId", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "UserId", "dbo.Users");
            DropIndex("dbo.Notifications", new[] { "UserId" });
            AlterColumn("dbo.Notifications", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Notifications", "UserId");
            AddForeignKey("dbo.Notifications", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
