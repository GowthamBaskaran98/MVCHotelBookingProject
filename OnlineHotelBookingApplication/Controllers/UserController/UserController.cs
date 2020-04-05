using OnlineHotelBookingApplication.Entity;
using System.Web.Mvc;
using System.Collections.Generic;
using OnlineHotelBookingApplication.Models;
using OnlineHotelBookingApplication.BL;
using System;
using System.Web.Security;
using System.Web;

namespace OnlineHotelBookingApplication.Controllers
{
    [HandleError]
    public class UserController : Controller
    {
        IManageUser userDetails;
        public UserController()
        {
            userDetails = new ManageUser();
        }
        //ManageUser userDetails = new ManageUser();
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult SignUp(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                userViewModel.UserType = "Customer";
                User user = AutoMapper.Mapper.Map<UserViewModel, User>(userViewModel);
                userDetails.SignUp(user);                                                       //Adding Customer Detials To database
                //return Content("<script language='javascript' type='text/javascript'>alert('Registered Successfully!');</script>");
                return RedirectToAction("SignIn", "User");
            }
            return View();
        }
        public JsonResult EmailExists(string Gmail)
        {
            return Json(userDetails.CheckEmail(Gmail));
        }
        public JsonResult NumberExists(long MobileNumber)
        {
            return Json(userDetails.CheckMobileNumber(MobileNumber));
        }
        //public JsonResult NumberExists(long MobileNumber)
        //{
        //    List<User> user = userDetails.GetUserDetails();
        //    foreach (User details in user)
        //    {                                                                                           //Checking the uniqueness of Email
        //        return Json(!String.Equals(Convert.ToString(MobileNumber), Convert.ToString(details.MobileNumber), StringComparison.OrdinalIgnoreCase));
        //    }
        //    return Json(true);
        //}
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                FormsAuthentication.SetAuthCookie(user.Gmail, false);
                var authTicket = new FormsAuthenticationTicket(1, user.Gmail, DateTime.Now, DateTime.Now.AddMinutes(20) , false, user.UserType);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpContext.Response.Cookies.Add(authCookie);
                return RedirectToAction("HomePage", "Home");
                //if (user.UserType.Equals("Admin"))
                //    return RedirectToAction("AdminPage", "Admin");
                //return RedirectToAction("CustomerPage", "Customer");
            }
            return RedirectToAction("SignUp", "User");
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Homepage", "Home");
        }
        public ActionResult Portal()
        {
            return View();
        }
        public ActionResult ManageUser()
        {
            List<User> list = userDetails.GetUserDetails();
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
            User user = AutoMapper.Mapper.Map<UserViewModel, User>(userViewModel);
            User userView = userDetails.GetDetailsById(user.UserId);
            return View(userView);
        }
        [HttpPost]
        public ActionResult Update(UserViewModel userViewModel)
        {
            User user = AutoMapper.Mapper.Map<UserViewModel, User>(userViewModel);
            userDetails.Update(user);
            return RedirectToAction("ManageUser", "User");
        }
        public ActionResult Delete(UserViewModel userViewModel)
        {
            User user = AutoMapper.Mapper.Map<UserViewModel, User>(userViewModel);
            User userView = userDetails.GetDetailsById(user.UserId);
            userDetails.Delete(userView);
            return RedirectToAction("ManageUser", "User");
        }// = new ManageHotel();
    }
}
