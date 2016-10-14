namespace RefWebApiOData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class penalty : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Penalties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GamePlayerId = c.Int(nullable: false),
                        PenaltyTypeId = c.String(),
                        PenaltyTime = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.GamePlayers", "Penalty_Id", c => c.Int());
            AddColumn("dbo.PenaltyTypes", "Penalty_Id", c => c.Int());
            CreateIndex("dbo.GamePlayers", "Penalty_Id");
            CreateIndex("dbo.PenaltyTypes", "Penalty_Id");
            AddForeignKey("dbo.GamePlayers", "Penalty_Id", "dbo.Penalties", "Id");
            AddForeignKey("dbo.PenaltyTypes", "Penalty_Id", "dbo.Penalties", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PenaltyTypes", "Penalty_Id", "dbo.Penalties");
            DropForeignKey("dbo.GamePlayers", "Penalty_Id", "dbo.Penalties");
            DropIndex("dbo.PenaltyTypes", new[] { "Penalty_Id" });
            DropIndex("dbo.GamePlayers", new[] { "Penalty_Id" });
            DropColumn("dbo.PenaltyTypes", "Penalty_Id");
            DropColumn("dbo.GamePlayers", "Penalty_Id");
            DropTable("dbo.Penalties");
        }
    }
}
