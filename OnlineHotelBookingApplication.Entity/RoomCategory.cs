using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineHotelBookingApplication.Entity
{
    public class RoomCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomId { get; set; }
        [Required]
        public string RoomType { get; set; }
        public IList<HotelRoomBind> HotelRooms { get; set; }
    }
}
