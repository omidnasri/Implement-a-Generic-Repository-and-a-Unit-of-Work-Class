namespace Karin.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;
    /// <summary>
    /// 
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<Karin.DataAccess.Context.KarinContext>
    {
        /// <summary>
        /// 
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(Karin.DataAccess.Context.KarinContext context)
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
            base.Seed(context);
        }
    }
}
