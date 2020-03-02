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
        //[Required(ErrorMessage = "First Name is required.")]
        //[RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]

        public string HotelName { get; set; }
        //[Required(ErrorMessage = "Last Name is required.")]
        //[RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string HotelAddress { get; set; }
        //[Required(ErrorMessage = "Mobile Number is required.")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public int TotalRooms { get; set; }
        //[Required]
        //[RegularExpression("^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@" + "[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "Enter a valid email id.")]
        public int AvailableRooms { get; set; }
        //[Required]
        public int BookedRooms { get; set; }
        //[Required]
        public string Services { get; set; }
        //[Required]
        public string RoomType { get; set; }
        //public User(int UserId, string FirstName, string LastName, long MobileNumber, string Gmail, string Password, string UserType)
        //{
        //    this.UserId = UserId;
        //    this.FirstName = FirstName;
        //    this.LastName = LastName;
        //    this.MobileNumber = MobileNumber;
        //    this.Gmail = Gmail;
        //    this.Password = Password;
        //}
        //public User(string Gmail, string Password)
        //{
        //    this.Gmail = Gmail;
        //    this.Password = Password;
        //}
        public Hotel()
        {

        }
    }
}
