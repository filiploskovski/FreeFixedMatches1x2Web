namespace FreeFixedMatches.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdsModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TopBottom = c.Boolean(nullable: false),
                        Alt = c.String(),
                        Title = c.String(),
                        ImgUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ads");
        }
    }
}
