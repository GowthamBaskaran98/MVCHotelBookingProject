namespace OnlineHotelBookingApplication.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datass : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BookHotels", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookHotels", "Password", c => c.String(nullable: false));
        }
    }
}
