namespace MigAlarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdditionalDatas",
                c => new
                    {
                        AdditionalDataId = c.Int(nullable: false, identity: true),
                        AdditionalDataTypeId = c.Int(nullable: false),
                        NotificationId = c.Int(nullable: false),
                        Text = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AdditionalDataId)
                .ForeignKey("dbo.AdditionalDataTypes", t => t.AdditionalDataTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Notifications", t => t.NotificationId, cascadeDelete: true)
                .Index(t => t.AdditionalDataTypeId)
                .Index(t => t.NotificationId);
            
            CreateTable(
                "dbo.AdditionalDataTypes",
                c => new
                    {
                        AdditionalDataTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AdditionalDataTypeId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                        CoordinateId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        DateAdded = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateAccepted = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateClosed = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coordinates", t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.EventId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Coordinates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Location = c.Geography(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Institutions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Id)
                .ForeignKey("dbo.Coordinates", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                        ZipCode = c.String(),
                        City = c.String(),
                        Street = c.String(),
                        HouseNo = c.Int(nullable: false),
                        FlatNo = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        InstitutionId = c.Int(nullable: false),
                        RoleType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId)
                .ForeignKey("dbo.Institutions", t => t.InstitutionId, cascadeDelete: true)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Forname = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Password = c.String(),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ParentEventId = c.Int(),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.Events", t => t.ParentEventId)
                .Index(t => t.ParentEventId);
            
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
                .PrimaryKey(t => t.EventNodeId)
                .ForeignKey("dbo.Events", t => t.AncestorId)
                .ForeignKey("dbo.Events", t => t.OffspringId)
                .ForeignKey("dbo.Events", t => t.Event_EventId)
                .ForeignKey("dbo.Events", t => t.Event_EventId1)
                .Index(t => t.AncestorId)
                .Index(t => t.OffspringId)
                .Index(t => t.Event_EventId)
                .Index(t => t.Event_EventId1);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        User_UserId = c.Int(nullable: false),
                        Role_RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.Role_RoleId })
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_RoleId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.Role_RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdditionalDatas", "NotificationId", "dbo.Notifications");
            DropForeignKey("dbo.Notifications", "UserId", "dbo.Users");
            DropForeignKey("dbo.Notifications", "EventId", "dbo.Events");
            DropForeignKey("dbo.EventNodes", "Event_EventId1", "dbo.Events");
            DropForeignKey("dbo.Events", "ParentEventId", "dbo.Events");
            DropForeignKey("dbo.EventNodes", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.EventNodes", "OffspringId", "dbo.Events");
            DropForeignKey("dbo.EventNodes", "AncestorId", "dbo.Events");
            DropForeignKey("dbo.Notifications", "Id", "dbo.Coordinates");
            DropForeignKey("dbo.UserRoles", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Roles", "InstitutionId", "dbo.Institutions");
            DropForeignKey("dbo.Institutions", "Id", "dbo.Coordinates");
            DropForeignKey("dbo.Institutions", "Id", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.AdditionalDatas", "AdditionalDataTypeId", "dbo.AdditionalDataTypes");
            DropIndex("dbo.UserRoles", new[] { "Role_RoleId" });
            DropIndex("dbo.UserRoles", new[] { "User_UserId" });
            DropIndex("dbo.EventNodes", new[] { "Event_EventId1" });
            DropIndex("dbo.EventNodes", new[] { "Event_EventId" });
            DropIndex("dbo.EventNodes", new[] { "OffspringId" });
            DropIndex("dbo.EventNodes", new[] { "AncestorId" });
            DropIndex("dbo.Events", new[] { "ParentEventId" });
            DropIndex("dbo.Roles", new[] { "InstitutionId" });
            DropIndex("dbo.Addresses", new[] { "CountryId" });
            DropIndex("dbo.Institutions", new[] { "Id" });
            DropIndex("dbo.Notifications", new[] { "UserId" });
            DropIndex("dbo.Notifications", new[] { "EventId" });
            DropIndex("dbo.Notifications", new[] { "Id" });
            DropIndex("dbo.AdditionalDatas", new[] { "NotificationId" });
            DropIndex("dbo.AdditionalDatas", new[] { "AdditionalDataTypeId" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.EventNodes");
            DropTable("dbo.Events");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Countries");
            DropTable("dbo.Addresses");
            DropTable("dbo.Institutions");
            DropTable("dbo.Coordinates");
            DropTable("dbo.Notifications");
            DropTable("dbo.AdditionalDataTypes");
            DropTable("dbo.AdditionalDatas");
        }
    }
}
