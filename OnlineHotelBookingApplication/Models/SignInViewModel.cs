using System.ComponentModel.DataAnnotations;

namespace OnlineHotelBookingApplication.Models
{
    public class SignInViewModel
    {
        [Key]
        [RegularExpression("^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@" + "[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "Enter a valid email id.")]
        public string Gmail { get; set; }
        [Required]
        public string Password { get; set; }
    }
}