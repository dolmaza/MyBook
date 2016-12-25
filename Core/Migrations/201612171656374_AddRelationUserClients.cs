namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationUserClients : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.Clients", "UserID");
            AddForeignKey("dbo.Clients", "UserID", "dbo.Users", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "UserID", "dbo.Users");
            DropIndex("dbo.Clients", new[] { "UserID" });
            DropColumn("dbo.Clients", "UserID");
        }
    }
}
