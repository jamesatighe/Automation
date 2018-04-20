namespace Automation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AutomationContext : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PowerShell", "ScriptContent", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PowerShell", "ScriptContent", c => c.String());
        }
    }
}
