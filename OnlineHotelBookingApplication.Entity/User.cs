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
        //[Required(ErrorMessage = "First Name is required.")]
        //[RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string firstName { get; set; }
        //[Required(ErrorMessage = "Last Name is required.")]
        //[RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string lastName { get; set; }
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public long mobileNumber { get; set; }
        //[Required]
        public string gmail { get; set; }
        //[RegularExpression("^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@" + "[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
         //[Required]
        public string password { get; set; }
        //[RegularExpression("([a-z]|[A-Z]|[0-9]|[\\W]){4}[a-zA-Z0-9\\W]{3,11}", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        //[Required]
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
