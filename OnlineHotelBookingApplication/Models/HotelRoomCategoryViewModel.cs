using OnlineHotelBookingApplication.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineHotelBookingApplication.Models
{
    public class HotelRoomCategoryViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HotelRoomCategoryId { get; set; }

        public int HotelId { get; set; }
        //public Hotel Hotel { get; set; }

        public int RoomId { get; set; }
        //public RoomCategory RoomCategory { get; set; }

        public int AvailableRooms { get; set; }
        public int Cost { get; set; }
    }
}