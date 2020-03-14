using System;
using System.Web;
using System.Collections.Generic;
using OnlineHotelBookingApplication.Entity;
using System.ComponentModel.DataAnnotations;

namespace OnlineHotelBookingApplication.Models
{
    enum RoomType{
        Single,
        Double,
        Triple,
        Quad,
        Queen,
        King,
        Twin
    }
    public class HotelViewModel
    {
        [Key]
        public int HotelId { get; set; }

        [Required(ErrorMessage = "Hotel Name is required.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string HotelName { get; set; }

        [Required(ErrorMessage = "Hotel Address is required.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string HotelAddress { get; set; }

        public string Services { get; set; }

        public HttpPostedFileBase HotelImage { get; set; }

        public DateTime UploadDate { get; set; }

        public IList<HotelRoomCategory> HotelRoomCategory { get; set; }
    }
}