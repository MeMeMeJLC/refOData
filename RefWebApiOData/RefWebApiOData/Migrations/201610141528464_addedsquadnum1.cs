namespace RefWebApiOData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedsquadnum1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Players", "SquadNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Players", "SquadNumber", c => c.Int(nullable: false));
        }
    }
}
