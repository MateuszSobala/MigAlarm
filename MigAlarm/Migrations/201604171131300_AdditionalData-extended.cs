namespace MigAlarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdditionalDataextended : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Notifications", "PhoneNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "PhoneNumber", c => c.String(nullable: false));
        }
    }
}
