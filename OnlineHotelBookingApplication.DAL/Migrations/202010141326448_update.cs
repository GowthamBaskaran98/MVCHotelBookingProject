namespace OnlineHotelBookingApplication.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Referrals",
                c => new
                    {
                        ReferalId = c.Int(nullable: false, identity: true),
                        ReferrerId = c.String(),
                        Candidate = c.String(),
                    })
                .PrimaryKey(t => t.ReferalId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Referrals");
        }
    }
}
