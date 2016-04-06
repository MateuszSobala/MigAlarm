namespace MigAlarm.Migrations
{
    using Models;
    using System.Data.Entity.Migrations;
    using Utils;
    internal sealed class Configuration : DbMigrationsConfiguration<MigAlarmContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            //AutomaticMigrationDataLossAllowed = true;
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

            var defaultAdmin = new User
            {
                Forname = "Mr",
                Surname = "Admin",
                Email = "admin@example.com",
                Password = "p@ssword"
            };
            context.Users.AddOrUpdate(u => u.Email, defaultAdmin);

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
                Location = GeoUtils.CreatePoint(51.114265, 16.972300),
            };
            context.Coordinates.AddOrUpdate(centrumZarzadzaniaKryzysowegoWroclawCoordinates);

            var centrumZarzadzaniaKryzysowegoWroclawAddress = new Address
            {
                Country = poland,
                ZipCode = "50-153",
                City = "Wroclaw",
                Street = "Pl. Powstancow Warszawy",
                HouseNo = 1
            };
            context.Addresses.AddOrUpdate(centrumZarzadzaniaKryzysowegoWroclawAddress);

            var centrumZarzadzaniaKryzysowegoWroclaw = new Institution
            {
                Name = "Dolnoslaskie Centrum Zarzadzania Kryzysowego",
                Coordinate = centrumZarzadzaniaKryzysowegoWroclawCoordinates,
                Address = centrumZarzadzaniaKryzysowegoWroclawAddress
            };
            context.Institutions.AddOrUpdate(i => i.Name, centrumZarzadzaniaKryzysowegoWroclaw);

            var centrumZarzadzaniaKryzysowegoLodzCoordinates = new Coordinate
            {
                Location = GeoUtils.CreatePoint(51.765161, 19.457474),
            };
            context.Coordinates.AddOrUpdate(centrumZarzadzaniaKryzysowegoLodzCoordinates);

            var centrumZarzadzaniaKryzysowegoLodzAddress = new Address
            {
                Country = poland,
                ZipCode = "90-004",
                City = "Lodz",
                Street = "Piotrkowska",
                HouseNo = 104
            };
            context.Addresses.AddOrUpdate(centrumZarzadzaniaKryzysowegoLodzAddress);

            var centrumZarzadzaniaKryzysowegoLodz = new Institution
            {
                Name = "Wojewodzkie Centrum Zarzadzania Kryzysowego w Lodzi",
                Coordinate = centrumZarzadzaniaKryzysowegoLodzCoordinates,
                Address = centrumZarzadzaniaKryzysowegoLodzAddress
            };
            context.Institutions.AddOrUpdate(i => i.Name, centrumZarzadzaniaKryzysowegoLodz);
            
            var defaultUserRole = new Role
            {
                Institution = centrumZarzadzaniaKryzysowegoWroclaw,
                RoleType = RoleType.User
            };
            defaultUserRole.Users.Add(defaultUser);
            context.Roles.AddOrUpdate(defaultUserRole);

            var defaultAdminRole = new Role
            {
                Institution = centrumZarzadzaniaKryzysowegoWroclaw,
                RoleType = RoleType.Admin
            };
            defaultAdminRole.Users.Add(defaultAdmin);
            context.Roles.AddOrUpdate(defaultAdminRole);
        }
    }
}
