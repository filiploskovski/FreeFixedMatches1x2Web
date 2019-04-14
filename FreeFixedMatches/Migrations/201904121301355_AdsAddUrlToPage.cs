namespace FreeFixedMatches.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdsAddUrlToPage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "Url", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ads", "Url");
        }
    }
}
