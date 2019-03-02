namespace FreeFixedMatches.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeToDb : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.TestTables");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TestTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestUser = c.String(),
                        Utakmica = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
