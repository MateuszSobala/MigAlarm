namespace MigAlarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LargeAdditionalData : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AdditionalDatas", "Text", c => c.String(nullable: false, storeType: "ntext"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AdditionalDatas", "Text", c => c.String(nullable: false));
        }
    }
}
