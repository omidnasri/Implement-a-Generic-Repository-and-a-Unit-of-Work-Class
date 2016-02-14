namespace Karin.DataAccess.Context
{
    using System.Data.Entity;
    using Karin.DataAccess.Interface;
    using Karin.DataAccess.Migrations;
    public class KarinContext : BaseContext, IKarinUnitOfWork
    {
        /// <summary>
        /// 
        /// </summary>
        public KarinContext() : base("KarinConnectionString")
        {
        }
        /// <summary>
        /// 
        /// </summary>
        static KarinContext()
        {
            Database.SetInitializer(strategy: new MigrateDatabaseToLatestVersion<KarinContext, Configuration>());
        }
        /// <summary>
        /// اشخاص
        /// </summary>
        public DbSet<Domain.Tables.Person> Persons { get; set; }
        /// <summary>
        /// موبایل
        /// </summary>
        public DbSet<Domain.Tables.Mobile> Mobiles { get; set; }
    }
}
