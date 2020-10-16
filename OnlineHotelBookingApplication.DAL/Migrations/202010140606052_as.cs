namespace OnlineHotelBookingApplication.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _as : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "AccountBalance", c => c.Int(nullable: false));
            AlterStoredProcedure(
                "dbo.sp_InsertUser",
                p => new
                    {
                        FirstName = p.String(maxLength: 30),
                        LastName = p.String(maxLength: 30),
                        MobileNumber = p.Long(),
                        Gmail = p.String(),
                        Password = p.String(),
                        UserType = p.String(),
                        AccountBalance = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[User]([FirstName], [LastName], [MobileNumber], [Gmail], [Password], [UserType], [AccountBalance])
                      VALUES (@FirstName, @LastName, @MobileNumber, @Gmail, @Password, @UserType, @AccountBalance)
                      
                      DECLARE @UserId int
                      SELECT @UserId = [UserId]
                      FROM [dbo].[User]
                      WHERE @@ROWCOUNT > 0 AND [UserId] = scope_identity()
                      
                      SELECT t0.[UserId]
                      FROM [dbo].[User] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[UserId] = @UserId"
            );
            
            AlterStoredProcedure(
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
                        AccountBalance = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[User]
                      SET [FirstName] = @FirstName, [LastName] = @LastName, [MobileNumber] = @MobileNumber, [Gmail] = @Gmail, [Password] = @Password, [UserType] = @UserType, [AccountBalance] = @AccountBalance
                      WHERE ([UserId] = @UserId)"
            );
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "AccountBalance");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
