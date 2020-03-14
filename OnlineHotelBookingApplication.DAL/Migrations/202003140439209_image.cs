namespace OnlineHotelBookingApplication.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HotelDatabase", "HotelImage", c => c.Binary());
            AlterStoredProcedure(
                "dbo.sp_InsertHotel",
                p => new
                    {
                        HotelName = p.String(),
                        HotelAddress = p.String(),
                        Services = p.String(),
                        HotelImage = p.Binary(),
                    },
                body:
                    @"INSERT [dbo].[HotelDatabase]([HotelName], [HotelAddress], [Services], [HotelImage])
                      VALUES (@HotelName, @HotelAddress, @Services, @HotelImage)
                      
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
                        HotelImage = p.Binary(),
                    },
                body:
                    @"UPDATE [dbo].[HotelDatabase]
                      SET [HotelName] = @HotelName, [HotelAddress] = @HotelAddress, [Services] = @Services, [HotelImage] = @HotelImage
                      WHERE ([HotelId] = @HotelId)"
            );
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.HotelDatabase", "HotelImage");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
