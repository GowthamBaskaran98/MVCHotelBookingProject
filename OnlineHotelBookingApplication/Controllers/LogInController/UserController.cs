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
        ManageUser userDetails = new ManageUser();
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
                userViewModel.UserType = "Customer";
                User user = AutoMapper.Mapper.Map<UserViewModel, User>(userViewModel);
                userDetails.SignUp(user);
                return RedirectToAction("SignIn", "User");
            }
            return View();
        }
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(SignInViewModel signInViewModel)
        {
            //List<User> list = details.SignIn();
            //foreach (var value in list)
            //{
            //    if (value.Gmail.Equals(user.Gmail) && value.Password.Equals(user.Password))
            //    {
            //        if (value.UserType.Equals("Admin"))
            //            return RedirectToAction("AdminPage", "Admin");
            //        return RedirectToAction("CustomerPage", "Customer");
            //    }
            //}
            User user = userDetails.SignIn(signInViewModel.Gmail, signInViewModel.Password);
            if (user != null)
            {
                if (user.UserType.Equals("Admin"))
                    return RedirectToAction("AdminPage", "Admin");
                return RedirectToAction("CustomerPage", "Customer");
            }
            return RedirectToAction("SignUp", "User");
        }
        public ActionResult Portal()
        {
            return View();
        }
    }
}
