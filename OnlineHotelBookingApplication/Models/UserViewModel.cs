using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace OnlineHotelBookingApplication.Models
{
    public enum UserType
    {
        HotelOwner,
        Customer
    }
    public class UserViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string LastName { get; set; }

        [Required]
        [Remote("NumberExists", "User", HttpMethod = "POST", ErrorMessage = "Mobile Number already registered.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public long MobileNumber { get; set; }

        [Required]
        [Remote("EmailExists", "User", HttpMethod = "POST", ErrorMessage = "Email address already registered.")]
        [RegularExpression("^[_A-Za-z0-9-478\\+]+(\\.[_A-Za-z0-9-]+)*@" + "[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "Enter a valid email id.")]
        public string Gmail { get; set; }

        [Required]
        //[RegularExpression("^[A-Z]")]
        public string Password { get; set; }

        public string UserType { get; set; }
        //public UserViewModel(string firstName, string lastName, long mobileNumber, string gmail, string password, string userType)
        //{
        //    this.firstName = firstName;
        //    this.lastName = lastName;
        //    this.mobileNumber = mobileNumber;
        //    this.gmail = gmail;
        //    this.password = password;
        //}
        //public UserViewModel(string gmail, string password)
        //{
        //    this.gmail = gmail;
        //    this.password = password;
        //}
        public int AccountBalance { get; set; }

        public string PromoCode { get; set; }

        public UserViewModel()
        {

        }
    }
}