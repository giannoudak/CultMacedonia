namespace CULTMACEDONIA_v2.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    /// <summary>
    /// Ενεργοποίηση των Migrations
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<CULTMACEDONIA_v2.Models.CultMacedoniaUserDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        /// Μέθοδος Αρχικοποίησης
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(CULTMACEDONIA_v2.Models.CultMacedoniaUserDbContext context)
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


            #region Δημιουργία Ρόλων

            context.Roles.AddOrUpdate(p => p.Name,
                new IdentityRole { Name = "CultMacedoniaAdmin" },
                new IdentityRole { Name = "CultMacedoniaUser" }
                );


            #endregion
        }
    }
}
