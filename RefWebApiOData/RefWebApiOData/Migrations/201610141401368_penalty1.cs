namespace RefWebApiOData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class penalty1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Penalties", "PenaltyTypeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Penalties", "PenaltyTypeId", c => c.String());
        }
    }
}
