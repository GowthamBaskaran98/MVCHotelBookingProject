using OnlineHotelBookingApplication.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace OnlineHotelBookingApplication.Models
{
    public class HotelRoomCategoryViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HotelRoomId { get; set; }
       
        public int HotelId { get; set; }
        public Hotel HotelDatabase { get; set; }

        public int RoomId { get; set; }
        public RoomCategory RoomCategories { get; set; }

        public int AdultsPerRoom { get; set; }
        
        public int TotalRooms { get; set; }

        //public int BookedRooms { get; set; }

        //public int VacantRooms { get; set; }

        public int Cost { get; set; }
        
        public HttpPostedFileBase RoomImages { get; set; }

        public Byte[] RoomImage { get; set; }

        public DateTime UploadDate { get; set; }

        public int AvailableRooms { get;  set; }

    }
}
