using OnlineHotelBookingApplication.Entity;
using OnlineHotelBookingApplication.DAL;
using System.Web.Mvc;
using System;

namespace OnlineHotelBookingApplication.Controllers
{
    public class HotelBookingController : Controller
    {
        Repository repository = new Repository();
        // GET: OnlineLogisticsRegistration
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User user)
        {
            if (ModelState.IsValid)
            {
                TryUpdateModel<User>(user);
                repository.Add(user);
            }
            return View();// Redirect("LogIn");

        }
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(User user)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("SignUp");
            }
            return View();
        }
        public ActionResult back()
        {
            return PartialView();
        }
        public ActionResult HomePage()
        {
            return View();
        }
    }
}