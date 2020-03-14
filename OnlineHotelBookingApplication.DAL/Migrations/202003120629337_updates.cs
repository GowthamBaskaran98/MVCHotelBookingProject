namespace OnlineHotelBookingApplication.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        MobileNumber = c.Long(nullable: false),
                        Gmail = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        UserType = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.HotelDatabase",
                c => new
                    {
                        HotelId = c.Int(nullable: false, identity: true),
                        HotelName = c.String(nullable: false),
                        HotelAddress = c.String(nullable: false),
                        Services = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.HotelId);
            
            CreateTable(
                "dbo.HotelRoomCategories",
                c => new
                    {
                        HotelId = c.Int(nullable: false),
                        HotelRoomCategoryId = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(nullable: false),
                        AvailableRooms = c.Int(nullable: false),
                        Cost = c.Int(nullable: false),
                        Hotel_HotelId = c.Int(),
                    })
                .PrimaryKey(t => t.HotelId)
                .ForeignKey("dbo.HotelDatabase", t => t.Hotel_HotelId)
                .Index(t => t.Hotel_HotelId);
            
            CreateTable(
                "dbo.RoomCategories",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        RoomType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RoomId);
            
            CreateTable(
                "dbo.RoomCategoryHotels",
                c => new
                    {
                        RoomCategory_RoomId = c.Int(nullable: false),
                        Hotel_HotelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoomCategory_RoomId, t.Hotel_HotelId })
                .ForeignKey("dbo.RoomCategories", t => t.RoomCategory_RoomId, cascadeDelete: true)
                .ForeignKey("dbo.HotelDatabase", t => t.Hotel_HotelId, cascadeDelete: true)
                .Index(t => t.RoomCategory_RoomId)
                .Index(t => t.Hotel_HotelId);
            
            CreateStoredProcedure(
                "dbo.sp_InsertUser",
                p => new
                    {
                        FirstName = p.String(maxLength: 30),
                        LastName = p.String(maxLength: 30),
                        MobileNumber = p.Long(),
                        Gmail = p.String(),
                        Password = p.String(),
                        UserType = p.String(),
                    },
                body:
                    @"INSERT [dbo].[User]([FirstName], [LastName], [MobileNumber], [Gmail], [Password], [UserType])
                      VALUES (@FirstName, @LastName, @MobileNumber, @Gmail, @Password, @UserType)
                      
                      DECLARE @UserId int
                      SELECT @UserId = [UserId]
                      FROM [dbo].[User]
                      WHERE @@ROWCOUNT > 0 AND [UserId] = scope_identity()
                      
                      SELECT t0.[UserId]
                      FROM [dbo].[User] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[UserId] = @UserId"
            );
            
            CreateStoredProcedure(
                "dbo.sp_UpdateUser",
                p => new
                    {
                        UserId = p.Int(),
                        FirstName = p.String(maxLength: 30),
                        LastName = p.String(maxLength: 30),
                        MobileNumber = p.Long(),
                        Gmail = p.String(),
                        Password = p.String(),
                        UserType = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[User]
                      SET [FirstName] = @FirstName, [LastName] = @LastName, [MobileNumber] = @MobileNumber, [Gmail] = @Gmail, [Password] = @Password, [UserType] = @UserType
                      WHERE ([UserId] = @UserId)"
            );
            
            CreateStoredProcedure(
                "dbo.sp_DeleteUser",
                p => new
                    {
                        UserId = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[User]
                      WHERE ([UserId] = @UserId)"
            );
            
            CreateStoredProcedure(
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
            
            CreateStoredProcedure(
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
            
            CreateStoredProcedure(
                "dbo.sp_DeleteHotel",
                p => new
                    {
                        HotelId = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[HotelDatabase]
                      WHERE ([HotelId] = @HotelId)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.sp_DeleteHotel");
            DropStoredProcedure("dbo.sp_UpdateHotel");
            DropStoredProcedure("dbo.sp_InsertHotel");
            DropStoredProcedure("dbo.sp_DeleteUser");
            DropStoredProcedure("dbo.sp_UpdateUser");
            DropStoredProcedure("dbo.sp_InsertUser");
            DropForeignKey("dbo.RoomCategoryHotels", "Hotel_HotelId", "dbo.HotelDatabase");
            DropForeignKey("dbo.RoomCategoryHotels", "RoomCategory_RoomId", "dbo.RoomCategories");
            DropForeignKey("dbo.HotelRoomCategories", "Hotel_HotelId", "dbo.HotelDatabase");
            DropIndex("dbo.RoomCategoryHotels", new[] { "Hotel_HotelId" });
            DropIndex("dbo.RoomCategoryHotels", new[] { "RoomCategory_RoomId" });
            DropIndex("dbo.HotelRoomCategories", new[] { "Hotel_HotelId" });
            DropTable("dbo.RoomCategoryHotels");
            DropTable("dbo.RoomCategories");
            DropTable("dbo.HotelRoomCategories");
            DropTable("dbo.HotelDatabase");
            DropTable("dbo.User");
        }
    }
}
