using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineHotelBookingApplication.Models
{
    public class RoomViewModel
    {
        public HotelRoomCategoryViewModel HotelRoomCategoryViewModel { get; set; }
        public HotelViewModel HotelViewModel { get; set; }
        public IEnumerable<HotelRoomCategoryViewModel> HotelRoomCategoryViewModels { get; set; }
        public HttpPostedFileBase RoomImages { get; set; }
        public Byte[] RoomImage { get; set; }
        public string HotelOwner;
    }
}