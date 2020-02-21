using System.ComponentModel.DataAnnotations;
namespace OnlineHotelBookingApplication.Entity
{
    public enum UserType
    {
        HotelOwner,
        Customer
    }
    public class User
    {
        [Required(ErrorMessage = "First Name is required.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        public long mobileNumber { get; set; }
        [Required]
        public string gmail { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public UserType userType { get; set; }
        public User(string firstName, string lastName, long mobileNumber, string gmail, string password, string userType)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.mobileNumber = mobileNumber;
            this.gmail = gmail;
            this.password = password;
        }
        public User(string gmail, string password)
        {
            this.gmail = gmail;
            this.password = password;
        }
        public User()
        {

        }
    }
}
