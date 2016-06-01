namespace MigAlarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "Comment", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "Comment");
        }
    }
}
