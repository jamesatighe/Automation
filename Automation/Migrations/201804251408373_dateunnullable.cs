namespace Automation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateunnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SCOMAlert", "Resolved", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SCOMAlert", "Resolved", c => c.DateTime());
        }
    }
}
