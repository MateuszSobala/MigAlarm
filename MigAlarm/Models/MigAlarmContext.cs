using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Text;

namespace MigAlarm.Models
{
    public class MigAlarmContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MigAlarmContext() : base("name=MigAlarm")
        {
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Coordinate> Coordinates { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<EventNode> EventNodes { get; set; }
        public DbSet<AdditionalData> AdditionalData { get; set; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb, ex
                    ); // Add the original exception as the innerException
            }
        }
    }
}
