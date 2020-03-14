using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineHotelBookingApplication.Entity
{
    public class ImageObject
    {
        [Key]
        public int ImageId { get; set; }
        public string FileName { get; set; }
        public Byte[] HotelImage { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
