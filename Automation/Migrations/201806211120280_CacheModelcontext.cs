namespace Automation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CacheModelcontext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cache",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        HTMLCache = c.String(),
                        LastLogOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cache");
        }
    }
}
