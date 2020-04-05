namespace OnlineHotelBookingApplication.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BPs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HotelRoomBinds", "RoomImage", c => c.Binary());
            AddColumn("dbo.HotelRoomBinds", "UploadDate", c => c.DateTime(nullable: false));
            AlterStoredProcedure(
                "dbo.sp_InsertRoomType",
                p => new
                    {
                        HotelId = p.Int(),
                        RoomId = p.Int(),
                        AdultsPerRoom = p.Int(),
                        AvailableRooms = p.Int(),
                        Cost = p.Int(),
                        RoomImage = p.Binary(),
                        UploadDate = p.DateTime(),
                    },
                body:
                    @"INSERT [dbo].[HotelRoomBinds]([HotelId], [RoomId], [AdultsPerRoom], [AvailableRooms], [Cost], [RoomImage], [UploadDate])
                      VALUES (@HotelId, @RoomId, @AdultsPerRoom, @AvailableRooms, @Cost, @RoomImage, @UploadDate)
                      
                      DECLARE @HotelRoomId int
                      SELECT @HotelRoomId = [HotelRoomId]
                      FROM [dbo].[HotelRoomBinds]
                      WHERE @@ROWCOUNT > 0 AND [HotelRoomId] = scope_identity()
                      
                      SELECT t0.[HotelRoomId]
                      FROM [dbo].[HotelRoomBinds] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[HotelRoomId] = @HotelRoomId"
            );
            
            AlterStoredProcedure(
                "dbo.sp_UpdateRoomType",
                p => new
                    {
                        HotelRoomId = p.Int(),
                        HotelId = p.Int(),
                        RoomId = p.Int(),
                        AdultsPerRoom = p.Int(),
                        AvailableRooms = p.Int(),
                        Cost = p.Int(),
                        RoomImage = p.Binary(),
                        UploadDate = p.DateTime(),
                    },
                body:
                    @"UPDATE [dbo].[HotelRoomBinds]
                      SET [HotelId] = @HotelId, [RoomId] = @RoomId, [AdultsPerRoom] = @AdultsPerRoom, [AvailableRooms] = @AvailableRooms, [Cost] = @Cost, [RoomImage] = @RoomImage, [UploadDate] = @UploadDate
                      WHERE ([HotelRoomId] = @HotelRoomId)"
            );
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.HotelRoomBinds", "UploadDate");
            DropColumn("dbo.HotelRoomBinds", "RoomImage");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
