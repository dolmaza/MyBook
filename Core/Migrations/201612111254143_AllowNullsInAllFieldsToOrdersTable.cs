namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllowNullsInAllFieldsToOrdersTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Firstname", c => c.String(maxLength: 200));
            AlterColumn("dbo.Orders", "Lastname", c => c.String(maxLength: 200));
            AlterColumn("dbo.Orders", "Address", c => c.String(maxLength: 200));
            AlterColumn("dbo.Orders", "Mobile", c => c.String(maxLength: 20));
            AlterColumn("dbo.Orders", "TotalPrice", c => c.Decimal(storeType: "money"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "TotalPrice", c => c.Decimal(nullable: false, storeType: "money"));
            AlterColumn("dbo.Orders", "Mobile", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Orders", "Address", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Orders", "Lastname", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Orders", "Firstname", c => c.String(nullable: false, maxLength: 200));
        }
    }
}
