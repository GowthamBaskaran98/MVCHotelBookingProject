using OnlineHotelBookingApplication.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotelBookingApplication.DAL
{
    public class UserContext : DbContext
    {
        public DbSet<User> dataset { get; set; }
        public DbSet<Hotel> HotelData { get; set; }
        public DbSet<ImageObject> ImageObjects { get; set; }
        public DbSet<RoomCategory> RoomCategory { get; set; }
        public DbSet<HotelRoomCategory> RoomCategories { get; set; }
        public UserContext() : base("ConnectionDB")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HotelRoomCategory>().HasKey(sc => new { sc.HotelId });
            modelBuilder.Entity<User>().MapToStoredProcedures(p => p.Insert(sp => sp.HasName("sp_InsertUser"))
                .Update(sp => sp.HasName("sp_UpdateUser"))
                .Delete(sp => sp.HasName("sp_DeleteUser"))
                );
            modelBuilder.Entity<Hotel>().MapToStoredProcedures(p => p.Insert(sp => sp.HasName("sp_InsertHotel"))
                 .Update(sp => sp.HasName("sp_UpdateHotel"))
                 .Delete(sp => sp.HasName("sp_DeleteHotel"))
                 );
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnModelCreating(ModuleBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });
        //}
    }
}
