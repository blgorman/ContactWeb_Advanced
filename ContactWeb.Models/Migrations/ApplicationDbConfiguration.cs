namespace ContactWeb.Migrations.Identity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class ApplicationDbConfiguration : DbMigrationsConfiguration<ContactWeb.Models.ApplicationDbContext>
    {
        public ApplicationDbConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(ContactWeb.Models.ApplicationDbContext context)
        {
        }
    }
}
