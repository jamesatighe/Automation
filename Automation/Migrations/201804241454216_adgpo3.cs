namespace Automation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adgpo3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AD", "W32TimeSvc", c => c.String());
            DropColumn("dbo.AD", "W32Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AD", "W32Time", c => c.String());
            DropColumn("dbo.AD", "W32TimeSvc");
        }
    }
}
