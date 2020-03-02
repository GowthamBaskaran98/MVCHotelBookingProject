using System.Collections.Generic;
using OnlineHotelBookingApplication.Entity;
using System.Linq;
using System.Diagnostics;
using System;

namespace OnlineHotelBookingApplication.DAL
{
    public class UserRepository
    {
        public static List<User> list = new List<User>();
        public IEnumerable<User> Display()
        {
            return list;
        }
        public void SignUp(User user)
        {
            UserContext userContext = new UserContext();
            userContext.dataset.Add(user);
            userContext.SaveChanges();
        }
        public List<User> SignIn()
        {
            UserContext userContext = new UserContext();
            return userContext.dataset.ToList();
        }
        public User GetDetailsById(int UserId)
        {
            UserContext userContext = new UserContext();
            User user = userContext.dataset.Where(model => model.UserId == UserId).SingleOrDefault();
            return user;
        }
        //public void UpdateDetails(User user)
        //{
        //    User detail = GetDetailsById(user.Gmail);
        //    List<UserViewModel> userList = new List<UserViewModel>();
        //    UserViewModel userViewModel = new UserViewModel
        //    {
        //        FirstName = detail.FirstName,
        //        LastName = detail.LastName,
        //        MobileNumber = detail.MobileNumber,
        //        Gmail = detail.Gmail,
        //        Password = detail.Password,
        //        UserType = detail.UserType
        //    };
        //    detail.FirstName = userViewModel.FirstName;
        //    detail.LastName = userViewModel.LastName;
        //}
        /*public void DeleteDetails(int trainId)
        {
            Train train = GetDetailsById(trainId);
            if (train != null)
                trainlist.Remove(train);
        }
        public void UpdateDetails(Train train)
        {
            //Train trains = train.Find(id => id.TrainNo == train.TrainNo);
            Train trains = GetDetailsById(train.TrainNo);
            trains.TrainName = train.TrainName;
        }*/
        /*public Train GetDetailsById(int trainId)
        {
            return trainlist.Find(id => id.TrainNo == trainId);
        }
        public void DeleteDetails(int trainId)
        {
            Train train = GetDetailsById(trainId);
            if (train != null)
                trainlist.Remove(train);
        }*/
        public void Update(User oldUser)
        {
            //Train trains = train.Find(id => id.TrainNo == train.TrainNo);
        }
    }
}
