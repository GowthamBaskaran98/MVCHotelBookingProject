namespace OnlineHotelBookingApplication.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageSetter : DbMigration
    {
        public override void Up()
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
            
            DropColumn("dbo.HotelDatabase", "HotelImage");
            AlterStoredProcedure(
                "dbo.sp_InsertHotel",
                p => new
                    {
                        HotelName = p.String(),
                        HotelAddress = p.String(),
                        Services = p.String(),
                    },
                body:
                    @"INSERT [dbo].[HotelDatabase]([HotelName], [HotelAddress], [Services])
                      VALUES (@HotelName, @HotelAddress, @Services)
                      
                      DECLARE @HotelId int
                      SELECT @HotelId = [HotelId]
                      FROM [dbo].[HotelDatabase]
                      WHERE @@ROWCOUNT > 0 AND [HotelId] = scope_identity()
                      
                      SELECT t0.[HotelId]
                      FROM [dbo].[HotelDatabase] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[HotelId] = @HotelId"
            );
            
            AlterStoredProcedure(
                "dbo.sp_UpdateHotel",
                p => new
                    {
                        HotelId = p.Int(),
                        HotelName = p.String(),
                        HotelAddress = p.String(),
                        Services = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[HotelDatabase]
                      SET [HotelName] = @HotelName, [HotelAddress] = @HotelAddress, [Services] = @Services
                      WHERE ([HotelId] = @HotelId)"
            );
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.HotelDatabase", "HotelImage", c => c.Binary());
            DropTable("dbo.ImageObjects");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
