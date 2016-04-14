namespace MigAlarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InstitutionCoordinate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Institutions", "Id", "dbo.Coordinates");
        }
        
        public override void Down()
        {
            AddForeignKey("dbo.Institutions", "Id", "dbo.Coordinates", "Id");
        }
    }
}
