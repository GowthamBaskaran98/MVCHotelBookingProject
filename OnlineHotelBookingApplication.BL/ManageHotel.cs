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
        int GetCountOfHotel();
        bool CheckHotelName(string HotelName);
        void UpdateRoomType(HotelRoomBind hotelRoomBind);
        void DeleteRoomType(int HotelRoomId);
        void AcceptHotel(int HotelId);
        void DeclineHotel(int HotelId);
        void BookHotel(BookHotel bookHotel);
        HotelRoomBind GetRoomCategoryDetail(int HotelRoomId);
        List<Hotel> GetHotelByName(string Gmail);
        HotelRoomBind GetRoomTypeDetails(int HotelRoomId);
        RoomCategory GetCategoryById(int RoomId);
        void UpdateRoomCount(int HotelRoomId);
        List<BookHotel> GetBookingDetails(string Gmail);
        int GetAvailableRoomsCount(int HotelId, int RoomId, string CheckIn, string CheckOut);
        Referral GetReferrerDetail(string Gmail);
        List<Referral> GetCandidateDetails(string Gmail);
        void Reward(string ReferrerId);
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
        public int GetCountOfHotel()
        {
            return hotelRepository.GetCountOfHotel();
        }
        public bool CheckHotelName(string HotelName)
        {
            return hotelRepository.CheckHotelName(HotelName);
        }

        public HotelRoomBind GetRoomCategoryDetail(int HotelRoomId)
        {
            return hotelRepository.GetRoomCategoryDetail(HotelRoomId);
        }

        public void UpdateRoomType(HotelRoomBind hotelRoomBind)
        {
            hotelRepository.UpdateRoomType(hotelRoomBind);
        }
        public void DeleteRoomType(int HotelRoomId)
        {
            hotelRepository.DeleteRoomType(HotelRoomId);
        }
        public void AcceptHotel(int HotelId)
        {
            hotelRepository.AcceptHotel(HotelId);
        }
        public void BookHotel(BookHotel bookHotel)
        {
            hotelRepository.BookHotel(bookHotel);
        }
        public List<Hotel> GetHotelByName(string Gmail)
        {
            return hotelRepository.GetHotelByName(Gmail);
        }
        public HotelRoomBind GetRoomTypeDetails(int HotelRoomId)
        {
            return hotelRepository.GetRoomTypeDetails(HotelRoomId);
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
        public List<BookHotel> GetBookingDetails(string Gmail)
        {
            return hotelRepository.GetBookingDetails(Gmail);
        }
        public int GetAvailableRoomsCount(int HotelId, int RoomId, string CheckIn, string CheckOut)
        {
            return hotelRepository.GetAvailableRoomsCount(HotelId, RoomId, CheckIn, CheckOut);
        }
        public Referral GetReferrerDetail(string Gmail)
        {
            return hotelRepository.GetReferrerDetail(Gmail);
        }
        public void Reward(string ReferrerId)
        {
            hotelRepository.Reward(ReferrerId);
        }
        public List<Referral> GetCandidateDetails(string Gmail)
        {
            return hotelRepository.GetCandidateDetails(Gmail);
        }
    }
}