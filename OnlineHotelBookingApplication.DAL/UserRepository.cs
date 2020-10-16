using System.Collections.Generic;
using OnlineHotelBookingApplication.Entity;
using System.Linq;
using System.Data.Entity;
using System.Data.SqlClient;

namespace OnlineHotelBookingApplication.DAL
{
    public interface IUserRepository
    {
        void SignUp(User user);
        User SignIn(string mail, string password);
        List<User> GetUserDetails();
        User GetDetailsById(int UserId);
        void Delete(User user);
        List<RoomCategory> GetCategory();
        void Update(User updateUser);
        bool CheckEmail(string Gmail);
        bool CheckMobileNumber(long MobileNumber);
        bool CheckPromoCode(User user, string Code);
        User GetUserDetailByName(string Gmail);
        void Reward(string ReferrerId);
    }
    public class UserRepository : IUserRepository
    {
        public static List<User> list = new List<User>();
        public void SignUp(User user)
        {
            using (UserContext userContext = new UserContext())
            {                                                                               //Adding Customer Details
                SqlParameter FirstName = new SqlParameter("@FirstName", user.FirstName);
                SqlParameter LastName = new SqlParameter("@LastName", user.LastName);
                SqlParameter MobileNumber = new SqlParameter("@MobileNumber", user.MobileNumber);
                SqlParameter Gmail = new SqlParameter("@Gmail", user.Gmail);
                SqlParameter Password = new SqlParameter("@Password", user.Password);
                SqlParameter UserType = new SqlParameter("@UserType", user.UserType);
                SqlParameter AccountBalance = new SqlParameter("@AccountBalance", user.AccountBalance);
                int result = userContext.Database.ExecuteSqlCommand("sp_InsertUser @FirstName,@LastName,@MobileNumber,@Gmail,@Password,@UserType,@AccountBalance", FirstName, LastName, MobileNumber, Gmail, Password, UserType, AccountBalance);
                //userContext.dataset.Add(user);
                userContext.SaveChanges();
            }
        }
        public User SignIn(string mail, string password)
        {
            using (UserContext userContext = new UserContext())
            {                                                                               //Checking Email id and Password
                User user = userContext.Users.Where(model => model.Gmail == mail && model.Password == password).SingleOrDefault();
                return user;
            }
        }
        public List<User> GetUserDetails()
        {
            using (UserContext userContext = new UserContext())
            {                                                                               //Getting User Details
                List<User> user = userContext.Users.ToList();
                return user;
            }
        }
        public User GetDetailsById(int UserId)
        {
            using (UserContext userContext = new UserContext())
            {                                                                               //Getting details by Userid
                User user = userContext.Users.Where(model => model.UserId == UserId).SingleOrDefault();
                return user;
            }
        }
        public User GetUserDetailByName(string Gmail)
        {
            using (UserContext userContext = new UserContext())
            {                                                                               //Getting details by Userid
                User user = userContext.Users.Where(model => model.Gmail == Gmail).SingleOrDefault();
                return user;
            }
        }
        public void Delete(User user)
        {
            using (UserContext userContext = new UserContext())
            {
                SqlParameter UserId = new SqlParameter("@UserId", user.UserId);             //Deleting the User Details
                int result = userContext.Database.ExecuteSqlCommand("sp_DeleteUser @UserID", UserId);
                userContext.SaveChanges();
            }
        }
        public List<RoomCategory> GetCategory()
        {
            using (UserContext userContext = new UserContext())
            {
                return userContext.RoomCategories.ToList();
            }
        }
        public void Update(User user)
        {
            using (UserContext userContext = new UserContext())
            {
                SqlParameter UserId = new SqlParameter("@UserId", user.UserId);
                SqlParameter FirstName = new SqlParameter("@FirstName", user.FirstName);
                SqlParameter LastName = new SqlParameter("@LastName", user.LastName);
                SqlParameter MobileNumber = new SqlParameter("@MobileNumber", user.MobileNumber);
                SqlParameter Gmail = new SqlParameter("@Gmail", user.Gmail);
                SqlParameter Password = new SqlParameter("@Password", user.Password);
                SqlParameter UserType = new SqlParameter("@UserType", user.UserType);
                int result = userContext.Database.ExecuteSqlCommand("sp_UpdateUser @UserId,@FirstName,@LastName,@MobileNumber,@Gmail,@Password,@UserType", UserId, FirstName, LastName, MobileNumber, Gmail, Password, UserType);
                //    userContext.Entry(updateUser).State = EntityState.Modified;                 //Updating the User Details
                userContext.SaveChanges();
            }
        }
        public bool CheckEmail(string Gmail)
        {
            using (UserContext userContext = new UserContext())
            {
                User user = userContext.Users.Where(model => model.Gmail == Gmail).SingleOrDefault();
                if (user != null)
                    return false;
                return true;
            }
        }
        public bool CheckMobileNumber(long MobileNumber)
        {
            using (UserContext userContext = new UserContext())
            {
                User user = userContext.Users.Where(model => model.MobileNumber == MobileNumber).SingleOrDefault();
                if (user != null)
                    return false;
                return true;
            }
        }
        public bool CheckPromoCode(User user, string Code)
        {
            using (UserContext userContext = new UserContext())
            {
                var referrer = userContext.Users.Where(model => string.Compare(model.Gmail.Substring(0, 7) + model.UserId.ToString(), Code) == 0).First();
                if (referrer != null)
                {
                    user.AccountBalance += 50;
                    userContext.SaveChanges();
                    Referral referrals = new Referral();
                    referrals.ReferrerId = referrer.Gmail;
                    referrals.Candidate = user.Gmail;
                    userContext.Referrals.Add(referrals);
                    userContext.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public void Reward(string ReferrerId)
        {
            using (UserContext userContext = new UserContext())
            {
                User user = userContext.Users.Where(model => model.Gmail == ReferrerId).SingleOrDefault();
                user.AccountBalance += 50;
                userContext.SaveChanges();                
            }
        }
    }
}
