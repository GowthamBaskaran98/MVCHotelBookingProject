using OnlineHotelBookingApplication.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineHotelBookingApplication.Models
{
    public class BookViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [MaxLength(30)]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [MaxLength(30)]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public long MobileNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression("^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@" + "[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "Enter a valid email id.")]
        public string Gmail { get; set; }
        
        public string UserId { get; set; }
        public User User { get; set; }

        public int HotelRoomId { get; set; }
        public HotelRoomBind HotelRooms { get; set; }
        
        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

    }
}