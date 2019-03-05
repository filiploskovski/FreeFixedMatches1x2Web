namespace FreeFixedMatches.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTitleTagInVipTicket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VipTickets", "Alt", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.VipTickets", "Alt");
        }
    }
}
