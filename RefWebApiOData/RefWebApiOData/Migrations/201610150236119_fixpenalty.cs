namespace RefWebApiOData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixpenalty : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PenaltyGamePlayers", "Penalty_Id", "dbo.Penalties");
            DropForeignKey("dbo.PenaltyGamePlayers", "GamePlayer_Id", "dbo.GamePlayers");
            DropIndex("dbo.PenaltyGamePlayers", new[] { "Penalty_Id" });
            DropIndex("dbo.PenaltyGamePlayers", new[] { "GamePlayer_Id" });
            CreateIndex("dbo.Penalties", "GamePlayerId");
            AddForeignKey("dbo.Penalties", "GamePlayerId", "dbo.GamePlayers", "Id", cascadeDelete: true);
            DropTable("dbo.PenaltyGamePlayers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PenaltyGamePlayers",
                c => new
                    {
                        Penalty_Id = c.Int(nullable: false),
                        GamePlayer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Penalty_Id, t.GamePlayer_Id });
            
            DropForeignKey("dbo.Penalties", "GamePlayerId", "dbo.GamePlayers");
            DropIndex("dbo.Penalties", new[] { "GamePlayerId" });
            CreateIndex("dbo.PenaltyGamePlayers", "GamePlayer_Id");
            CreateIndex("dbo.PenaltyGamePlayers", "Penalty_Id");
            AddForeignKey("dbo.PenaltyGamePlayers", "GamePlayer_Id", "dbo.GamePlayers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PenaltyGamePlayers", "Penalty_Id", "dbo.Penalties", "Id", cascadeDelete: true);
        }
    }
}
