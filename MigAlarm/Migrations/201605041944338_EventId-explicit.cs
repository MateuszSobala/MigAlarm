namespace MigAlarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventIdexplicit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventNodes", "AncestorId", "dbo.Events");
            DropForeignKey("dbo.EventNodes", "OffspringId", "dbo.Events");
            DropForeignKey("dbo.EventNodes", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.Events", "ParentEventId", "dbo.Events");
            DropForeignKey("dbo.EventNodes", "Event_EventId1", "dbo.Events");
            DropForeignKey("dbo.Notifications", "EventId", "dbo.Events");
            DropPrimaryKey("dbo.Events");
            AlterColumn("dbo.Events", "EventId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Events", "EventId");
            AddForeignKey("dbo.EventNodes", "AncestorId", "dbo.Events", "EventId");
            AddForeignKey("dbo.EventNodes", "OffspringId", "dbo.Events", "EventId");
            AddForeignKey("dbo.EventNodes", "Event_EventId", "dbo.Events", "EventId");
            AddForeignKey("dbo.Events", "ParentEventId", "dbo.Events", "EventId");
            AddForeignKey("dbo.EventNodes", "Event_EventId1", "dbo.Events", "EventId");
            AddForeignKey("dbo.Notifications", "EventId", "dbo.Events", "EventId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "EventId", "dbo.Events");
            DropForeignKey("dbo.EventNodes", "Event_EventId1", "dbo.Events");
            DropForeignKey("dbo.Events", "ParentEventId", "dbo.Events");
            DropForeignKey("dbo.EventNodes", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.EventNodes", "OffspringId", "dbo.Events");
            DropForeignKey("dbo.EventNodes", "AncestorId", "dbo.Events");
            DropPrimaryKey("dbo.Events");
            AlterColumn("dbo.Events", "EventId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Events", "EventId");
            AddForeignKey("dbo.Notifications", "EventId", "dbo.Events", "EventId", cascadeDelete: true);
            AddForeignKey("dbo.EventNodes", "Event_EventId1", "dbo.Events", "EventId");
            AddForeignKey("dbo.Events", "ParentEventId", "dbo.Events", "EventId");
            AddForeignKey("dbo.EventNodes", "Event_EventId", "dbo.Events", "EventId");
            AddForeignKey("dbo.EventNodes", "OffspringId", "dbo.Events", "EventId");
            AddForeignKey("dbo.EventNodes", "AncestorId", "dbo.Events", "EventId");
        }
    }
}
