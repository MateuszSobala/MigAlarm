namespace MigAlarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveEventNode : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventNodes", "AncestorId", "dbo.Events");
            DropForeignKey("dbo.EventNodes", "OffspringId", "dbo.Events");
            DropForeignKey("dbo.EventNodes", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.EventNodes", "Event_EventId1", "dbo.Events");
            DropIndex("dbo.EventNodes", new[] { "AncestorId" });
            DropIndex("dbo.EventNodes", new[] { "OffspringId" });
            DropIndex("dbo.EventNodes", new[] { "Event_EventId" });
            DropIndex("dbo.EventNodes", new[] { "Event_EventId1" });
            DropTable("dbo.EventNodes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EventNodes",
                c => new
                    {
                        EventNodeId = c.Int(nullable: false, identity: true),
                        AncestorId = c.Int(),
                        OffspringId = c.Int(),
                        Event_EventId = c.Int(),
                        Event_EventId1 = c.Int(),
                    })
                .PrimaryKey(t => t.EventNodeId);
            
            CreateIndex("dbo.EventNodes", "Event_EventId1");
            CreateIndex("dbo.EventNodes", "Event_EventId");
            CreateIndex("dbo.EventNodes", "OffspringId");
            CreateIndex("dbo.EventNodes", "AncestorId");
            AddForeignKey("dbo.EventNodes", "Event_EventId1", "dbo.Events", "EventId");
            AddForeignKey("dbo.EventNodes", "Event_EventId", "dbo.Events", "EventId");
            AddForeignKey("dbo.EventNodes", "OffspringId", "dbo.Events", "EventId");
            AddForeignKey("dbo.EventNodes", "AncestorId", "dbo.Events", "EventId");
        }
    }
}
