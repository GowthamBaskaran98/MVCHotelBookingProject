namespace OnlineHotelBookingApplication.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class m : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ImageObjects");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ImageObjects",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        HotelImage = c.Binary(),
                        UploadDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ImageId);
            
        }
    }
}
