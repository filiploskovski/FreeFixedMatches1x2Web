namespace FreeFixedMatches.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MonthlyTotalStringToDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MonthlySubscrations", "TotalOdd", c => c.Double(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MonthlySubscrations", "TotalOdd", c => c.String());
        }
    }
}
