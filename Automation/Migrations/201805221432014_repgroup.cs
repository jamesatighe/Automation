namespace Automation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class repgroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SCOM", "RepGroup", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SCOM", "RepGroup");
        }
    }
}
