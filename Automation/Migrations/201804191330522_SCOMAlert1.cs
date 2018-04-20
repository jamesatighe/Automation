namespace Automation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SCOMAlert1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SCOMAlert", "AlertState", c => c.String());
            DropColumn("dbo.SCOMAlert", "AlkertState");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SCOMAlert", "AlkertState", c => c.String());
            DropColumn("dbo.SCOMAlert", "AlertState");
        }
    }
}
