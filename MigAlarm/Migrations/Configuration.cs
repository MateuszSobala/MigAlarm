namespace MigAlarm.Migrations
{
    using Models;
    using System.Data.Entity.Migrations;

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
            context.Users.AddOrUpdate(u => u.Email,
              new User
              {
                  Forname = "John",
                  Surname = "Doe",
                  Email = "johndoe@example.com",
                  Password = "password"
              }
            );

            context.Countries.AddOrUpdate(c => c.Code,
                new Country
                {
                    Name = "Polska",
                    Code = "PL"
                }
            );
        }
    }
}
