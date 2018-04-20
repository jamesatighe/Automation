namespace Automation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SCOMAlert2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SCOMAlert", "IsMonitorAlert", c => c.Boolean(nullable: false));
            DropColumn("dbo.SCOMAlert", "AlertState");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SCOMAlert", "AlertState", c => c.String());
            DropColumn("dbo.SCOMAlert", "IsMonitorAlert");
        }
    }
}
