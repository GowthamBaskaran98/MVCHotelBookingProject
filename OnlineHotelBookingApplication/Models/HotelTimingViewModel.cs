
using System.Collections.Generic;

namespace OnlineHotelBookingApplication.Models
{
    public class HotelTimingViewModel
    {
        public UserViewModel UserViewModel { get; set; }
        public TimingViewModel TimingViewModel { get; set; }
        public HotelViewModel HotelViewModel { get; set; }
        public HotelRoomCategoryViewModel HotelRoomCategoryViewModel { get; set; }
        public BookViewModel BookViewModel { get; set; }
        public IEnumerable<HotelRoomCategoryViewModel > HotelRoomCategoryViewModels { get; set; }
        public IEnumerable<RoomCategoryViewModel> RoomCategoryViewModels { get; set; }
        public IEnumerable<HotelViewModel> HotelViewModels { get; set; }
        public IEnumerable<HotelTimingViewModel> HotelTimingViewModels { get; set; }
        public RoomCategoryViewModel RoomCategoryViewModel { get; set; }
        public string Category { get; set; }
    }
}