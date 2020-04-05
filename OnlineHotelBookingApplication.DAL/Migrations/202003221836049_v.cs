namespace OnlineHotelBookingApplication.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookHotels", "CheckIn", c => c.DateTime(nullable: false));
            AddColumn("dbo.BookHotels", "CheckOut", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookHotels", "CheckOut");
            DropColumn("dbo.BookHotels", "CheckIn");
        }
    }
}
