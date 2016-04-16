namespace MigAlarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationInstitutiondirect_relationship : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "InstitutionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Notifications", "InstitutionId");
            AddForeignKey("dbo.Notifications", "InstitutionId", "dbo.Institutions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "InstitutionId", "dbo.Institutions");
            DropIndex("dbo.Notifications", new[] { "InstitutionId" });
            DropColumn("dbo.Notifications", "InstitutionId");
        }
    }
}
