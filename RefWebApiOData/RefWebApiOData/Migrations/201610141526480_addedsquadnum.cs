namespace RefWebApiOData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedsquadnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GamePlayers", "SquadNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Players", "SquadNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "SquadNumber");
            DropColumn("dbo.GamePlayers", "SquadNumber");
        }
    }
}
