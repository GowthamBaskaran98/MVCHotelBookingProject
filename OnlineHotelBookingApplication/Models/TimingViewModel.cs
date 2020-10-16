using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineHotelBookingApplication.Models
{
    public class TimingViewModel
    {
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }
    }
}