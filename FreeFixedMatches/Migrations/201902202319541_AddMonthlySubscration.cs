namespace FreeFixedMatches.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMonthlySubscration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MonthlySubscrations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.String(),
                        TotalOdd = c.String(),
                        Match = c.String(),
                        Tip = c.String(),
                        Odd = c.Double(nullable: false),
                        Result = c.String(),
                        WinLose = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MonthlySubscrations");
        }
    }
}
