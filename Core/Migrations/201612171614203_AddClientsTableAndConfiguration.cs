namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClientsTableAndConfiguration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StatusID = c.Int(nullable: false),
                        Firstname = c.String(nullable: false, maxLength: 200),
                        Lastname = c.String(nullable: false, maxLength: 200),
                        Address = c.String(),
                        Mobile = c.String(nullable: false, maxLength: 20),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Dictionaries", t => t.StatusID)
                .Index(t => t.StatusID);
            
            AddColumn("dbo.Orders", "ClientID", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "ClientID");
            AddForeignKey("dbo.Orders", "ClientID", "dbo.Clients", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "StatusID", "dbo.Dictionaries");
            DropForeignKey("dbo.Orders", "ClientID", "dbo.Clients");
            DropIndex("dbo.Orders", new[] { "ClientID" });
            DropIndex("dbo.Clients", new[] { "StatusID" });
            DropColumn("dbo.Orders", "ClientID");
            DropTable("dbo.Clients");
        }
    }
}
