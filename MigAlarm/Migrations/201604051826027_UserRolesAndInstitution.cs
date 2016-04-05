namespace MigAlarm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRolesAndInstitution : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        InstitutionId = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                        City = c.String(),
                        Street = c.String(),
                        HouseNo = c.Int(nullable: false),
                        FlatNo = c.Int(),
                    })
                .PrimaryKey(t => t.AddressId)
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
                "dbo.Institutions",
                c => new
                    {
                        InstitutionId = c.Int(nullable: false),
                        Name = c.String(),
                        AddressId = c.Int(nullable: false),
                        CoordinateId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InstitutionId)
                .ForeignKey("dbo.Addresses", t => t.InstitutionId)
                .ForeignKey("dbo.Coordinates", t => t.InstitutionId)
                .Index(t => t.InstitutionId);
            
            CreateTable(
                "dbo.Coordinates",
                c => new
                    {
                        CoordinateId = c.Int(nullable: false, identity: true),
                        Location = c.Geography(),
                        InstitutionId = c.Int(),
                    })
                .PrimaryKey(t => t.CoordinateId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        RoleTypeId = c.Int(nullable: false),
                        RoleType_RoleTypeId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.RoleId)
                .ForeignKey("dbo.Institutions", t => t.RoleId)
                .ForeignKey("dbo.RoleTypes", t => t.RoleType_RoleTypeId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.RoleType_RoleTypeId);
            
            CreateTable(
                "dbo.RoleTypes",
                c => new
                    {
                        RoleTypeId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.RoleTypeId);
            
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
            
            AlterColumn("dbo.Users", "Forname", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Surname", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Roles", "RoleType_RoleTypeId", "dbo.RoleTypes");
            DropForeignKey("dbo.Roles", "RoleId", "dbo.Institutions");
            DropForeignKey("dbo.Institutions", "InstitutionId", "dbo.Coordinates");
            DropForeignKey("dbo.Institutions", "InstitutionId", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "CountryId", "dbo.Countries");
            DropIndex("dbo.UserRoles", new[] { "Role_RoleId" });
            DropIndex("dbo.UserRoles", new[] { "User_UserId" });
            DropIndex("dbo.Roles", new[] { "RoleType_RoleTypeId" });
            DropIndex("dbo.Roles", new[] { "RoleId" });
            DropIndex("dbo.Institutions", new[] { "InstitutionId" });
            DropIndex("dbo.Addresses", new[] { "CountryId" });
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Surname", c => c.String());
            AlterColumn("dbo.Users", "Forname", c => c.String());
            DropTable("dbo.UserRoles");
            DropTable("dbo.RoleTypes");
            DropTable("dbo.Roles");
            DropTable("dbo.Coordinates");
            DropTable("dbo.Institutions");
            DropTable("dbo.Countries");
            DropTable("dbo.Addresses");
        }
    }
}
