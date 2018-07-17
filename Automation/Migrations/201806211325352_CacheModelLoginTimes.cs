namespace Automation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CacheModelLoginTimes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cache", "LastLogOff", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cache", "LastLogOff");
        }
    }
}
