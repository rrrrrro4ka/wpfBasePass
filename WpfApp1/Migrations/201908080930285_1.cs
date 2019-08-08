namespace WpfApp1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        id = c.Int(nullable: false),
                        authSystem = c.String(nullable: false),
                        login = c.String(nullable: false),
                        passwordSystem = c.String(nullable: false),
                        web = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        pass = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInfoes", "id", "dbo.Users");
            DropIndex("dbo.UserInfoes", new[] { "id" });
            DropTable("dbo.Users");
            DropTable("dbo.UserInfoes");
        }
    }
}
