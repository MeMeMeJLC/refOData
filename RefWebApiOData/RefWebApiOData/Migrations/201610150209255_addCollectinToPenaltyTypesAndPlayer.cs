namespace RefWebApiOData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCollectinToPenaltyTypesAndPlayer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PenaltyTypes", "Penalty_Id", "dbo.Penalties");
            DropIndex("dbo.PenaltyTypes", new[] { "Penalty_Id" });
            CreateTable(
                "dbo.PenaltyTypePenalties",
                c => new
                    {
                        PenaltyType_Id = c.Int(nullable: false),
                        Penalty_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PenaltyType_Id, t.Penalty_Id })
                .ForeignKey("dbo.PenaltyTypes", t => t.PenaltyType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Penalties", t => t.Penalty_Id, cascadeDelete: true)
                .Index(t => t.PenaltyType_Id)
                .Index(t => t.Penalty_Id);
            
            DropColumn("dbo.PenaltyTypes", "Penalty_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PenaltyTypes", "Penalty_Id", c => c.Int());
            DropForeignKey("dbo.PenaltyTypePenalties", "Penalty_Id", "dbo.Penalties");
            DropForeignKey("dbo.PenaltyTypePenalties", "PenaltyType_Id", "dbo.PenaltyTypes");
            DropIndex("dbo.PenaltyTypePenalties", new[] { "Penalty_Id" });
            DropIndex("dbo.PenaltyTypePenalties", new[] { "PenaltyType_Id" });
            DropTable("dbo.PenaltyTypePenalties");
            CreateIndex("dbo.PenaltyTypes", "Penalty_Id");
            AddForeignKey("dbo.PenaltyTypes", "Penalty_Id", "dbo.Penalties", "Id");
        }
    }
}
