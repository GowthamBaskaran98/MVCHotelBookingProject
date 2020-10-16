using System.Data.Entity;
using OnlineHotelBookingApplication.Entity;

namespace OnlineHotelBookingApplication.DAL
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Hotel> HotelDatabases { get; set; }
        public DbSet<RoomCategory> RoomCategories { get; set; }
        public DbSet<HotelRoomBind> HotelRooms { get; set; }
        public DbSet<ImageObject> ImageObjects { get; set; }
        public DbSet<BookHotel> BookHotels { get; set; }
        public DbSet<Referral> Referrals { get; set; }
        //public DbSet<HotelRoomCategory> HotelRoomCategories { get; set; }
        //public DbSet<HotelRoomCategory> HotelRoomCategories { get; set; }
        public UserContext() : base("ConnectionDB")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HotelRoomBind>().HasKey(sc => new { sc.HotelRoomId });
            modelBuilder.Entity<User>().MapToStoredProcedures(p => p.Insert(sp => sp.HasName("sp_InsertUser"))
                .Update(sp => sp.HasName("sp_UpdateUser"))
                .Delete(sp => sp.HasName("sp_DeleteUser"))
                );
            modelBuilder.Entity<Hotel>().MapToStoredProcedures(p => p.Insert(sp => sp.HasName("sp_InsertHotel"))
                 .Update(sp => sp.HasName("sp_UpdateHotel"))
                 .Delete(sp => sp.HasName("sp_DeleteHotel"))
                 );
            modelBuilder.Entity<HotelRoomBind>().MapToStoredProcedures(p => p.Insert(sp => sp.HasName("sp_InsertRoomType"))
                 .Update(sp => sp.HasName("sp_UpdateRoomType"))
                 .Delete(sp => sp.HasName("sp_DeleteRoomType"))
                 );
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnModelCreating(ModuleBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });
        //}
    }
}
