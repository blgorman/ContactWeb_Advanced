namespace ContactWeb.Migrations.ContactWebDb
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateToAllowNonNullInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "StateId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Addresses", "StateId", c => c.Int(nullable: false));
        }
    }
}
