namespace MigAlarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressCoordinate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Institutions", "Id", "dbo.Addresses");
            DropPrimaryKey("dbo.Addresses");
            AlterColumn("dbo.Addresses", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Addresses", "Id");
            CreateIndex("dbo.Addresses", "Id");
            AddForeignKey("dbo.Addresses", "Id", "dbo.Coordinates", "Id");
            AddForeignKey("dbo.Institutions", "Id", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Institutions", "Id", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "Id", "dbo.Coordinates");
            DropIndex("dbo.Addresses", new[] { "Id" });
            DropPrimaryKey("dbo.Addresses");
            AlterColumn("dbo.Addresses", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Addresses", "Id");
            AddForeignKey("dbo.Institutions", "Id", "dbo.Addresses", "Id");
        }
    }
}
