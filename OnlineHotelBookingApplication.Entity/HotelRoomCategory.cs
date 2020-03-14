using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineHotelBookingApplication.Entity
{
    public class HotelRoomCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HotelRoomCategoryId { get; set; }

        public int HotelId { get; set; }
        [NotMapped]
        public Hotel Hotel { get; set; }

        public int RoomId { get; set; }
        [NotMapped]
        public RoomCategory RoomCategory { get; set; } 

        public int AvailableRooms { get; set; }
        public int Cost { get; set; }

        //public int AvailableRoom { get; set; }

        //public int BookedRooom { get; set; }

    }
}
