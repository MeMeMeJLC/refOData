namespace RefWebApiOData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gameplayerchange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GamePlayers", "Penalty_Id", "dbo.Penalties");
            DropIndex("dbo.GamePlayers", new[] { "Penalty_Id" });
            CreateTable(
                "dbo.PenaltyGamePlayers",
                c => new
                    {
                        Penalty_Id = c.Int(nullable: false),
                        GamePlayer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Penalty_Id, t.GamePlayer_Id })
                .ForeignKey("dbo.Penalties", t => t.Penalty_Id, cascadeDelete: true)
                .ForeignKey("dbo.GamePlayers", t => t.GamePlayer_Id, cascadeDelete: true)
                .Index(t => t.Penalty_Id)
                .Index(t => t.GamePlayer_Id);
            
            AddColumn("dbo.Substitutions", "GamePlayer_Id", c => c.Int());
            CreateIndex("dbo.Substitutions", "GamePlayer_Id");
            AddForeignKey("dbo.Substitutions", "GamePlayer_Id", "dbo.GamePlayers", "Id");
            DropColumn("dbo.GamePlayers", "Penalty_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GamePlayers", "Penalty_Id", c => c.Int());
            DropForeignKey("dbo.Substitutions", "GamePlayer_Id", "dbo.GamePlayers");
            DropForeignKey("dbo.PenaltyGamePlayers", "GamePlayer_Id", "dbo.GamePlayers");
            DropForeignKey("dbo.PenaltyGamePlayers", "Penalty_Id", "dbo.Penalties");
            DropIndex("dbo.PenaltyGamePlayers", new[] { "GamePlayer_Id" });
            DropIndex("dbo.PenaltyGamePlayers", new[] { "Penalty_Id" });
            DropIndex("dbo.Substitutions", new[] { "GamePlayer_Id" });
            DropColumn("dbo.Substitutions", "GamePlayer_Id");
            DropTable("dbo.PenaltyGamePlayers");
            CreateIndex("dbo.GamePlayers", "Penalty_Id");
            AddForeignKey("dbo.GamePlayers", "Penalty_Id", "dbo.Penalties", "Id");
        }
    }
}
