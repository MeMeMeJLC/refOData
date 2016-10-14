namespace RefWebApiOData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Substitutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GamePlayerGoingOffTheFieldId = c.Int(nullable: false),
                        GamePlayerGoingOnTheFieldId = c.Int(nullable: false),
                        SubstitutionTime = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GamePlayers", t => t.GamePlayerGoingOffTheFieldId, cascadeDelete: false)
                .ForeignKey("dbo.GamePlayers", t => t.GamePlayerGoingOnTheFieldId, cascadeDelete: false)
                .Index(t => t.GamePlayerGoingOffTheFieldId)
                .Index(t => t.GamePlayerGoingOnTheFieldId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Substitutions", "GamePlayerGoingOnTheFieldId", "dbo.GamePlayers");
            DropForeignKey("dbo.Substitutions", "GamePlayerGoingOffTheFieldId", "dbo.GamePlayers");
            DropIndex("dbo.Substitutions", new[] { "GamePlayerGoingOnTheFieldId" });
            DropIndex("dbo.Substitutions", new[] { "GamePlayerGoingOffTheFieldId" });
            DropTable("dbo.Substitutions");
        }
    }
}
