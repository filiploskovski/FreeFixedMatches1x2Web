namespace FreeFixedMatches.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConstrainsOnVipTicketTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VipTickets", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.VipTickets", "ImagePath", c => c.String(nullable: false));
            AlterColumn("dbo.VipTickets", "Date", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VipTickets", "Date", c => c.String());
            AlterColumn("dbo.VipTickets", "ImagePath", c => c.String());
            AlterColumn("dbo.VipTickets", "Title", c => c.String());
        }
    }
}
