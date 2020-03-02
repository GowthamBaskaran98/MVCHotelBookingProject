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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HotelId { get; set; }
        [Required(ErrorMessage = "Hotel Name is required.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string HotelName { get; set; }
        [Required(ErrorMessage = "Hotel Address is required.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string HotelAddress { get; set; }
        [Required]
        [Range(1,5)]
        public int TotalRooms { get; set; }
        [Required]
        [Range(1, 5)]
        public int AvailableRooms { get; set; }
        [Required]
        [Range(1, 5)]
        public int BookedRooms { get; set; }
        public string Services { get; set; }
        [Required(ErrorMessage = "Room Type is required.")]
        public string RoomType { get; set; }
        //public HotelViewModel(int HotelId, string HotelName, string HotelAddress, int TotalRooms, int AvailableRooms,int BookedRooms, string Services, string RoomType)
        //{
        //    this.HotelId = HotelId;
        //    this.HotelName = HotelName;
        //    this.HotelAddress = HotelAddress;
        //    this.TotalRooms = TotalRooms;
        //    this.AvailableRooms = AvailableRooms;
        //    this.BookedRooms = BookedRooms;
        //    this.Services = Services;
        //    this.RoomType = RoomType;
        //}
    }
}