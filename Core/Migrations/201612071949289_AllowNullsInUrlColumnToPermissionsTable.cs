namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllowNullsInUrlColumnToPermissionsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Permissions", "Url", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Permissions", "Url", c => c.String(nullable: false, maxLength: 200));
        }
    }
}
