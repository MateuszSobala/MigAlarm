namespace MigAlarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InstitutionNameRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Institutions", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Institutions", "Name", c => c.String());
        }
    }
}
