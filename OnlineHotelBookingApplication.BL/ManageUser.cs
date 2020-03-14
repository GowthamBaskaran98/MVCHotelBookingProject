using OnlineHotelBookingApplication.DAL;
using OnlineHotelBookingApplication.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotelBookingApplication.BL
{
    public class ManageUser
    {
        UserRepository userRepository = new UserRepository();
        public void SignUp(User user)
        {
            userRepository.SignUp(user);
        }
        public User SignIn(string mail, string password)
        {
            return userRepository.SignIn(mail, password);
        }
        public List<User> GetUserDetails()
        {
            return userRepository.GetUserDetails();
        }
        public void Update(User updateUser)
        {
            userRepository.Update(updateUser);
        }
        public void Delete(User userView)
        {
            userRepository.Delete(userView);
        }
        public User GetDetailsById(int id)
        {
            return userRepository.GetDetailsById(id);
        }
    }
}
