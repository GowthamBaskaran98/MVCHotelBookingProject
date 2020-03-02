using OnlineHotelBookingApplication.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotelBookingApplication.DAL
{
    public class HotelRepository
    {
        public void AddHotel(Hotel hotel)
        {
            UserContext userContext = new UserContext();
            userContext.HotelData.Add(hotel);
            userContext.SaveChanges();
        }
    } 
}
