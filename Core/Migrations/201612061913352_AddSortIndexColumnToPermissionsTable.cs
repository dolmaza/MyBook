namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSortIndexColumnToPermissionsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Permissions", "SortIndex", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Permissions", "SortIndex");
        }
    }
}
