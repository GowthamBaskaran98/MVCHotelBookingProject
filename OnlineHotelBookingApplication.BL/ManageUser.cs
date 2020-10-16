using OnlineHotelBookingApplication.DAL;
using OnlineHotelBookingApplication.Entity;
using System.Collections.Generic;

namespace OnlineHotelBookingApplication.BL
{
    public interface IManageUser
    {
        void SignUp(User user);
        User SignIn(string mail, string password);
        List<User> GetUserDetails();
        void Update(User updateUser);
        void Delete(User userView);
        User GetDetailsById(int id);
        bool CheckEmail(string Gmail);
        bool CheckMobileNumber(long MobileNumber);
        bool CheckPromoCode(User User, string Code);
        User GetUserDetailByName(string Gmail);
    }
    public class ManageUser : IManageUser
    {
        //UserRepository userRepository = new UserRepository();
        IUserRepository userRepository;
        public ManageUser()
        {
            userRepository = new UserRepository();
        }
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
        public bool CheckEmail(string Gmail)
        {
            return userRepository.CheckEmail(Gmail);
        }
        public bool CheckMobileNumber(long MobileNumber)
        {
            return userRepository.CheckMobileNumber(MobileNumber);
        }
        public bool CheckPromoCode(User user, string Code)
        {
            return userRepository.CheckPromoCode(user,Code);
        }
        public User GetUserDetailByName(string Gmail)
        {
            return userRepository.GetUserDetailByName(Gmail);
        }
    }
}
