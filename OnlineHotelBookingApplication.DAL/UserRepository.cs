using System.Collections.Generic;
using OnlineHotelBookingApplication.Entity;
using System.Linq;
using System.Data.Entity;
using System.Data.SqlClient;

namespace OnlineHotelBookingApplication.DAL
{
    public class UserRepository
    {
        public static List<User> list = new List<User>();
        public void SignUp(User user)
        {
            using (UserContext userContext = new UserContext())
            {
                SqlParameter FirstName = new SqlParameter("@FirstName", user.FirstName);
                SqlParameter LastName = new SqlParameter("@LastName", user.LastName);
                SqlParameter MobileNumber = new SqlParameter("@MobileNumber", user.MobileNumber);
                SqlParameter Gmail = new SqlParameter("@Gmail", user.Gmail);
                SqlParameter Password = new SqlParameter("@Password", user.Password);
                SqlParameter UserType = new SqlParameter("@UserType", user.UserType);
                int result = userContext.Database.ExecuteSqlCommand("sp_InsertUser @FirstName,@LastName,@MobileNumber,@Gmail,@Password,@UserType", FirstName, LastName, MobileNumber, Gmail, Password, UserType);
                //userContext.dataset.Add(user);
                userContext.SaveChanges();
            }
        }
        public User SignIn(string mail,string password)
        {
            using (UserContext userContext = new UserContext())
            {
                User user = userContext.dataset.Where(model => model.Gmail == mail && model.Password == password).SingleOrDefault();
                return user;
            }
        }
        public List<User> GetUserDetails()
        {
            using (UserContext userContext = new UserContext())
            {
                List<User> user = userContext.dataset.ToList();
                return user;
            }
        }
        public User GetDetailsById(int UserId)
        {
            using (UserContext userContext = new UserContext())
            {
                User user = userContext.dataset.Where(model => model.UserId == UserId).SingleOrDefault();
                return user;
            }
        }
        public void Delete(User user)
        {
            using (UserContext userContext = new UserContext())
            {
                SqlParameter UserId = new SqlParameter("@UserId", user.UserId);
                int result = userContext.Database.ExecuteSqlCommand("sp_DeleteUser @UserID", UserId);
                //userContext.dataset.Attach(user);
                //userContext.dataset.Remove(user);
                userContext.SaveChanges();
            }
        }
        public List<RoomCategory> GetCategory()
        {
            using (UserContext userContext = new UserContext())
            {
                return userContext.RoomCategory.ToList();
            }
        }
        public void Update(User updateUser)
        {
            using (UserContext userContext = new UserContext())
            {
                userContext.Entry(updateUser).State = EntityState.Modified;
                userContext.SaveChanges();
            }
        }
        //public void DeleteHotel(Hotel deleteHotel)
        //{
        //    using (UserContext userContext = new UserContext())
        //    {
        //        userContext.HotelData.Attach(deleteHotel);
        //        userContext.HotelData.Remove(deleteHotel);
        //        userContext.SaveChanges();
        //    }
        //}
        //public void UpdateHotel(Hotel updateHotel)
        //{
        //    using (UserContext userContext = new UserContext())
        //    {
        //        userContext.Entry(updateHotel).State = EntityState.Modified;
        //        userContext.SaveChanges();
        //    }
        //}
    }
}
