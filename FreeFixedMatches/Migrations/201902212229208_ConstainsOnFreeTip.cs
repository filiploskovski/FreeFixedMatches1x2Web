namespace FreeFixedMatches.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConstainsOnFreeTip : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FreeTips", "Date", c => c.String(nullable: false));
            AlterColumn("dbo.FreeTips", "Match", c => c.String(nullable: false, maxLength: 70));
            AlterColumn("dbo.FreeTips", "Tip", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.FreeTips", "Result", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.FreeTips", "WinLose", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FreeTips", "WinLose", c => c.String());
            AlterColumn("dbo.FreeTips", "Result", c => c.String());
            AlterColumn("dbo.FreeTips", "Tip", c => c.String());
            AlterColumn("dbo.FreeTips", "Match", c => c.String());
            AlterColumn("dbo.FreeTips", "Date", c => c.String());
        }
    }
}
