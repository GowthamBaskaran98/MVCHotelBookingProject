using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Collections.Generic;
using OnlineHotelBookingApplication.Entity;
using System;

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
        //void AddImage(ImageObject image);
        //List<ImageObject> GetImagesByName(string hotelName);
        int GetCountOfHotel();
        bool CheckHotelName(string HotelName);
        void UpdateRoomType(HotelRoomBind hotelRoomBind);
       // void EditRoomType(HotelRoomBind hotelRoomBind);
        void DeleteRoomType(int HotelRoomId);
        void AcceptHotel(int HotelId);
        void BookHotel(BookHotel bookHotel);
        void DeclineHotel(int HotelId);
        HotelRoomBind GetRoomCategoryDetail(int HotelRoomId);
        List<Hotel> GetHotelByName(string Gmail);
        HotelRoomBind GetRoomTypeDetails(int hotelRoomId);
        List<BookHotel> GetBookingDetails(string Gmail);
        RoomCategory GetCategoryById(int RoomId);
        void UpdateRoomCount(int HotelRoomId);
        int GetAvailableRoomsCount(int HotelId, int RoomId, string CheckIn, string CheckOut);
        Referral GetReferrerDetail(string Gmail);
        void Reward(string ReferrerId);
        List<Referral> GetCandidateDetails(string Gmail);
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
        public int GetAvailableRoomsCount(int HotelId, int RoomId, string CheckIn, string CheckOut)
        {
            using (UserContext userContext = new UserContext())
            {
                DateTime dt;
                dt = DateTime.Parse(CheckIn).AddDays(1);
                DateTime dt1;
                dt1 = DateTime.Parse(CheckIn).AddDays(-1);
                int totalRooms;
                int count = (from p in userContext.BookHotels where dt >= p.CheckIn && dt1 <= p.CheckIn && HotelId == p.HotelRooms.HotelId select p).Count();
                if (RoomId == 0)
                    totalRooms = (from num in userContext.HotelRooms where (HotelId == num.HotelId) select num.TotalRooms).Sum();
                else
                    totalRooms = (from num in userContext.HotelRooms where (HotelId == num.HotelId && RoomId == num.RoomId) select num.TotalRooms).SingleOrDefault();
                return totalRooms - count;
            }
        }
        public List<HotelRoomBind> GetRoomCategoryDetails(int hotelId)
        {
            using (UserContext userContext = new UserContext())
            {                                                                                        //Getting Available Room Categories for a hotel
                List<HotelRoomBind> hotel = userContext.HotelRooms.Include("RoomCategories").Include("HotelDatabase").Where(model => model.HotelId == hotelId).ToList();
                return hotel;
            }
        }
        public Hotel GetHotelDetailsById(int HotelId)
        {
            using (UserContext userContext = new UserContext())
            {                                                                                       //Getting Hotel Detail Based on Hotel Id
                Hotel hotel = userContext.HotelDatabases.Where(model => model.HotelId == HotelId).SingleOrDefault();
                return hotel;
            }
        }
        public HotelRoomBind GetRoomCategoryDetail(int HotelRoomId)
        {
            using (UserContext userContext = new UserContext())
            {
                HotelRoomBind hotelRoom = userContext.HotelRooms.Where(model => model.HotelRoomId == HotelRoomId).SingleOrDefault();
                return hotelRoom;
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
            {                                                                                       //For Checking Hotel Name
                Hotel hotel = userContext.HotelDatabases.Where(model => model.HotelName == HotelName).SingleOrDefault();
                if(hotel!=null)
                    return true;
                return false;
            }
        }
        public void UpdateRoomType(HotelRoomBind hotelRoomBind)
        {
            using (UserContext userContext = new UserContext())
            {                                                                                         //For Updating the details of room type
                userContext.Entry(hotelRoomBind).State = EntityState.Modified;
                userContext.SaveChanges();
            }
        }
        //public void EditRoomType(HotelRoomBind hotelRoomBind)
        //{
        //    using (UserContext userContext = new UserContext())
        //    {

        //    }
        //}
        public void DeleteRoomType(int HotelRoomId)
        {
            using (UserContext userContext = new UserContext())
            {                                                                                               
                SqlParameter HotelRoom = new SqlParameter("@HotelRoomId", HotelRoomId);           //Deleting Hotel  Based on Hotel Id
                int result = userContext.Database.ExecuteSqlCommand("sp_DeleteRoomType @HotelRoomId", HotelRoom);
            }
        }
        public void BookHotel(BookHotel bookHotel)
        {
            using (UserContext userContext = new UserContext())
            {                                                                                      //Booking the hotel
                userContext.BookHotels.Add(bookHotel);
                userContext.SaveChanges();
            }
        }
        public void AcceptHotel(int HotelId)
        {
            using (UserContext userContext = new UserContext())
            {                                                                                         //Approving the requested hotel by HotelId
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
            {                                                                                           //Declining the requested hotel by HotelId
                var hotel = userContext.HotelDatabases.Find(HotelId);
                string Gmail = hotel.HotelOwner;
                if (hotel.Permission != "Declined")
                    hotel.Permission = "Declined";
                else
                {
                    SqlParameter HotelIds = new SqlParameter("@HotelId", HotelId);           //Deleting Hotel  Based on Hotel Id
                    int result = userContext.Database.ExecuteSqlCommand("sp_DeleteHotel @HotelId", HotelIds);
                }   
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
        public HotelRoomBind GetRoomTypeDetails(int HotelRoomId)
        {
            using (UserContext userContext = new UserContext())
            {
                HotelRoomBind hotelRoomDetails = userContext.HotelRooms.Where(model => model.HotelRoomId == HotelRoomId).SingleOrDefault();
                return hotelRoomDetails;
            }
        }
        public List<BookHotel> GetBookingDetails(string Gmail)
        {
            using (UserContext userContext = new UserContext())
            {
                List<BookHotel> bookHotels = userContext.BookHotels.Where(model => model.UserId == Gmail).ToList();
                return bookHotels;
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
                //var availableRooms = userContext.
                //hotelRoom.TotalRooms += 1;
                userContext.SaveChanges();
            }
        }
        public Referral GetReferrerDetail(string Gmail)
        {
            using (UserContext userContext = new UserContext())
            {
                return userContext.Referrals.Where(model => model.Candidate == Gmail).SingleOrDefault();
            }
        }
        public void Reward(string ReferrerId)
        {
            using (UserContext userContext = new UserContext())
            {
                User user = userContext.Users.Where(model => model.Gmail == ReferrerId).SingleOrDefault();
                user.AccountBalance += 50;
                userContext.SaveChanges();
            }
        }
        public List<Referral> GetCandidateDetails(string Gmail)
        {
            using (UserContext userContext = new UserContext())
            {
                return userContext.Referrals.Where(model => model.ReferrerId == Gmail).ToList();
            }
        }
    }
}