namespace OnlineHotelBookingApplication.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookHotels", "FirstName", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.BookHotels", "LastName", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.BookHotels", "MobileNumber", c => c.Long(nullable: false));
            AddColumn("dbo.BookHotels", "Gmail", c => c.String(nullable: false));
            AddColumn("dbo.BookHotels", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookHotels", "Password");
            DropColumn("dbo.BookHotels", "Gmail");
            DropColumn("dbo.BookHotels", "MobileNumber");
            DropColumn("dbo.BookHotels", "LastName");
            DropColumn("dbo.BookHotels", "FirstName");
        }
    }
}
