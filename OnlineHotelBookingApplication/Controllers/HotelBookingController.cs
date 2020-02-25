using OnlineHotelBookingApplication.Entity;
using OnlineHotelBookingApplication.DAL;
using System.Web.Mvc;
using System.Collections.Generic;
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
        //public ActionResult SignUp([Bind(Include = "firstName")]User user)
        public ActionResult SignUp([Bind(Exclude = "password")]User user)
        {
            if (ModelState.IsValid)
            {
                repository.Add(user);
                return RedirectToAction("Portal");
            }
            return View();
        }
        public ActionResult Portal()
        {
            IEnumerable<User> details = repository.Display();
            ViewBag.store = details;
            return View();
        }
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn([Bind(Exclude =("password"))] User user)
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
            IEnumerable<User> list = Repository.Demo();
            return View();
        }
    }
}