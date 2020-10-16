using System;
using System.Web;
using System.Collections.Generic;
using OnlineHotelBookingApplication.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Description { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }
        
        public HttpPostedFileBase HotelImages { get; set; }

        public Byte[] HotelImage { get; set; }

        public DateTime UploadDate { get; set; }

        public string HotelOwner { get; set; }

        public string Permission { get; set; }
        //public int RoomId { get; set; }
        public int AvailableRooms { get; set; }
        //public int Cost { get; set; }
        [NotMapped]
        public IList<HotelRoomBind> HotelRooms { get; set; }
    }
}