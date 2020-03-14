using OnlineHotelBookingApplication.DAL;
using OnlineHotelBookingApplication.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotelBookingApplication.BL
{
    public class ManageHotel
    {
        HotelRepository hotelRepository = new HotelRepository();
        public void AddHotel(Hotel hotel)
        {
            hotelRepository.AddHotel(hotel);
        }
        public List<HotelRoomCategory> GetRoomCategoryDetails(int hotelId)
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
        public void AddRoomCategoryForHotel(HotelRoomCategory hotelRoomCategory)
        {
            hotelRepository.AddRoomCategoryForHotel(hotelRoomCategory);
        }
        public List<Hotel> GetHotelDetails()
        {
            return hotelRepository.GetHotelDetails();
        }
        public Hotel GetHotelDetailsById(int id)
        {
            return hotelRepository.GetHotelDetailsById(id);
        }
        public void AddImage(ImageObject image)
        {
            hotelRepository.AddImage(image);
        }
    }
}
