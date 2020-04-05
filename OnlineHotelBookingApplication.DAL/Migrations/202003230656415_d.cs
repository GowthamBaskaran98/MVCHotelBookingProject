namespace OnlineHotelBookingApplication.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HotelDatabase", "HotelOwner", c => c.String());
            AlterStoredProcedure(
                "dbo.sp_InsertHotel",
                p => new
                    {
                        HotelName = p.String(maxLength: 20),
                        Description = p.String(),
                        Street = p.String(),
                        City = p.String(),
                        State = p.String(),
                        HotelImage = p.Binary(),
                        UploadDate = p.DateTime(),
                        HotelOwner = p.String(),
                    },
                body:
                    @"INSERT [dbo].[HotelDatabase]([HotelName], [Description], [Street], [City], [State], [HotelImage], [UploadDate], [HotelOwner])
                      VALUES (@HotelName, @Description, @Street, @City, @State, @HotelImage, @UploadDate, @HotelOwner)
                      
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
                        HotelName = p.String(maxLength: 20),
                        Description = p.String(),
                        Street = p.String(),
                        City = p.String(),
                        State = p.String(),
                        HotelImage = p.Binary(),
                        UploadDate = p.DateTime(),
                        HotelOwner = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[HotelDatabase]
                      SET [HotelName] = @HotelName, [Description] = @Description, [Street] = @Street, [City] = @City, [State] = @State, [HotelImage] = @HotelImage, [UploadDate] = @UploadDate, [HotelOwner] = @HotelOwner
                      WHERE ([HotelId] = @HotelId)"
            );
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.HotelDatabase", "HotelOwner");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
