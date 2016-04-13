using MigAlarm.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MigAlarm.Models
{
    public class MigAlarmInitializer : CreateDatabaseIfNotExists<MigAlarmContext>
    {
        protected override void Seed(MigAlarmContext context)
        {
            var defaultUser = new User
            {
                Forname = "John",
                Surname = "Doe",
                Email = "johndoe@example.com",
                Password = "password"
            };
            context.Users.Add(defaultUser);

            var defaultAdmin = new User
            {
                Forname = "Mr",
                Surname = "Admin",
                Email = "admin@example.com",
                Password = "p@ssword"
            };
            context.Users.Add(defaultAdmin);

            var poland = new Country
            {
                Name = "Polska",
                Code = "PL"
            };
            context.Countries.Add(poland);

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
            context.Events.AddRange(new List<Event>() { police, fireDepartment, emergencyService, robbery, carAccident });

            var centrumZarzadzaniaKryzysowegoWroclawCoordinates = new Coordinate
            {
                Location = GeoUtils.CreatePoint(51.114265, 16.972300),
            };
            context.Coordinates.Add(centrumZarzadzaniaKryzysowegoWroclawCoordinates);

            var centrumZarzadzaniaKryzysowegoWroclawAddress = new Address
            {
                Country = poland,
                ZipCode = "50-153",
                City = "Wroclaw",
                Street = "Pl. Powstancow Warszawy",
                HouseNo = 1
            };
            context.Addresses.Add(centrumZarzadzaniaKryzysowegoWroclawAddress);

            var centrumZarzadzaniaKryzysowegoWroclaw = new Institution
            {
                Name = "Dolnoslaskie Centrum Zarzadzania Kryzysowego",
                Coordinate = centrumZarzadzaniaKryzysowegoWroclawCoordinates,
                Address = centrumZarzadzaniaKryzysowegoWroclawAddress
            };
            context.Institutions.Add(centrumZarzadzaniaKryzysowegoWroclaw);

            var centrumZarzadzaniaKryzysowegoLodzCoordinates = new Coordinate
            {
                Location = GeoUtils.CreatePoint(51.765161, 19.457474),
            };
            context.Coordinates.Add(centrumZarzadzaniaKryzysowegoLodzCoordinates);

            var centrumZarzadzaniaKryzysowegoLodzAddress = new Address
            {
                Country = poland,
                ZipCode = "90-004",
                City = "Lodz",
                Street = "Piotrkowska",
                HouseNo = 104
            };
            context.Addresses.Add(centrumZarzadzaniaKryzysowegoLodzAddress);

            var centrumZarzadzaniaKryzysowegoLodz = new Institution
            {
                Name = "Wojewodzkie Centrum Zarzadzania Kryzysowego w Lodzi",
                Coordinate = centrumZarzadzaniaKryzysowegoLodzCoordinates,
                Address = centrumZarzadzaniaKryzysowegoLodzAddress
            };
            context.Institutions.Add(centrumZarzadzaniaKryzysowegoLodz);

            var defaultUserRole = new Role
            {
                Institution = centrumZarzadzaniaKryzysowegoWroclaw,
                RoleType = RoleType.User
            };
            defaultUserRole.Users.Add(defaultUser);
            context.Roles.Add(defaultUserRole);

            var defaultAdminRole = new Role
            {
                Institution = centrumZarzadzaniaKryzysowegoWroclaw,
                RoleType = RoleType.Admin
            };
            defaultAdminRole.Users.Add(defaultAdmin);
            context.Roles.Add(defaultAdminRole);
            context.SaveChanges();
        }
    }
}