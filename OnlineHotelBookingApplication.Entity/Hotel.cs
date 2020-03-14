using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OnlineHotelBookingApplication.Entity
{
    [Table("HotelDatabase")]
    public class Hotel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HotelId { get; set; }

        [Required(ErrorMessage = "Hotel Name is required.")]
        public string HotelName { get; set; }

        [Required(ErrorMessage = "Hotel Address is required.")]
        public string HotelAddress { get; set; }

        [Required(ErrorMessage = "Services is required.")]
        public string Services { get; set; }
        public IList<HotelRoomCategory> HotelRoomCategories { get; set; }
        public IList<RoomCategory> RoomCategories { get; set; }

        public Hotel()
        {

        }
    }
}