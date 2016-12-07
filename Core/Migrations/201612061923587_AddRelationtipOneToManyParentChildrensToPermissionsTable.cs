namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationtipOneToManyParentChildrensToPermissionsTable : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Permissions", "ParentID");
            AddForeignKey("dbo.Permissions", "ParentID", "dbo.Permissions", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Permissions", "ParentID", "dbo.Permissions");
            DropIndex("dbo.Permissions", new[] { "ParentID" });
        }
    }
}
