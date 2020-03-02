using OnlineHotelBookingApplication.Entity;
using System.Web.Mvc;
using System.Collections.Generic;
using OnlineHotelBookingApplication.Models;
using OnlineHotelBookingApplication.BL;
using OnlineHotelBookingApplication.DAL;

namespace OnlineHotelBookingApplication.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult AdminPage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminPage(HotelViewModel hotelViewModel)
        {
            return RedirectToAction("ManageHotel");
        }
        // GET: Admin
        public ActionResult ManageHotel()
        {
            return View();
        }
        public ActionResult AddHotel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddHotel(HotelViewModel hotelViewModel)
        {
            Details details = new Details();
            if (ModelState.IsValid)
            {
                Hotel hotel = new Hotel
                {
                    HotelName = hotelViewModel.HotelName,
                    HotelAddress = hotelViewModel.HotelAddress,
                    TotalRooms = hotelViewModel.TotalRooms,
                    BookedRooms = hotelViewModel.BookedRooms,
                    AvailableRooms = hotelViewModel.AvailableRooms,
                    Services = hotelViewModel.Services,
                    RoomType = hotelViewModel.RoomType
                };
                details.AddHotel(hotel);
                return RedirectToAction("User", "Portal");
            }
            return View();
        }
        public ActionResult ManageUser()
        {
            Details details = new Details();
            List<User> list = details.SignIn();
            List<UserViewModel> userList = new List<UserViewModel>();
            foreach (User detail in list)
            {
                UserViewModel userViewModel = new UserViewModel
                {
                    UserId = detail.UserId,
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    MobileNumber = detail.MobileNumber,
                    Gmail = detail.Gmail,
                    Password = detail.Password,
                    UserType = detail.UserType
                };
                userList.Add(userViewModel);
            }
            return View(userList);
        }
        [HttpGet]
        public ActionResult Edit(UserViewModel userViewModel)
        {
            Details detail = new Details();
            User user = new User
            {
                UserId = userViewModel.UserId
            };
            User userView = detail.GetDetailsById(user.UserId);
            return View(userView);
        }
        [HttpPost]
        public ActionResult Update(UserViewModel userViewModel)
        {
            User user = new User
            {
                UserId = userViewModel.UserId,
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                MobileNumber = userViewModel.MobileNumber,
                Gmail = userViewModel.Gmail,
                Password = userViewModel.Password,
                UserType = userViewModel.UserType
            };
            UserContext userContext = new UserContext();
            userContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
            userContext.SaveChanges();
            return RedirectToAction("ManageUser", "Admin");
        }
        public ActionResult Delete(UserViewModel userViewModel)
        {
            UserContext userContext = new UserContext();
            User user = new User
            {
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                MobileNumber = userViewModel.MobileNumber,
                Gmail = userViewModel.Gmail,
                Password = userViewModel.Password,
                UserType = "Customer"
            };
            userContext.dataset.Remove(user);
            return RedirectToAction("ManageUser", "Admin");
        }
        //[HttpPost]
        //public ActionResult Update(UserViewModel userViewModel)
        //{
        //    Details details = new Details();
        //    User user = new User
        //    {
        //        FirstName = userViewModel.FirstName,
        //        LastName = userViewModel.LastName,
        //        MobileNumber = userViewModel.MobileNumber,
        //        Gmail = userViewModel.Gmail,
        //        Password = userViewModel.Password,
        //        UserType = "Customer"
        //    };
        //    details.Update(user);
        //    TempData["Details"] = "Details updated";
        //    return RedirectToAction("Index");
        //    //}
        //    //return View("Edit",packageDetails);
        //}
    }
}
