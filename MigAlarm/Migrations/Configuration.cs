namespace MigAlarm.Migrations
{
    using Models;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Spatial;
    using Utils;
    internal sealed class Configuration : DbMigrationsConfiguration<MigAlarmContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MigAlarmContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var defaultUser = new User
            {
                Forname = "John",
                Surname = "Doe",
                Email = "johndoe@example.com",
                Password = "password"
            };
            context.Users.AddOrUpdate(u => u.Email, defaultUser);

            var poland = new Country
            {
                Name = "Polska",
                Code = "PL"
            };
            context.Countries.AddOrUpdate(c => c.Code, poland);

            var police = new Event
            {
                Name = "Police"
            };
            var fireDepartment = new Event
            {
                Name = "FireDeparment"
            };
            var emergencyService = new Event
            {
                Name = "EmergencyService"
            };
            var robbery = new Event
            {
                Name = "Robbery",
                ParentEvent = police
            };
            var carAccident = new Event
            {
                Name = "CarAccident",
                ParentEvent = police
            };
            context.Events.AddOrUpdate(ev => ev.Name,
                police, fireDepartment, emergencyService, robbery, carAccident);

            var centrumZarzadzaniaKryzysowegoWroclawCoordinates = new Coordinate
            {
                Location = DbGeography.FromText("POINT(51.114265 16.972300)", 4326),
            };
            context.Coordinates.AddOrUpdate(centrumZarzadzaniaKryzysowegoWroclawCoordinates);

            var centrumZarzadzaniaKryzysowegoWroclawAddress = new Address
            {
                Country = poland,
                ZipCode = "50-153",
                City = "Wroc³aw",
                Street = "Pl. Powstañców Warszawy",
                HouseNo = 1
            };
            context.Addresses.AddOrUpdate(centrumZarzadzaniaKryzysowegoWroclawAddress);

            var centrumZarzadzaniaKryzysowegoWroclaw = new Institution
            {
                Name = "Dolnoœl¹skie Centrum Zarz¹dzania Kryzysowego",
                Coordinate = centrumZarzadzaniaKryzysowegoWroclawCoordinates,
                Address = centrumZarzadzaniaKryzysowegoWroclawAddress
            };
            context.Institutions.AddOrUpdate(i => i.Name, centrumZarzadzaniaKryzysowegoWroclaw);

            var defaultRole = new Role
            {
                Institution = centrumZarzadzaniaKryzysowegoWroclaw,
                RoleType = RoleType.USER
            };
            defaultRole.Users.Add(defaultUser);
            context.Roles.AddOrUpdate(defaultRole);

        }
    }
}
