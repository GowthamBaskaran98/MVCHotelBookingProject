using OnlineHotelBookingApplication.BL;
using OnlineHotelBookingApplication.DAL;
using OnlineHotelBookingApplication.Entity;
using OnlineHotelBookingApplication.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnlineHotelBookingApplication.Controllers
{
    public class HotelDetailsController : Controller
    {
        ManageHotel hotelDetails = new ManageHotel();
        // GET: HotelDetails
        public ActionResult AddRoomCategory()
        {
            HotelRoomCategoryViewModel category = new HotelRoomCategoryViewModel();
            //category.HotelRoomCategoryId = (int)TempData["HotelRoomCategoryId"];
            List<RoomCategory> roomCategory = hotelDetails.GetCategory();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (RoomCategory detail in roomCategory)
            {
                categoryList.Add(new SelectListItem { Text = @detail.RoomType, Value = @detail.RoomId.ToString() });
            }
            ViewBag.List = categoryList;
            return View();
        }
        [HttpPost]
        public ActionResult AddRoomCategory(HotelRoomCategoryViewModel hotelRoomCategory)
        {
            List<RoomCategory> list = hotelDetails.GetCategory();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (RoomCategory details in list)
            {
                categoryList.Add(new SelectListItem { Text = @details.RoomType, Value = @details.RoomId.ToString() });
            }
            if (ModelState.IsValid)
            {
                HotelRoomCategory hotel = AutoMapper.Mapper.Map<HotelRoomCategoryViewModel, HotelRoomCategory>(hotelRoomCategory);
                hotelDetails.AddRoomCategoryForHotel(hotel);
                return RedirectToAction("ManageHotel", "Admin");
            }
            //ViewBag.RoomCategory = new SelectList(HotelRepository.GetCategories(), "RoomId", "RoomType");
            return View();
        }

        //public ActionResult ManageUser()
        //{
        //    Details details = new Details();
        //    List<User> list = details.GetUserDetails();
        //    List<UserViewModel> userList = new List<UserViewModel>();
        //    foreach (User detail in list)
        //    {
        //        UserViewModel userViewModel = new UserViewModel
        //        {
        //            UserId = detail.UserId,
        //            FirstName = detail.FirstName,
        //            LastName = detail.LastName,
        //            MobileNumber = detail.MobileNumber,
        //            Gmail = detail.Gmail,
        //            Password = detail.Password,
        //            UserType = detail.UserType
        //        };
        //        userList.Add(userViewModel);
        //    }
        //    return View(userList);
        //}
        public ActionResult ManageHotel()
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
        public ActionResult AddHotel()
        {
            ViewBag.RoomCategory = new SelectList(HotelRepository.GetCategories(), "RoomId", "RoomType");
            return View();
        }
        [HttpPost]
        public ActionResult AddHotel(HotelViewModel hotelViewModel)
        {
            if (ModelState.IsValid)
            {
                Hotel hotel = AutoMapper.Mapper.Map<HotelViewModel, Hotel>(hotelViewModel);
                //hotel.HotelRoomCategories = new List<HotelRoomCategory>();
                //foreach(int data in hotelViewModel.RoomCategory)
                //{
                //    HotelRoomCategory hotelRoomCategory = new HotelRoomCategory();
                //    hotelRoomCategory.RoomId = data;
                //    hotel.HotelRoomCategories.Add(hotelRoomCategory);
                //}
                hotelDetails.AddHotel(hotel);
                TempData["HotelId"] = hotel.HotelId;
                return RedirectToAction("AddRoomCategory", "HotelDetails");
            }
            //ViewBag.RoomCategory = new SelectList(HotelRepository.GetCategories(),"RoomId","RoomType");
            return View(hotelViewModel);
        }
        [HttpGet]
        public ActionResult EditHotel(HotelViewModel hotelViewModel)
        {
            Hotel hotel = AutoMapper.Mapper.Map<HotelViewModel, Hotel>(hotelViewModel);
            Hotel hotelView = hotelDetails.GetHotelDetailsById(hotel.HotelId);
            return View(hotelView);
        }
        [HttpPost]
        public ActionResult Update([Bind(Exclude = "RoomCategory")]HotelViewModel hotelViewModel)
        {
            Hotel hotel = AutoMapper.Mapper.Map<HotelViewModel, Hotel>(hotelViewModel);
            hotelDetails.UpdateHotel(hotel);
            return RedirectToAction("ManageHotel", "HotelDetails");
        }
        public ActionResult DeleteHotel(HotelViewModel hotelViewModel)
        {
            UserContext userContext = new UserContext();
            Hotel hotel = AutoMapper.Mapper.Map<HotelViewModel, Hotel>(hotelViewModel);
            Hotel hotelView = hotelDetails.GetHotelDetailsById(hotel.HotelId);
            hotelDetails.DeleteHotel(hotelView);
            return RedirectToAction("ManageHotel", "HotelDetails");
        }
    }
}
