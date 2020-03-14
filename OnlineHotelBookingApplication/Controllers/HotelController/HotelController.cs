using System.Web.Mvc;
using OnlineHotelBookingApplication.Entity;
using System.Collections.Generic;
using OnlineHotelBookingApplication.Models;
using OnlineHotelBookingApplication.BL;
using OnlineHotelBookingApplication.DAL;
using System.Web;
using System;
using System.IO;

namespace OnlineHotelBookingApplication.Controllers
{
    public class HotelController : Controller
    {
        ManageUser userDetails = new ManageUser();
        public ActionResult AdminPage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminPage(HotelViewModel hotelViewModel)
        {
            return RedirectToAction("ManageHotel","Hotel");
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
            return RedirectToAction("ManageUser", "Hotel");
        }
        public ActionResult Delete(UserViewModel userViewModel)
        {
            UserContext userContext = new UserContext();
            User user = AutoMapper.Mapper.Map<UserViewModel, User>(userViewModel);
            User userView = userDetails.GetDetailsById(user.UserId);
            userDetails.Delete(userView);
            return RedirectToAction("ManageUser", "Hotel");
        }
        //
        ManageHotel hotelDetails = new ManageHotel();
        // GET: HotelDetails
        public ActionResult AddRoomCategory()
        {
            HotelRoomCategoryViewModel category = new HotelRoomCategoryViewModel();
            category.HotelId = (int)TempData["HotelId"];
            List<RoomCategory> roomCategory = hotelDetails.GetCategory();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (RoomCategory detail in roomCategory)
            {
                categoryList.Add(new SelectListItem { Text = @detail.RoomType, Value = @detail.RoomId.ToString()});
            }
            ViewBag.list = categoryList;
            return View(category);
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
                TempData["HotelId"] = hotel.HotelId;
                return RedirectToAction("ManageHotel", "Hotel");
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
        public ActionResult DisplayRoomType(int HotelId)
        {
            List<HotelRoomCategory> list = hotelDetails.GetRoomCategoryDetails(HotelId);
            List<HotelRoomCategoryViewModel> hotelList = new List<HotelRoomCategoryViewModel>();
            foreach (HotelRoomCategory detail in list)
            {
                HotelRoomCategoryViewModel hotelRoomCategoryViewModel = new HotelRoomCategoryViewModel
                {
                    HotelId = detail.HotelId,
                    RoomId = detail.RoomId,
                    AvailableRooms = detail.AvailableRooms,
                    Cost = detail.Cost,
                };
                hotelList.Add(hotelRoomCategoryViewModel);
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
                var db = new UserContext();
                Byte[] bytes = null;
                if (hotelViewModel.HotelImage.FileName!=null)
                {
                    Stream fs = hotelViewModel.HotelImage.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    bytes = br.ReadBytes((Int32)fs.Length);
                    //byte[] uploadedFile = new byte[hotelViewModel.FileAttach.InputStream.Length];
                    //hotelViewModel.FileAttach.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
                    //hotelViewModel.fileContent = Convert.ToBase64String(uploadedFile);
                    //string fileContentType = hotelViewModel.FileAttach.ContentType;
                    //.sp_insert_file(hotelViewModel.FileAttach.FileName, fileContentType, fileContent);

                    //hotelViewModel.HotelImage = new byte[HotelImage.ContentLength];
                    //HotelImage.InputStream.Read(hotelViewModel.HotelImage, 0, HotelImage.ContentLength);
                    ImageObject image = new ImageObject();
                    image.FileName = hotelViewModel.HotelName;
                    image.HotelImage = bytes;
                    image.UploadDate = DateTime.Now;
                    hotelDetails.AddImage(image);
                    ViewBag.Image = ViewImage(bytes);
                }
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
                return RedirectToAction("AddRoomCategory", "Hotel");
            }
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
        [ActionName("EditHotel")]
        public ActionResult Update([Bind(Exclude = "RoomCategory")]HotelViewModel hotelViewModel)
        {
            Hotel hotel = AutoMapper.Mapper.Map<HotelViewModel, Hotel>(hotelViewModel);
            hotelDetails.UpdateHotel(hotel);
            return RedirectToAction("ManageHotel", "Hotel");
        }
        public ActionResult DeleteHotel(HotelViewModel hotelViewModel)
        {
            UserContext userContext = new UserContext();
            Hotel hotel = AutoMapper.Mapper.Map<HotelViewModel, Hotel>(hotelViewModel);
            hotelDetails.DeleteHotel(hotel);
            return RedirectToAction("ManageHotel", "Hotel");
        }
        private string ViewImage(byte[] arrayImage)
        {
            string base64String = Convert.ToBase64String(arrayImage, 0, arrayImage.Length);
            return "data:image/png;base64," + base64String;
        }
    }
}
