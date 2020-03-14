using OnlineHotelBookingApplication.Entity;

namespace OnlineHotelBookingApplication.Models
{
    public class MapperConfig
    {
        public static void Maps()
        {
            AutoMapper.Mapper.Initialize(Config =>
            {
                Config.CreateMap<UserViewModel, User>();
                Config.CreateMap<HotelViewModel, Hotel>();
                Config.CreateMap<HotelRoomCategoryViewModel, HotelRoomCategory>();
            });
        }
    }
}