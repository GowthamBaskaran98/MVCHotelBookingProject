namespace OnlineHotelBookingApplication.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bind : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HotelRoomBinds", "TotalRooms", c => c.Int(nullable: false));
            AddColumn("dbo.HotelRoomBinds", "BookedRooms", c => c.Int(nullable: false));
            AddColumn("dbo.HotelRoomBinds", "VacantRooms", c => c.Int(nullable: false));
            DropColumn("dbo.HotelRoomBinds", "AvailableRooms");
            AlterStoredProcedure(
                "dbo.sp_InsertRoomType",
                p => new
                    {
                        HotelId = p.Int(),
                        RoomId = p.Int(),
                        AdultsPerRoom = p.Int(),
                        TotalRooms = p.Int(),
                        BookedRooms = p.Int(),
                        VacantRooms = p.Int(),
                        Cost = p.Int(),
                        RoomImage = p.Binary(),
                        UploadDate = p.DateTime(),
                    },
                body:
                    @"INSERT [dbo].[HotelRoomBinds]([HotelId], [RoomId], [AdultsPerRoom], [TotalRooms], [BookedRooms], [VacantRooms], [Cost], [RoomImage], [UploadDate])
                      VALUES (@HotelId, @RoomId, @AdultsPerRoom, @TotalRooms, @BookedRooms, @VacantRooms, @Cost, @RoomImage, @UploadDate)
                      
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
                        TotalRooms = p.Int(),
                        BookedRooms = p.Int(),
                        VacantRooms = p.Int(),
                        Cost = p.Int(),
                        RoomImage = p.Binary(),
                        UploadDate = p.DateTime(),
                    },
                body:
                    @"UPDATE [dbo].[HotelRoomBinds]
                      SET [HotelId] = @HotelId, [RoomId] = @RoomId, [AdultsPerRoom] = @AdultsPerRoom, [TotalRooms] = @TotalRooms, [BookedRooms] = @BookedRooms, [VacantRooms] = @VacantRooms, [Cost] = @Cost, [RoomImage] = @RoomImage, [UploadDate] = @UploadDate
                      WHERE ([HotelRoomId] = @HotelRoomId)"
            );
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.HotelRoomBinds", "AvailableRooms", c => c.Int(nullable: false));
            DropColumn("dbo.HotelRoomBinds", "VacantRooms");
            DropColumn("dbo.HotelRoomBinds", "BookedRooms");
            DropColumn("dbo.HotelRoomBinds", "TotalRooms");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
