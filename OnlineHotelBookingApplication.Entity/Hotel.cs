using System;
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
        [MaxLength(20)]
        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Hotel Name is required.")]
        public string HotelName { get; set; }

        [Required(ErrorMessage = "Hotel Address is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Hotel Address is required.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Hotel Address is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Hotel Address is required.")]
        public string State { get; set; }
        
        public Byte[] HotelImage { get; set; }

        public DateTime UploadDate { get; set; }

        public string HotelOwner { get; set; }
        
        public string Permission { get; set; }
        //public IList<Demo> Demos { get; set; }
        [NotMapped]
        public IList<HotelRoomBind> HotelRooms { get; set; }

        public Hotel()
        {

        }
    }
}