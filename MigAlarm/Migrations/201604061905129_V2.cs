namespace MigAlarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AdditionalDatas", "AdditionalDataTypeId", "dbo.AdditionalDataTypes");
            DropIndex("dbo.AdditionalDatas", new[] { "AdditionalDataTypeId" });
            AddColumn("dbo.AdditionalDatas", "AdditionalDataType", c => c.Int(nullable: false));
            DropColumn("dbo.AdditionalDatas", "AdditionalDataTypeId");
            DropColumn("dbo.Notifications", "CoordinateId");
            DropTable("dbo.AdditionalDataTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AdditionalDataTypes",
                c => new
                    {
                        AdditionalDataTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AdditionalDataTypeId);
            
            AddColumn("dbo.Notifications", "CoordinateId", c => c.Int(nullable: false));
            AddColumn("dbo.AdditionalDatas", "AdditionalDataTypeId", c => c.Int(nullable: false));
            DropColumn("dbo.AdditionalDatas", "AdditionalDataType");
            CreateIndex("dbo.AdditionalDatas", "AdditionalDataTypeId");
            AddForeignKey("dbo.AdditionalDatas", "AdditionalDataTypeId", "dbo.AdditionalDataTypes", "AdditionalDataTypeId", cascadeDelete: true);
        }
    }
}
