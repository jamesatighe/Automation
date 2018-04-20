namespace Automation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SCOM2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SCOM",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        NewError = c.Int(nullable: false),
                        NewWarning = c.Int(nullable: false),
                        TotalNew = c.Int(nullable: false),
                        TotalClusters = c.Int(nullable: false),
                        ClustersError = c.Int(nullable: false),
                        ClustersWarning = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SCOM");
        }
    }
}
