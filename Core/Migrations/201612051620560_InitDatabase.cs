namespace Core.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dictionaries",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    ParentID = c.Int(),
                    Caption = c.String(nullable: false, maxLength: 200),
                    CaptionEng = c.String(nullable: false, maxLength: 200),
                    StringCode = c.String(maxLength: 200),
                    IntCode = c.Int(),
                    DecimalValue = c.Decimal(storeType: "money"),
                    Level = c.Int(),
                    Hierarchy = c.String(),
                    Code = c.Int(nullable: false),
                    SortIndex = c.Int(),
                    CreateTime = c.DateTime(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Dictionaries", t => t.ParentID)
                .Index(t => t.ParentID);

            Sql("CREATE  TRIGGER [dbo].[OnDictionariesHierarchyCalc] ON [dbo].[Dictionaries] FOR INSERT, UPDATE AS DECLARE @DEL bit = 0 DECLARE @INS bit  = 0 DECLARE @numrows int = @@rowcount DECLARE @Delimiter varchar(1) = '.' IF EXISTS (SELECT TOP 1 1 FROM DELETED) SET @DEL=1 IF EXISTS (SELECT TOP 1 1 FROM INSERTED) SET @INS = 1  IF @INS = 0 AND @DEL = 0 RETURN IF @INS = 1 AND @DEL = 0 BEGIN IF @numrows > 1 BEGIN RAISERROR('Only single row inserts are supported!', 16, 1) ROLLBACK TRAN RETURN END ELSE BEGIN UPDATE D SET D.[Level] = CASE WHEN D.ParentID IS NULL THEN 0 ELSE D1.[Level] + 1 END, D.Hierarchy = CASE WHEN D.ParentID IS NULL THEN @Delimiter ELSE D1.Hierarchy END + CAST(D.ID AS varchar(10)) + @Delimiter FROM Dictionaries D INNER JOIN inserted I ON I.ID = D.ID LEFT JOIN Dictionaries D1 ON D.ParentID = D1.ID END END ELSE BEGIN IF UPDATE(ID) BEGIN RAISERROR('Updates to codeid not allowed!', 16, 1) ROLLBACK TRAN RETURN END ELSE BEGIN IF UPDATE(ParentID) or UPDATE(StringCode) BEGIN UPDATE D SET D.[Level] = D.[Level] - I.[Level] + CASE WHEN I.ParentID IS NULL THEN 0 ELSE D1.[Level] + 1 END, D.Hierarchy = ISNULL(D1.Hierarchy, '.') + CAST(I.ID AS varchar(10)) + '.' + ISNULL(RIGHT(D.Hierarchy, LEN(D.Hierarchy) - LEN(I.Hierarchy)), '') FROM Dictionaries D INNER JOIN INSERTED I ON D.Hierarchy LIKE I.Hierarchy + '%' LEFT JOIN Dictionaries D1 ON I.ParentID = D1.ID END END END");


            CreateTable(
                "dbo.Orders",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    UserID = c.Int(nullable: false),
                    StatusID = c.Int(nullable: false),
                    Firstname = c.String(nullable: false, maxLength: 200),
                    Lastname = c.String(nullable: false, maxLength: 200),
                    Address = c.String(nullable: false, maxLength: 200),
                    Mobile = c.String(nullable: false, maxLength: 20),
                    TotalPrice = c.Decimal(nullable: false, storeType: "money"),
                    DeliveryTime = c.DateTime(),
                    Note = c.String(),
                    CreateTime = c.DateTime(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Dictionaries", t => t.StatusID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.StatusID);

            CreateTable(
                "dbo.OrderDetails",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    OrderID = c.Int(nullable: false),
                    BookName = c.String(nullable: false, maxLength: 200),
                    Price = c.Decimal(nullable: false, storeType: "money"),
                    CreateTime = c.DateTime(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    RoleID = c.Int(nullable: false),
                    Username = c.String(nullable: false, maxLength: 200),
                    Password = c.String(nullable: false, maxLength: 200),
                    Firstname = c.String(maxLength: 200),
                    Lastname = c.String(maxLength: 200),
                    IsActive = c.Boolean(nullable: false),
                    CreateTime = c.DateTime(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Roles", t => t.RoleID)
                .Index(t => t.RoleID)
                .Index(t => t.Username, unique: true, name: "IX_UsernameUnique");

            CreateTable(
                "dbo.Roles",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Caption = c.String(nullable: false, maxLength: 200),
                    Code = c.Int(),
                    CreateTime = c.DateTime(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Permissions",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    ParentID = c.Int(),
                    Caption = c.String(nullable: false, maxLength: 200),
                    Url = c.String(nullable: false, maxLength: 200),
                    IsMenuItem = c.Boolean(nullable: false),
                    Code = c.String(nullable: false, maxLength: 50),
                    CreateTime = c.DateTime(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.RolePermissions",
                c => new
                {
                    RoleID = c.Int(nullable: false),
                    PermissionID = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.RoleID, t.PermissionID })
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .ForeignKey("dbo.Permissions", t => t.PermissionID, cascadeDelete: true)
                .Index(t => t.RoleID)
                .Index(t => t.PermissionID);

        }

        public override void Down()
        {
            Sql("DROP TRIGGER [OnDictionariesHierarchyCalc]");
            DropForeignKey("dbo.Dictionaries", "ParentID", "dbo.Dictionaries");
            DropForeignKey("dbo.Orders", "UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.RolePermissions", "PermissionID", "dbo.Permissions");
            DropForeignKey("dbo.RolePermissions", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Orders", "StatusID", "dbo.Dictionaries");
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders");
            DropIndex("dbo.RolePermissions", new[] { "PermissionID" });
            DropIndex("dbo.RolePermissions", new[] { "RoleID" });
            DropIndex("dbo.Users", "IX_UsernameUnique");
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropIndex("dbo.Orders", new[] { "StatusID" });
            DropIndex("dbo.Orders", new[] { "UserID" });
            DropIndex("dbo.Dictionaries", new[] { "ParentID" });
            DropTable("dbo.RolePermissions");
            DropTable("dbo.Permissions");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Orders");
            DropTable("dbo.Dictionaries");
        }
    }
}
