namespace ContactWeb.Migrations.ContactWebDb
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateContactAndAddAddressEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactId = c.Int(nullable: false),
                        Email = c.String(maxLength: 255),
                        PhonePrimary = c.String(maxLength: 12),
                        PhoneSecondary = c.String(maxLength: 12),
                        StreetAddress1 = c.String(maxLength: 255),
                        StreetAddress2 = c.String(maxLength: 255),
                        City = c.String(maxLength: 50),
                        StateId = c.Int(nullable: true),
                        Zip = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ContactId);
            
            AlterColumn("dbo.Contacts", "FirstName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Contacts", "LastName", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Contacts", "Email");
            DropColumn("dbo.Contacts", "PhonePrimary");
            DropColumn("dbo.Contacts", "PhoneSecondary");
            DropColumn("dbo.Contacts", "StreetAddress1");
            DropColumn("dbo.Contacts", "StreetAddress2");
            DropColumn("dbo.Contacts", "City");
            DropColumn("dbo.Contacts", "State");
            DropColumn("dbo.Contacts", "Zip");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contacts", "Zip", c => c.String());
            AddColumn("dbo.Contacts", "State", c => c.String());
            AddColumn("dbo.Contacts", "City", c => c.String());
            AddColumn("dbo.Contacts", "StreetAddress2", c => c.String());
            AddColumn("dbo.Contacts", "StreetAddress1", c => c.String());
            AddColumn("dbo.Contacts", "PhoneSecondary", c => c.String());
            AddColumn("dbo.Contacts", "PhonePrimary", c => c.String());
            AddColumn("dbo.Contacts", "Email", c => c.String());
            DropForeignKey("dbo.Addresses", "ContactId", "dbo.Contacts");
            DropIndex("dbo.Addresses", new[] { "ContactId" });
            AlterColumn("dbo.Contacts", "LastName", c => c.String());
            AlterColumn("dbo.Contacts", "FirstName", c => c.String());
            DropTable("dbo.Addresses");
        }
    }
}
