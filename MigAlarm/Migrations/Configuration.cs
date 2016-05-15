using System.Collections.Generic;
using System.Data.Entity;

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
            AutomaticMigrationDataLossAllowed = true;
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
            using (var ts = context.Database.BeginTransaction())
            {
                ClearData(context);

                var defaultUser = new User
                {
                    Forname = "John",
                    Surname = "Doe",
                    Email = "johndoe@example.com",
                    Password = "password"
                };
                context.Users.AddOrUpdate(defaultUser);

                var secondUser = new User
                {
                    Forname = "Ben",
                    Surname = "Murphy",
                    Email = "benmurphy@example.com",
                    Password = "ben"
                };
                context.Users.AddOrUpdate(secondUser);

                var defaultAdmin = new User
                {
                    Forname = "Mr",
                    Surname = "Admin",
                    Email = "admin@example.com",
                    Password = "p@ssword"
                };
                context.Users.AddOrUpdate(defaultAdmin);

                var poland = new Country
                {
                    Name = "Polska",
                    Code = "PL"
                };
                context.Countries.AddOrUpdate(poland);

                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Events ON");

                var carCollision = new Event
                {
                    EventId = 1,
                    Name = "St³uczka"
                };
                var carAccidentWithInjured = new Event
                {
                    EventId = 2,
                    Name = "Wypadek z rannymi"
                };
                var hitPedestrian = new Event
                {
                    EventId = 3,
                    Name = "Potr¹cenie pieszego"
                };
                var multiVehicleAccident = new Event
                {
                    EventId = 4,
                    Name = "Kraksa wielu samochodów"
                };
                var theft = new Event
                {
                    EventId = 10,
                    Name = "Kradzie¿"
                };
                var burglar = new Event
                {
                    EventId = 11,
                    Name = "W³amanie"
                };
                var physicalViolence = new Event
                {
                    EventId = 20,
                    Name = "Przemoc fizyczna"
                };
                var psychologicalViolence = new Event
                {
                    EventId = 21,
                    Name = "Przemoc psychiczna"
                };
                var sexualViolence = new Event
                {
                    EventId = 22,
                    Name = "Przemoc seksualna"
                };
                var criminalDamage = new Event
                {
                    EventId = 23,
                    Name = "Niszczenie mienia"
                };
                var breachOfTheDutyToCare = new Event
                {
                    EventId = 24,
                    Name = "Naruszenie obowi¹zku do opieki"
                };
                var murder = new Event
                {
                    EventId = 90,
                    Name = "Zabójstwo"
                };
                var rape = new Event
                {
                    EventId = 91,
                    Name = "Gwa³t"
                };
                var assaultWithWeapon = new Event
                {
                    EventId = 92,
                    Name = "Napaœæ z broni¹"
                };
                var otherPolice = new Event
                {
                    EventId = 99,
                    Name = "Policja"
                };
                var faint = new Event
                {
                    EventId = 101,
                    Name = "Omdlenie"
                };
                var lossOfVitalFunctions = new Event
                {
                    EventId = 102,
                    Name = "Utrata funkcji ¿yciowych"
                };
                var seriousRoadAccident = new Event
                {
                    EventId = 103,
                    Name = "Powa¿ny wypadek drogowy"
                };
                var severeInjury = new Event
                {
                    EventId = 104,
                    Name = "Ciê¿kie zranienie"
                };
                var severeBurns = new Event
                {
                    EventId = 105,
                    Name = "Ciê¿kie oparzenia"
                };
                var drowningOrElectricShock = new Event
                {
                    EventId = 106,
                    Name = "Utoniêcie lub pora¿enie pr¹dem"
                };
                var painInChest = new Event
                {
                    EventId = 110,
                    Name = "Ból w klatce piersiowej"
                };
                var headache = new Event
                {
                    EventId = 111,
                    Name = "Ból g³owy"
                };
                var stomachAche = new Event
                {
                    EventId = 112,
                    Name = "Ból brzucha"
                };
                var limbPain = new Event
                {
                    EventId = 113,
                    Name = "Ból koñczyny"
                };
                var backPain = new Event
                {
                    EventId = 114,
                    Name = "Ból pleców"
                };
                var otherPain = new Event
                {
                    EventId = 115,
                    Name = "Inny ból"
                };
                var childbirth = new Event
                {
                    EventId = 190,
                    Name = "Poród"
                };
                var otherEmergency = new Event
                {
                    EventId = 199,
                    Name = "Pogotowie ratunkowe"
                };
                var smallFire = new Event
                {
                    EventId = 201,
                    Name = "Ma³y ogieñ"
                };
                var largeFire = new Event
                {
                    EventId = 202,
                    Name = "Du¿y ogieñ"
                };
                var conflagration = new Event
                {
                    EventId = 203,
                    Name = "Po¿oga"
                };
                var carAccident = new Event
                {
                    EventId = 210,
                    Name = "Wypadek drogowy"
                };
                var multiVehicleAccidentFireBrigade = new Event
                {
                    EventId = 211,
                    Name = "Kraksa wielu samochodów"
                };
                var accidentInHome = new Event
                {
                    EventId = 212,
                    Name = "Wypadek w domu"
                };
                var animalInNeed = new Event
                {
                    EventId = 290,
                    Name = "Zwierzê w potrzebie"
                };
                var otherFireBrigade = new Event
                {
                    EventId = 299,
                    Name = "Stra¿ po¿arna"
                };
                var other = new Event
                {
                    EventId = 300,
                    Name = "Inne"
                };
                context.Events.AddOrUpdate(carCollision, carAccidentWithInjured, hitPedestrian, multiVehicleAccident, theft, burglar,
                    physicalViolence, psychologicalViolence, sexualViolence, criminalDamage, breachOfTheDutyToCare, murder,
                    rape, assaultWithWeapon, otherPolice, faint, lossOfVitalFunctions, seriousRoadAccident, severeInjury,
                    severeBurns, drowningOrElectricShock, painInChest, headache, stomachAche, limbPain, backPain, otherPain,
                    childbirth, otherEmergency, smallFire, largeFire, conflagration, carAccident, multiVehicleAccidentFireBrigade,
                    accidentInHome, animalInNeed, otherFireBrigade, other);

                var centrumZarzadzaniaKryzysowegoWroclawCoordinates = new Coordinate
                {
                    Location = GeoUtils.CreatePoint(51.114265, 16.972300)
                };
                context.Coordinates.AddOrUpdate(centrumZarzadzaniaKryzysowegoWroclawCoordinates);

                var centrumZarzadzaniaKryzysowegoWroclawAddress = new Address
                {
                    Country = poland,
                    ZipCode = "50-153",
                    City = "Wroclaw",
                    Street = "Pl. Powstancow Warszawy",
                    HouseNo = 1,
                    Coordinate = centrumZarzadzaniaKryzysowegoWroclawCoordinates
                };
                context.Addresses.AddOrUpdate(centrumZarzadzaniaKryzysowegoWroclawAddress);

                var centrumZarzadzaniaKryzysowegoWroclaw = new Institution
                {
                    Name = "Dolnoslaskie Centrum Zarzadzania Kryzysowego",
                    Address = centrumZarzadzaniaKryzysowegoWroclawAddress
                };
                context.Institutions.AddOrUpdate(centrumZarzadzaniaKryzysowegoWroclaw);

                var centrumZarzadzaniaKryzysowegoLodzCoordinates = new Coordinate
                {
                    Location = GeoUtils.CreatePoint(51.765161, 19.457474)
                };
                context.Coordinates.AddOrUpdate(centrumZarzadzaniaKryzysowegoLodzCoordinates);

                var centrumZarzadzaniaKryzysowegoLodzAddress = new Address
                {
                    Country = poland,
                    ZipCode = "90-004",
                    City = "Lodz",
                    Street = "Piotrkowska",
                    HouseNo = 104,
                    Coordinate = centrumZarzadzaniaKryzysowegoLodzCoordinates
                };
                context.Addresses.AddOrUpdate(centrumZarzadzaniaKryzysowegoLodzAddress);

                var centrumZarzadzaniaKryzysowegoLodz = new Institution
                {
                    Name = "Wojewodzkie Centrum Zarzadzania Kryzysowego w Lodzi",
                    Address = centrumZarzadzaniaKryzysowegoLodzAddress
                };
                context.Institutions.AddOrUpdate(centrumZarzadzaniaKryzysowegoLodz);

                var defaultUserRole = new Role
                {
                    Institution = centrumZarzadzaniaKryzysowegoWroclaw,
                    RoleType = RoleType.User
                };
                defaultUserRole.Users.Add(defaultUser);
                context.Roles.AddOrUpdate(defaultUserRole);

                var secondUserRole = new Role
                {
                    Institution = centrumZarzadzaniaKryzysowegoWroclaw,
                    RoleType = RoleType.User
                };
                context.Roles.AddOrUpdate(secondUserRole);

                var defaultAdminRole = new Role
                {
                    Institution = centrumZarzadzaniaKryzysowegoWroclaw,
                    RoleType = RoleType.Admin
                };
                defaultAdminRole.Users.Add(defaultAdmin);
                context.Roles.AddOrUpdate(defaultAdminRole);

                var exampleNotification = new Notification
                {
                    Event = carCollision,
                    EventId = 1,
                    Coordinate = centrumZarzadzaniaKryzysowegoWroclawCoordinates,
                    Institution = centrumZarzadzaniaKryzysowegoWroclaw
                };
                context.Notifications.Add(exampleNotification);

                var exmapleAdditionalData = new List<AdditionalData>();

                var exampleUserName = new AdditionalData
                {
                    AdditionalDataType = AdditionalDataType.Name,
                    Text = "Jan Kowalski",
                    Notification = exampleNotification
                };
                exmapleAdditionalData.Add(exampleUserName);

                var examplePhoneNumber = new AdditionalData
                {
                    AdditionalDataType = AdditionalDataType.PhoneNumber,
                    Text = "123456789",
                    Notification = exampleNotification
                };
                exmapleAdditionalData.Add(examplePhoneNumber);

                var exampleUserYearOfBirth = new AdditionalData
                {
                    AdditionalDataType = AdditionalDataType.Birthday,
                    Text = "1992",
                    Notification = exampleNotification
                };
                exmapleAdditionalData.Add(exampleUserYearOfBirth);

                var exampleUserAddress = new AdditionalData
                {
                    AdditionalDataType = AdditionalDataType.HomeAddress,
                    Text = "Ul. Œmieszna 4, Wroclaw",
                    Notification = exampleNotification
                };
                exmapleAdditionalData.Add(exampleUserAddress);

                var exampleUserDiseases = new AdditionalData
                {
                    AdditionalDataType = AdditionalDataType.Diseases,
                    Text = "Alergia na trawy",
                    Notification = exampleNotification
                };
                exmapleAdditionalData.Add(exampleUserDiseases);

                var exampleUserMedicines = new AdditionalData
                {
                    AdditionalDataType = AdditionalDataType.Medicines,
                    Text = "Claritine Allergy",
                    Notification = exampleNotification
                };
                exmapleAdditionalData.Add(exampleUserMedicines);

                var exampleUserAppearance = new AdditionalData
                {
                    AdditionalDataType = AdditionalDataType.Appearance,
                    Text = "Wysoki blondyn",
                    Notification = exampleNotification
                };
                exmapleAdditionalData.Add(exampleUserAppearance);

                var exampleUserBloodGroup = new AdditionalData
                {
                    AdditionalDataType = AdditionalDataType.BloodGroup,
                    Text = "0",
                    Notification = exampleNotification
                };
                exmapleAdditionalData.Add(exampleUserBloodGroup);

                var exampleLocalization = new AdditionalData
                {
                    AdditionalDataType = AdditionalDataType.Localization,
                    Text = "Micha³a Wroc³awczyka 10, 50-380 Wroc³aw",
                    Notification = exampleNotification
                };
                exmapleAdditionalData.Add(exampleLocalization);

                var exampleOther = new AdditionalData
                {
                    AdditionalDataType = AdditionalDataType.Other,
                    Text = "W domu groŸny pies",
                    Notification = exampleNotification
                };
                exmapleAdditionalData.Add(exampleOther);

                context.AdditionalData.AddRange(exmapleAdditionalData);

                context.SaveChanges();

                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Events OFF");

                ts.Commit();
            }
        }

        private void ClearData(DbContext context)
        {
            context.Database.ExecuteSqlCommand("DELETE UserRoles");
            context.Database.ExecuteSqlCommand("DELETE Roles");
            context.Database.ExecuteSqlCommand("DELETE Institutions");
            context.Database.ExecuteSqlCommand("DELETE Addresses");
            context.Database.ExecuteSqlCommand("DELETE Countries");
            context.Database.ExecuteSqlCommand("DELETE AdditionalDatas");
            context.Database.ExecuteSqlCommand("DELETE Notifications");
            context.Database.ExecuteSqlCommand("DELETE Coordinates");
            context.Database.ExecuteSqlCommand("DELETE Events");
            context.Database.ExecuteSqlCommand("DELETE Users");
            context.SaveChanges();
        }
    }
}
