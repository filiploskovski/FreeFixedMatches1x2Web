namespace FreeFixedMatches.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestModel : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            DropTable("dbo.TestTables");
        }
    }
}
