namespace Automation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permission",
                c => new
                    {
                        PermissionID = c.Int(nullable: false, identity: true),
                        PowerShellID = c.Int(nullable: false),
                        GroupName = c.String(),
                        Read = c.Boolean(nullable: false),
                        Write = c.Boolean(nullable: false),
                        Modify = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PermissionID)
                .ForeignKey("dbo.PowerShell", t => t.PowerShellID, cascadeDelete: true)
                .Index(t => t.PowerShellID);
            
            CreateTable(
                "dbo.PowerShell",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ScriptName = c.String(),
                        ScriptContent = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        LastRunDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Permission", "PowerShellID", "dbo.PowerShell");
            DropIndex("dbo.Permission", new[] { "PowerShellID" });
            DropTable("dbo.PowerShell");
            DropTable("dbo.Permission");
        }
    }
}
