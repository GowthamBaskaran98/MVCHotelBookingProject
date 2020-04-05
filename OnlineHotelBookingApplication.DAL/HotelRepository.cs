using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Collections.Generic;
using OnlineHotelBookingApplication.Entity;

namespace OnlineHotelBookingApplication.DAL
{
    public interface IHotelRepository
    {
        void AddHotel(Hotel hotel);
        List<RoomCategory> GetCategory();
        void AddRoomCategoryForHotel(HotelRoomBind hotelRoomCategory);
        List<Hotel> GetHotelDetails(string Decision);
        List<HotelRoomBind> GetRoomCategoryDetails(int hotelId);
        Hotel GetHotelDetailsById(int HotelId);
        void DeleteHotel(Hotel deleteHotel);
        void UpdateHotel(Hotel updateHotel);
        void AddImage(ImageObject image);
        List<ImageObject> GetImagesByName(string hotelName);
        int GetCountOfHotel();
        bool CheckHotelName(string HotelName);
        void UpdateRoomType(HotelRoomBind hotelRoomBind);
        void DeleteRoomType(int HotelRoomId);
        void AcceptHotel(int HotelId);
        void DeclineHotel(int HotelId);
        List<Hotel> GetHotelByName(string Gmail);
        RoomCategory GetCategoryById(int RoomId);
        void UpdateRoomCount(int HotelRoomId);
    }
    public class HotelRepository : IHotelRepository
    {
        public void AddHotel(Hotel hotel)
        {
            using (UserContext userContext = new UserContext())
            {
                //SqlParameter HotelName = new SqlParameter("@HotelName", hotel.HotelName);
                //SqlParameter Description = new SqlParameter("@Description", hotel.Description);
                //SqlParameter Street = new SqlParameter("@Street", hotel.Street);                                         
                //SqlParameter City = new SqlParameter("@City", hotel.City);
                //SqlParameter State = new SqlParameter("@State", hotel.State);
                //SqlParameter HotelImage = new SqlParameter("@HotelImage", hotel.HotelImage);
                //SqlParameter UploadDate = new SqlParameter("@UploadDate", hotel.UploadDate);
                //int result = userContext.Database.ExecuteSqlCommand("sp_InsertHotel @HotelName,@Description,@Street,@City,@State,@HotelImage,@UploadDate", HotelName, Description, Street, City, State, HotelImage, UploadDate);
                userContext.HotelDatabases.Add(hotel);                                              //Adding Hotel details
                userContext.SaveChanges();
            }
        }
        //public static List<RoomCategory> GetCategories()
        //{
        //    using (UserContext userContext = new UserContext())
        //    {
        //        return userContext.RoomCategories.ToList();
        //    }
        //}
        public List<RoomCategory> GetCategory()
        {
            using (UserContext userContext = new UserContext())
            {
                return userContext.RoomCategories.ToList();                                         //Getting RoomCategories
            }
        }
        public void AddRoomCategoryForHotel(HotelRoomBind hotelRoomCategory)
        {
            using (UserContext userContext = new UserContext())
            {
                userContext.HotelRooms.Add(hotelRoomCategory);                                       //Adding RoomCategories
                userContext.SaveChanges();
            }
        }
        public List<Hotel> GetHotelDetails(string Decision)
        {
            using (UserContext userContext = new UserContext())
            {
                return userContext.HotelDatabases.Where(model=>model.Permission == Decision).ToList();                                          //Getting Hotels Details
            }
        }
        public List<HotelRoomBind> GetRoomCategoryDetails(int hotelId)
        {
            using (UserContext userContext = new UserContext())
            {                                                                                        //Getting Available Room Categories for a hotel
                List<HotelRoomBind> hotel = userContext.HotelRooms.Include("RoomCategories").Include("HotelDatabase").Where(model => model.HotelId == hotelId).ToList();
                //var room = (from rc in userContext.RoomCategories
                //            join rd in userContext.HotelRooms on rc.RoomId equals rd.RoomId where rd.HotelId == rd.HotelId                           select new
                //            {
                //                rc.RoomType,
                //                rd.AvailableRooms,
                //                rd.AdultsPerRoom,
                //                rd.Cost
                //            }).ToList();
                //List<> hotelRoomCategory = userContext.RoomCategories.Where(model => model.HotelId == hotelId).ToList();
                return hotel;            }
        }
        public Hotel GetHotelDetailsById(int HotelId)
        {
            using (UserContext userContext = new UserContext())
            {                                                                                       //Getting Hotel Detail Based on Hotel Id
                Hotel hotel = userContext.HotelDatabases.Where(model => model.HotelId == HotelId).SingleOrDefault();
                return hotel;
            }
        }
        public void DeleteHotel(Hotel deleteHotel)
        {
            using (UserContext userContext = new UserContext())
            {
                SqlParameter HotelId = new SqlParameter("@HotelId", deleteHotel.HotelId);           //Deleting Hotel  Based on Hotel Id
                int result = userContext.Database.ExecuteSqlCommand("sp_DeleteHotel @HotelId", HotelId);
                //userContext.HotelDatabases.Attach(deleteHotel);
                //userContext.HotelDatabases.Remove(deleteHotel);
                //userContext.SaveChanges();
            }
        }
        public void UpdateHotel(Hotel updateHotel)
        {
            using (UserContext userContext = new UserContext())
            {                                                                                       //Updating Hotel Details
                userContext.Entry(updateHotel).State = EntityState.Modified;
                userContext.SaveChanges();
            }
        }
        public void AddImage(ImageObject image)
        {
            using (UserContext userContext = new UserContext())
            {
                userContext.ImageObjects.Add(image);                                                //Adding Image in database
                userContext.SaveChanges();
            }
        }
        public List<ImageObject> GetImagesByName(string hotelName)
        {
            using (UserContext userContext = new UserContext())
            {                                                                                       //Getting Image using Hotel Name
                List<ImageObject> hotel = userContext.ImageObjects.Where(model => model.FileName == hotelName).ToList();
                return hotel;
            }
        }
        public int GetCountOfHotel()
        {
            using (UserContext userContext = new UserContext())     
            {                                                                                       //For Getting the total Number of Hotel
                return userContext.HotelDatabases.Count();
            }
        }
        public bool CheckHotelName(string HotelName)
        {
            using (UserContext userContext = new UserContext())
            {
                Hotel hotel = userContext.HotelDatabases.Where(model => model.HotelName == HotelName).SingleOrDefault();
                if(hotel!=null)
                    return true;
                return false;
            }
        }
        public void UpdateRoomType(HotelRoomBind hotelRoomBind)
        {
            using (UserContext userContext = new UserContext())
            {
                userContext.Entry(hotelRoomBind).State = EntityState.Modified;
                userContext.SaveChanges();
            }
        }
        public void DeleteRoomType(int HotelRoomId)
        {
            using (UserContext userContext = new UserContext())
            {
                SqlParameter HotelRoom = new SqlParameter("@HotelRoomId", HotelRoomId);           //Deleting Hotel  Based on Hotel Id
                int result = userContext.Database.ExecuteSqlCommand("sp_DeleteRoomType @HotelRoomId", HotelRoom);
            }
        }
        public void AcceptHotel(int HotelId)
        {
            using (UserContext userContext = new UserContext())
            {
                var hotel = userContext.HotelDatabases.Find(HotelId);
                string Gmail = hotel.HotelOwner;
                hotel.Permission = "Approved";
                userContext.SaveChanges();
                var user = userContext.Users.Where(model => model.Gmail == Gmail).SingleOrDefault();
                user.UserType = "HotelOwner";
                userContext.SaveChanges();
            }
        }
        public void DeclineHotel(int HotelId)
        {
            using (UserContext userContext = new UserContext())
            {
                var hotel = userContext.HotelDatabases.Find(HotelId);
                string Gmail = hotel.HotelOwner;
                hotel.Permission = "Declined";
                userContext.SaveChanges();
            }
        }
        public List<Hotel> GetHotelByName(string Gmail)
        {
            using (UserContext userContext = new UserContext())
            {                                                                                       //Getting Image using Hotel Name
                List<Hotel> hotel = userContext.HotelDatabases.Where(model => model.HotelOwner == Gmail).ToList();
                return hotel;
            }
        }
        public RoomCategory GetCategoryById(int RoomId)
        {
            using (UserContext userContext = new UserContext())
            {
                return userContext.RoomCategories.Where(model => model.RoomId == RoomId).SingleOrDefault();
            }
        }
        public void UpdateRoomCount(int HotelRoomId)
        {
            using (UserContext userContext = new UserContext())
            {
                var hotelRoom = userContext.HotelRooms.Find(HotelRoomId);
                hotelRoom.BookedRooms += 1;
                hotelRoom.VacantRooms -= 1;
                userContext.SaveChanges();
            }
        }
    }
}