using System.Collections.Generic;

namespace OnlineHotelBookingApplication.Models
{
    public class HotelTimingViewModel
    {
        public TimingViewModel TimingViewModel { get; set; }
        public HotelViewModel HotelViewModel { get; set; }
        public HotelRoomCategoryViewModel HotelRoomCategoryViewModel { get; set; }
        public IEnumerable<HotelRoomCategoryViewModel > HotelRoomCategoryViewModels { get; set; }
        public IEnumerable<HotelViewModel> HotelViewModels { get; set; }
        public RoomCategoryViewModel RoomCategoryViewModel { get; set; }
        public string Category { get; set; }
    }
}