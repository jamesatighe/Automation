namespace Automation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adgpo2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AD",
                c => new
                    {
                        ADId = c.Int(nullable: false, identity: true),
                        Domain = c.String(),
                        DomainController = c.String(),
                        Connection = c.String(),
                        RepTestState = c.String(),
                        RepTestResult = c.String(),
                        NetLogonTestState = c.String(),
                        NetLogonTestResult = c.String(),
                        NetLogonSvc = c.String(),
                        NTDSSvc = c.String(),
                        KDCSvc = c.String(),
                        DNSSvc = c.String(),
                        W32Time = c.String(),
                        DFSRSvc = c.String(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ADId);
            
            CreateTable(
                "dbo.GPO",
                c => new
                    {
                        GPOId = c.Int(nullable: false, identity: true),
                        DomainName = c.String(),
                        Name = c.String(),
                        Guid = c.String(),
                        Status = c.String(),
                        UserADVer = c.String(),
                        UserSysvolVer = c.String(),
                        CompADVer = c.String(),
                        CompSysvolVer = c.String(),
                        GPOCreated = c.DateTime(nullable: false),
                        GPOModified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.GPOId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GPO");
            DropTable("dbo.AD");
        }
    }
}
