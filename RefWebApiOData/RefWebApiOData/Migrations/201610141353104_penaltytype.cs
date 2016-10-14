namespace RefWebApiOData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class penaltytype : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PenaltyTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PenaltyTypes");
        }
    }
}
