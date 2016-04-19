namespace MigAlarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userloggedininfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "LastLogin", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Users", "IsLoggedIn", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsLoggedIn");
            DropColumn("dbo.Users", "LastLogin");
        }
    }
}
