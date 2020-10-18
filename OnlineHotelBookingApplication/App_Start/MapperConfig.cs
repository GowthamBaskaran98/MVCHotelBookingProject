
using OnlineHotelBookingApplication.Entity;
using OnlineHotelBookingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineHotelBookingApplication.App_Start
{
    public class MapperConfig
    {
        public static void RegisterMaps()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<HotelRoomBind, HotelRoomCategoryViewModel>().ForMember(dest => dest.RoomImages, act => act.Ignore());
                config.CreateMap<Hotel, HotelViewModel>().ForMember(dest=>dest.HotelRooms, act=>act.Ignore());
                config.CreateMap<HotelRoomCategoryViewModel, HotelRoomBind>().ForMember(dest => dest.HotelDatabase, act => act.Ignore()).ForMember(dest => dest.RoomCategories, act => act.Ignore());
                config.CreateMap<User, UserViewModel>().ForMember(dest => dest.PromoCode, act => act.Ignore());
            });
        }
    }
}