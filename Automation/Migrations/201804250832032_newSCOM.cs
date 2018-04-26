namespace Automation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newSCOM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SCOM", "Cluster", c => c.Boolean(nullable: false));
            AddColumn("dbo.SCOM", "Server", c => c.Boolean(nullable: false));
            AddColumn("dbo.SCOM", "HealthState", c => c.String());
            AddColumn("dbo.SCOM", "InMaintenanceMode", c => c.Boolean(nullable: false));
            AddColumn("dbo.SCOM", "DisplayName", c => c.String());
            DropColumn("dbo.SCOM", "NewError");
            DropColumn("dbo.SCOM", "NewWarning");
            DropColumn("dbo.SCOM", "TotalNew");
            DropColumn("dbo.SCOM", "TotalClusters");
            DropColumn("dbo.SCOM", "ClustersError");
            DropColumn("dbo.SCOM", "ClustersWarning");
            DropColumn("dbo.SCOM", "TotalServers");
            DropColumn("dbo.SCOM", "ServersError");
            DropColumn("dbo.SCOM", "ServersWarning");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SCOM", "ServersWarning", c => c.Int(nullable: false));
            AddColumn("dbo.SCOM", "ServersError", c => c.Int(nullable: false));
            AddColumn("dbo.SCOM", "TotalServers", c => c.Int(nullable: false));
            AddColumn("dbo.SCOM", "ClustersWarning", c => c.Int(nullable: false));
            AddColumn("dbo.SCOM", "ClustersError", c => c.Int(nullable: false));
            AddColumn("dbo.SCOM", "TotalClusters", c => c.Int(nullable: false));
            AddColumn("dbo.SCOM", "TotalNew", c => c.Int(nullable: false));
            AddColumn("dbo.SCOM", "NewWarning", c => c.Int(nullable: false));
            AddColumn("dbo.SCOM", "NewError", c => c.Int(nullable: false));
            DropColumn("dbo.SCOM", "DisplayName");
            DropColumn("dbo.SCOM", "InMaintenanceMode");
            DropColumn("dbo.SCOM", "HealthState");
            DropColumn("dbo.SCOM", "Server");
            DropColumn("dbo.SCOM", "Cluster");
        }
    }
}
