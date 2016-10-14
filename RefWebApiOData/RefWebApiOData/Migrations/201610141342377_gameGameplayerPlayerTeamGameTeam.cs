namespace RefWebApiOData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gameGameplayerPlayerTeamGameTeam : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GamePlayers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        IsCaptain = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Colour = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GameTeams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameTeams", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.GameTeams", "GameId", "dbo.Games");
            DropForeignKey("dbo.GamePlayers", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Players", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.GamePlayers", "GameId", "dbo.Games");
            DropIndex("dbo.GameTeams", new[] { "TeamId" });
            DropIndex("dbo.GameTeams", new[] { "GameId" });
            DropIndex("dbo.Players", new[] { "TeamId" });
            DropIndex("dbo.GamePlayers", new[] { "GameId" });
            DropIndex("dbo.GamePlayers", new[] { "PlayerId" });
            DropTable("dbo.GameTeams");
            DropTable("dbo.Teams");
            DropTable("dbo.Players");
            DropTable("dbo.GamePlayers");
        }
    }
}
