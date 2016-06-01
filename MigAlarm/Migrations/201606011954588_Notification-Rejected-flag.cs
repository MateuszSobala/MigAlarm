namespace MigAlarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationRejectedflag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "Rejected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "Rejected");
        }
    }
}
