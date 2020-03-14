using OnlineHotelBookingApplication.BL;
using OnlineHotelBookingApplication.Entity;
using OnlineHotelBookingApplication.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnlineHotelBookingApplication.Controllers
{
    public class CustomerController : Controller
    {
        ManageHotel hotelDetails = new ManageHotel();
        public ActionResult CustomerPage()
        {
            List<Hotel> list = hotelDetails.GetHotelDetails();
            List<HotelViewModel> hotelList = new List<HotelViewModel>();
            foreach (Hotel detail in list)
            {
                HotelViewModel hotelViewModel = new HotelViewModel
                {
                    HotelId = detail.HotelId,
                    HotelName = detail.HotelName,
                    HotelAddress = detail.HotelAddress,
                    Services = detail.Services,
                };
                hotelList.Add(hotelViewModel);
            }
            return View(hotelList);
        }
    }
}
