namespace AuthAndAccessControl.Service.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class MembershipRebootConfiguration : DbMigrationsConfiguration<AuthAndAccessControl.Service.MembershipReboot.IACCDatabase>
    {
        public MembershipRebootConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AuthAndAccessControl.Service.MembershipReboot.IACCDatabase context)
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
        }
    }
}
