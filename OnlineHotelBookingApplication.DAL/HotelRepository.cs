using OnlineHotelBookingApplication.Entity;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace OnlineHotelBookingApplication.DAL
{
    public class HotelRepository
    {
        public void AddHotel(Hotel hotel)
        {
            using (UserContext userContext = new UserContext())
            {
                //SqlParameter HotelName = new SqlParameter("@HotelName", hotel.HotelName);
                //SqlParameter HotelAddress = new SqlParameter("@HotelAddress", hotel.HotelAddress);
                //SqlParameter Services = new SqlParameter("@Services", hotel.Services);
                //int result = userContext.Database.ExecuteSqlCommand("sp_InsertHotel @HotelName,@HotelAddress,@Services", HotelName, HotelAddress, Services);
                userContext.HotelData.Add(hotel);
                userContext.SaveChanges();
            }
        }
        public static List<RoomCategory> GetCategories()
        {
            using (UserContext userContext = new UserContext())
            {
                return userContext.RoomCategory.ToList();
            }
        }
        public List<RoomCategory> GetCategory()
        {
            //using (SqlConnection sqlConnection = DbUtil.DbConnection())
            //{
            //    using (SqlCommand sqlCommand = new SqlCommand("sp_GetCategories", sqlConnection))
            //    {
            //        sqlCommand.CommandType = CommandType.StoredProcedure;
            //        SqlDataAdapter sqlDataAdpater = new SqlDataAdapter(sqlCommand);
            //        DataTable dataTable = new DataTable();
            //        sqlDataAdpater.Fill(dataTable);
            //        return dataTable;
            //    }
            //}
            using (UserContext userContext = new UserContext())
            {
                return userContext.RoomCategory.ToList();
            }
        }
        public void AddRoomCategoryForHotel(HotelRoomCategory hotelRoomCategory)
        {
            using (UserContext userContext = new UserContext())
            {
                userContext.RoomCategories.Add(hotelRoomCategory);
                userContext.SaveChanges();
            }
        }
        public List<Hotel> GetHotelDetails()
        {
            using (UserContext userContext = new UserContext())
            {
                return userContext.HotelData.ToList();
            }
        }
        public List<HotelRoomCategory> GetRoomCategoryDetails(int hotelId)
        {
            using (UserContext userContext = new UserContext())
            {
                List<HotelRoomCategory> hotel = userContext.RoomCategories.Where(model => model.HotelId == hotelId).ToList();
                //List<> hotelRoomCategory = userContext.RoomCategories.Where(model => model.HotelId == hotelId).ToList();
                return hotel;
            }
        }
        public Hotel GetHotelDetailsById(int HotelId)
        {
            using (UserContext userContext = new UserContext())
            {
                Hotel hotel = userContext.HotelData.Where(model => model.HotelId == HotelId).SingleOrDefault();
                return hotel;
            }
        }
        public void DeleteHotel(Hotel deleteHotel)
        {
            using (UserContext userContext = new UserContext())
            {
                SqlParameter HotelId = new SqlParameter("@HotelId", deleteHotel.HotelId);
                int result = userContext.Database.ExecuteSqlCommand("sp_DeleteHotel @HotelId", HotelId);
                //userContext.HotelData.Attach(deleteHotel);
                //userContext.HotelData.Remove(deleteHotel);
                userContext.SaveChanges();
            }
        }
        public void UpdateHotel(Hotel updateHotel)
        {
            using (UserContext userContext = new UserContext())
            {
                userContext.Entry(updateHotel).State = EntityState.Modified;
                userContext.SaveChanges();
            }
        }
        public void AddImage(ImageObject image)
        {
            using (UserContext userContext = new UserContext())
            {
                userContext.ImageObjects.Add(image);
                userContext.SaveChanges();
            }
        }
    }
}
