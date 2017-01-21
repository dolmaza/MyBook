namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsArchivedColumnToOrdersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsArchived", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "IsArchived");
        }
    }
}
