using System.Collections.Generic;
using OnlineHotelBookingApplication.Entity;
using System.Linq;

namespace OnlineHotelBookingApplication.DAL
{
    public class Repository
    {
        public static List<User> list = new List<User>();
        public IEnumerable<User> Display()
        {
            return list;
        }
        public static List<User> Demo()
        {
            UserContext user = new UserContext();
            return user.DataBase.ToList();
        }
        //static Repository()
        //{
        //    User user = new User("Gowtham", "12345Aa#");
        //    list.Add(user);
        //    user = new User("Bhanu", "12345Aa#");
        //    list.Add(user);
        //    user = new User("Manju", "12345Aa#");
        //    list.Add(user);
        //}
        public void Add(User user)
        {
            list.Add(user);
        }
    }
}
