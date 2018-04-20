namespace Automation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SCOMAlert : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SCOMAlert",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AlertHealth = c.String(),
                        AlertName = c.String(),
                        MonitorPath = c.String(),
                        MonitoredObject = c.String(),
                        MonitoredObjectHealth = c.String(),
                        PrincipalName = c.String(),
                        Created = c.DateTime(nullable: false),
                        Resolved = c.DateTime(),
                        AlkertState = c.String(),
                        ResolvedBy = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.SCOM", "TotalServers", c => c.Int(nullable: false));
            AddColumn("dbo.SCOM", "ServersError", c => c.Int(nullable: false));
            AddColumn("dbo.SCOM", "ServersWarning", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SCOM", "ServersWarning");
            DropColumn("dbo.SCOM", "ServersError");
            DropColumn("dbo.SCOM", "TotalServers");
            DropTable("dbo.SCOMAlert");
        }
    }
}
