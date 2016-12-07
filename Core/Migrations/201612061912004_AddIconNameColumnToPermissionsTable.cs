namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIconNameColumnToPermissionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Permissions", "IconName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Permissions", "IconName");
        }
    }
}
