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
                        authSystem = c.String(),
                        passwordSystem = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.id)
                .Index(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInfoes", "id", "dbo.Users");
            DropIndex("dbo.UserInfoes", new[] { "id" });
            DropTable("dbo.UserInfoes");
        }
    }
}
