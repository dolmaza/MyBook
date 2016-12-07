namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllowNullsInCaptionEngToDictionaryTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Dictionaries", "CaptionEng", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Dictionaries", "CaptionEng", c => c.String(nullable: false, maxLength: 200));
        }
    }
}
