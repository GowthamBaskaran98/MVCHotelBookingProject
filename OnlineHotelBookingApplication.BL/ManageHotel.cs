using OnlineHotelBookingApplication.DAL;
using OnlineHotelBookingApplication.Entity;
using System.Collections.Generic;

namespace OnlineHotelBookingApplication.BL
{
    public interface IManageHotel
    {
        void AddHotel(Hotel hotel);
        List<HotelRoomBind> GetRoomCategoryDetails(int hotelId);
        void UpdateHotel(Hotel updateHotel);
        void DeleteHotel(Hotel deleteHotel);
        List<RoomCategory> GetCategory();
        void AddRoomCategoryForHotel(HotelRoomBind hotelRoomCategory);
        List<Hotel> GetHotelDetails(string Decision);
        Hotel GetHotelDetailsById(int id);
        void AddImage(ImageObject image);
        List<ImageObject> GetImagesByName(string hotelName);
        int GetCountOfHotel();
        bool CheckHotelName(string HotelName);
        void UpdateRoomType(HotelRoomBind hotelRoomBind);
        void DeleteRoomType(int HotelRoomId);
        void BookHotel(BookHotel bookHotel);
        void AcceptHotel(int HotelId);
        void DeclineHotel(int HotelId);
        List<Hotel> GetHotelByName(string Gmail);
        List<BookHotel> GetBookingDetails(string UserName);
        RoomCategory GetCategoryById(int RoomId);
        void UpdateRoomCount(int HotelRoomId);
    }
    public class ManageHotel : IManageHotel
    {
        IHotelRepository hotelRepository;
        public ManageHotel()
        {
            hotelRepository = new HotelRepository();
        }
        public void AddHotel(Hotel hotel)
        {
            hotelRepository.AddHotel(hotel);
        }
        public List<HotelRoomBind> GetRoomCategoryDetails(int hotelId)
        {
            return hotelRepository.GetRoomCategoryDetails(hotelId);
        }
        public void UpdateHotel(Hotel updateHotel)
        {
            hotelRepository.UpdateHotel(updateHotel);
        }
        public void DeleteHotel(Hotel deleteHotel)
        {
            hotelRepository.DeleteHotel(deleteHotel);
        }
        public List<RoomCategory> GetCategory()
        {
            return hotelRepository.GetCategory();
        }
        public void AddRoomCategoryForHotel(HotelRoomBind hotelRoomCategory)
        {
            hotelRepository.AddRoomCategoryForHotel(hotelRoomCategory);
        }
        public List<Hotel> GetHotelDetails(string Decision)
        {
            return hotelRepository.GetHotelDetails(Decision);
        }
        public Hotel GetHotelDetailsById(int id)
        {
            return hotelRepository.GetHotelDetailsById(id);
        }
        public void AddImage(ImageObject image)
        {
            hotelRepository.AddImage(image);
        }
        public List<ImageObject> GetImagesByName(string hotelName)
        {
            return hotelRepository.GetImagesByName(hotelName);
        }
        public int GetCountOfHotel()
        {
            return hotelRepository.GetCountOfHotel();
        }
        public bool CheckHotelName(string HotelName)
        {
            return hotelRepository.CheckHotelName(HotelName);
        }
        public void UpdateRoomType(HotelRoomBind hotelRoomBind)
        {
            hotelRepository.UpdateRoomType(hotelRoomBind);
        }
        public void DeleteRoomType(int HotelRoomId)
        {
            hotelRepository.DeleteRoomType(HotelRoomId);
        }
        public void BookHotel(BookHotel bookHotel)
        {
            hotelRepository.BookHotel(bookHotel);
        }
        public void AcceptHotel(int HotelId)
        {
            hotelRepository.AcceptHotel(HotelId);
        }
        public List<Hotel> GetHotelByName(string Gmail)
        {
            return hotelRepository.GetHotelByName(Gmail);
        }
        public List<BookHotel> GetBookingDetails(string UserName)
        {
            return hotelRepository.GetBookingDetails(UserName);
        }
        public RoomCategory GetCategoryById(int RoomId)
        {
            return hotelRepository.GetCategoryById(RoomId);
        }
        public void DeclineHotel(int HotelId)
        {
            hotelRepository.DeclineHotel(HotelId);
        }
        public void UpdateRoomCount(int HotelRoomId)
        {
            hotelRepository.UpdateRoomCount(HotelRoomId); 
        }
    }
}   