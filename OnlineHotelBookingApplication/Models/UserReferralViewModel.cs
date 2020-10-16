using System.Collections.Generic;

namespace OnlineHotelBookingApplication.Models
{
    public class UserReferralViewModel
    {

        public UserViewModel UserViewModel { get; set; }

        public IEnumerable<ReferralViewModel> ReferralViewModels { get; set; }
        
    }
}