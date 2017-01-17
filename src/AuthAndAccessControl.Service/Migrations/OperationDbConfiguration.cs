namespace AuthAndAccessControl.Service.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class OperationDbConfiguration : DbMigrationsConfiguration<IdentityServer3.EntityFramework.OperationalDbContext>
    {
        public OperationDbConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(IdentityServer3.EntityFramework.OperationalDbContext context)
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
