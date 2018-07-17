namespace Automation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SCOMAlert_ALertID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SCOMAlert", "AlertID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SCOMAlert", "AlertID");
        }
    }
}
