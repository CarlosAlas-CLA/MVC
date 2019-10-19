namespace MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.ContactID);
            
            CreateTable(
                "dbo.EmailAddresses",
                c => new
                    {
                        EmailID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        EmailType = c.Int(nullable: false),
                        ContactID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmailID)
                .ForeignKey("dbo.Contacts", t => t.ContactID, cascadeDelete: true)
                .Index(t => t.ContactID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmailAddresses", "ContactID", "dbo.Contacts");
            DropIndex("dbo.EmailAddresses", new[] { "ContactID" });
            DropTable("dbo.EmailAddresses");
            DropTable("dbo.Contacts");
        }
    }
}
