using OnlineHotelBookingApplication.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineHotelBookingApplication.Models
{
    public class RoomCategoryViewModel
    {
        [Key]
        public int RoomId { get; set; }
        [Required]
        public string RoomType { get; set; }
        public IList<HotelRoomBind> HotelRoomCategories { get; set; }
    }
}