using OnlineHotelBookingApplication.DAL;
using OnlineHotelBookingApplication.Entity;
using System.Collections.Generic;
namespace OnlineHotelBookingApplication.BL
{
    public class Details
    {
        UserRepository userRepository = new UserRepository();
        HotelRepository hotelRepository = new HotelRepository();
        public void SignUp(User user)
        {
            userRepository.SignUp(user);
        }
        public List<User> SignIn()
        {
            return userRepository.SignIn();
        }
        public void AddHotel(Hotel hotel)
        {
            hotelRepository.AddHotel(hotel);
        }
        public User GetDetailsById(int id)
        {
            return userRepository.GetDetailsById(id);
        }
        public void Update(User user)
        {
            userRepository.Update(user);
        }
    }
}
