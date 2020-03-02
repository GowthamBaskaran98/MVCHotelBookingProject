using OnlineHotelBookingApplication.Entity;
using System.Web.Mvc;
using System.Collections.Generic;
using OnlineHotelBookingApplication.Models;
using OnlineHotelBookingApplication.BL;

namespace OnlineHotelBookingApplication.Controllers
{
    [HandleError]
    public class UserController : Controller
    {
        Details details = new Details();
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignUp(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    FirstName = userViewModel.FirstName,
                    LastName = userViewModel.LastName,
                    MobileNumber = userViewModel.MobileNumber,
                    Gmail = userViewModel.Gmail,
                    Password = userViewModel.Password,
                    UserType = "Customer"
                };
                details.SignUp(user);
                return RedirectToAction("User","Portal");
            }
            return View();
        }
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(SignInViewModel user)
        {
            List<User> list = details.SignIn();
            foreach (var value in list)
            {
                if (value.Gmail.Equals(user.Gmail) && value.Password.Equals(user.Password))
                {
                    if (value.UserType.Equals("Admin"))
                        return RedirectToAction("AdminPage", "Admin");
                    return RedirectToAction("CustomerPage", "Customer");
                }
            }
            return RedirectToAction("User", "SignUp");
        }
        public ActionResult Portal()
        {
            return View();
        }
        //public ActionResult HomePage()
        //{
        //    IEnumerable<User> list = details.Demo();
        //    return View();
        //}
    }
}