using System;
using System.IO;
using System.Web.Mvc;
using System.Collections.Generic;
using OnlineHotelBookingApplication.BL;
using OnlineHotelBookingApplication.DAL;
using OnlineHotelBookingApplication.Entity;
using OnlineHotelBookingApplication.Models;
using OnlineHotelBookingApplication.App_Start;
using System.Linq;

namespace OnlineHotelBookingApplication.Controllers
{
    [CustomExceptionFilter]
    public class HotelController : Controller
    {
        IManageHotel hotelDetails;
        public HotelController()
        {
            hotelDetails = new ManageHotel();
        }
        public ActionResult AdminPage()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminPage(HotelViewModel hotelViewModel)
        {
            return RedirectToAction("ManageHotel", "Hotel");
        }
        [HttpPost]
        public JsonResult Search(string Prefix)
        {
            //Note : you can bind same list from database  
            List<Hotel> hotelList = hotelDetails.GetHotelDetails("Approved");
            //Searching records from list using LINQ query  
            var HotelList = (from N in hotelList where N.HotelName.StartsWith(Prefix) select new { N.HotelName });
            return Json(HotelList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddRoomCategory()
        {
            HotelRoomCategoryViewModel category = new HotelRoomCategoryViewModel();
            category.HotelId = (int)TempData["HotelId"];
            List<RoomCategory> roomCategory = hotelDetails.GetCategory();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (RoomCategory detail in roomCategory)                           //For Getting list of Categories
            {
                categoryList.Add(new SelectListItem { Text = @detail.RoomType, Value = @detail.RoomId.ToString() });
            }
            ViewBag.list = categoryList;    
            List<HotelRoomBind> list = hotelDetails.GetRoomCategoryDetails(category.HotelId);
            List<HotelRoomCategoryViewModel> hotelRoomList = new List<HotelRoomCategoryViewModel>();
            foreach (HotelRoomBind detail in list)
            {
                HotelRoomCategoryViewModel hotelRoomCategoryViewModel = AutoMapper.Mapper.Map<HotelRoomBind, HotelRoomCategoryViewModel>(detail);
                hotelRoomList.Add(hotelRoomCategoryViewModel);
            }
            RoomViewModel roomViewModel = new RoomViewModel();
            roomViewModel.HotelRoomCategoryViewModel = category;
            roomViewModel.HotelRoomCategoryViewModels = hotelRoomList;
            //return View(hotelViewModel);
            return View(roomViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRoomCategory(RoomViewModel roomViewModel, HotelRoomCategoryViewModel hotelRoomCategoryViewModel, string Save)
        {
            Byte[] bytes = null;
            if (roomViewModel.HotelRoomCategoryViewModel.RoomImages.FileName != null)
            {
                Stream fs = roomViewModel.HotelRoomCategoryViewModel.RoomImages.InputStream;
                BinaryReader br = new BinaryReader(fs);
                bytes = br.ReadBytes((Int32)fs.Length);
                roomViewModel.HotelRoomCategoryViewModel.RoomImage = bytes;
                roomViewModel.HotelRoomCategoryViewModel.UploadDate = DateTime.Now;
            }
            TempData["HotelId"] = roomViewModel.HotelRoomCategoryViewModel.HotelId;
            List<RoomCategory> list = hotelDetails.GetCategory();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (RoomCategory details in list)
            {
                categoryList.Add(new SelectListItem { Text = @details.RoomType, Value = @details.RoomId.ToString() });
            }
            ViewBag.List = categoryList;
            if (ModelState.IsValid)
            {
                HotelRoomBind hotel = AutoMapper.Mapper.Map<HotelRoomCategoryViewModel, HotelRoomBind>(roomViewModel.HotelRoomCategoryViewModel);
                hotelDetails.AddRoomCategoryForHotel(hotel);                                            //Adding RoomCategories
                TempData["HotelId"] = hotel.HotelId;
                if (Save != null)
                {
                    if (User.IsInRole("Admin"))
                        return RedirectToAction("ManageHotel", "Hotel", new { Approved = "Approved" });
                    else
                        return RedirectToAction("ManageHotel", "Hotel", new { MyHotel = "MyHotel" });
                }
                return RedirectToAction("AddRoomCategory", "Hotel");
            }
            if (Save != null)
            {
                if (User.IsInRole("Admin"))
                    return RedirectToAction("ManageHotel", "Hotel", new { Approved = "Approved" });
                else
                    return RedirectToAction("ManageHotel", "Hotel", new { MyHotel = "MyHotel" });
            }
            return View(roomViewModel);
        }
        public ActionResult ManageHotel(string Status, string Approved, string Pending, string MyHotel, string Declined)
        {
            List<Hotel> list = new List<Hotel>();
            list = hotelDetails.GetHotelDetails(Status);
            if (Approved != null)
                list = hotelDetails.GetHotelDetails(Approved);
            if (Pending != null)
                list = hotelDetails.GetHotelDetails(Pending);
            if (Pending != null)
                list = hotelDetails.GetHotelDetails(Status);
            if (MyHotel != null)
                list = hotelDetails.GetHotelByName(User.Identity.Name);
            List<HotelViewModel> hotelList = new List<HotelViewModel>();
            foreach (Hotel detail in list)
            {
                //HotelViewModel hotelViewModel = new HotelViewModel
                //{
                //    HotelId = detail.HotelId,
                //    HotelName = detail.HotelName,
                //    Description = detail.Description,
                //    Street = detail.Street,
                //    City = detail.City,
                //    State = detail.State,
                //};
                HotelViewModel hotelViewModel = AutoMapper.Mapper.Map<Hotel, HotelViewModel>(detail);
                hotelList.Add(hotelViewModel);                                                          //Displaying Hotel Details          
            }
            TimingViewModel timing = new TimingViewModel();
            HotelTimingViewModel hotelTimingViewModel = new HotelTimingViewModel();
            hotelTimingViewModel.HotelViewModels = hotelList;
            hotelTimingViewModel.TimingViewModel = timing;
            return View(hotelTimingViewModel);
        }
        [HttpPost]
        public ActionResult ManageHotel(HotelTimingViewModel hotelTimingViewModel, string Status)
        {
            TempData["CheckIn"] = hotelTimingViewModel.TimingViewModel.CheckIn;
            TempData["CheckOut"] = hotelTimingViewModel.TimingViewModel.CheckOut;
            TempData.Keep();
            List<Hotel> list = hotelDetails.GetHotelDetails(Status);
            List<HotelViewModel> hotelList = new List<HotelViewModel>();
            foreach (Hotel detail in list)
            {
                HotelViewModel hotelViewModel = AutoMapper.Mapper.Map<Hotel, HotelViewModel>(detail);
                hotelList.Add(hotelViewModel);                                                          //Displaying Hotel Details          
            }
            TimingViewModel timing = new TimingViewModel();
            hotelTimingViewModel.HotelViewModels = hotelList;
            hotelTimingViewModel.TimingViewModel = timing;
            return View(hotelTimingViewModel);
        }
        public ActionResult DisplayRoomType(HotelViewModel hotelViewModel)
        {
            HotelTimingViewModel hotelTimingViewModel = new HotelTimingViewModel();
            ViewBag.CheckIn = TempData["CheckIn"];
            ViewBag.CheckOut = TempData["CheckOut"];    
            TempData["HotelId"] = hotelViewModel.HotelId;
            Hotel hotel = hotelDetails.GetHotelDetailsById(hotelViewModel.HotelId);
            hotelViewModel = AutoMapper.Mapper.Map<Hotel, HotelViewModel>(hotel);
            List<HotelRoomBind> list = hotelDetails.GetRoomCategoryDetails(hotelViewModel.HotelId);
            List<HotelRoomCategoryViewModel> hotelList = new List<HotelRoomCategoryViewModel>();
            foreach (HotelRoomBind detail in list)
            {
                HotelRoomCategoryViewModel hotelRoomCategoryViewModel = AutoMapper.Mapper.Map<HotelRoomBind, HotelRoomCategoryViewModel>(detail);
                hotelList.Add(hotelRoomCategoryViewModel);                                              //Displaying Room Categories
            }
            TempData["CheckIn"] = ViewBag.CheckIn;
            TempData["CheckOut"] = ViewBag.CheckOut;
            return View(hotelList);
        }
        public ActionResult AddHotel()
        {
            List<RoomCategory> roomCategory = hotelDetails.GetCategory();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (RoomCategory detail in roomCategory)
            {
                categoryList.Add(new SelectListItem { Text = @detail.RoomType, Value = @detail.RoomId.ToString() });
            }   
            ViewBag.List = categoryList;
            ViewBag.RoomCategory = new SelectList(hotelDetails.GetCategory(), "RoomId", "RoomType");
            HotelViewModel hotelViewModel = new HotelViewModel();
            hotelViewModel.HotelOwner = User.Identity.Name;
            return View(hotelViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddHotel(HotelViewModel hotelViewModel, string Image)
        {
            Byte[] bytes = null;
            if (hotelViewModel.HotelImages.FileName != null)
            {
                Stream fs = hotelViewModel.HotelImages.InputStream;
                BinaryReader br = new BinaryReader(fs);
                bytes = br.ReadBytes((Int32)fs.Length);
                hotelViewModel.HotelImage = bytes;
                hotelViewModel.UploadDate = DateTime.Now;
            }
            if (ModelState.IsValid)
            {
                if (User.IsInRole("Admin"))
                    hotelViewModel.Permission = "Approved";
                else
                    hotelViewModel.Permission = "Pending";
                Hotel hotel = AutoMapper.Mapper.Map<HotelViewModel, Hotel>(hotelViewModel);
                hotelDetails.AddHotel(hotel);                                                   //Adding Hotel Details
                TempData["HotelId"] = hotel.HotelId;
                return RedirectToAction("AddRoomCategory", "Hotel", hotelViewModel);
            }
            if (!string.IsNullOrEmpty(Image))
            {
                var db = new UserContext();
                if (hotelViewModel.HotelImages.FileName != null)
                {
                    Stream fs = hotelViewModel.HotelImages.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    bytes = br.ReadBytes((Int32)fs.Length);
                    ImageObject image = new ImageObject();
                    image.FileName = hotelViewModel.HotelName;
                    image.HotelImage = bytes;
                    image.UploadDate = DateTime.Now;
                    hotelDetails.AddImage(image);
                    //hotelDetails.GetImagesByName(hotelViewModel.HotelName);
                    ViewBag.Image = ViewImage(bytes);
                }
                ViewBag.Message = "The operation was cancelled!";
            }
            return View(hotelViewModel);
        }
        [HttpGet]
        public ActionResult EditHotel(HotelViewModel hotelViewModel)
        {
            Hotel hotel = AutoMapper.Mapper.Map<HotelViewModel, Hotel>(hotelViewModel);
            Hotel hotelView = hotelDetails.GetHotelDetailsById(hotel.HotelId);                          //Editing Hotel Details
            ViewBag.Image = ViewImage(hotelView.HotelImage);
            return View(hotelView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("EditHotel")]
        public ActionResult Update([Bind(Exclude = "RoomCategory")]HotelViewModel hotelViewModel)
        {
            Byte[] bytes = null;
            if (hotelViewModel.HotelImages != null)
            {
                Stream fs = hotelViewModel.HotelImages.InputStream;
                BinaryReader br = new BinaryReader(fs);
                bytes = br.ReadBytes((Int32)fs.Length);
                hotelViewModel.HotelImage = bytes;
            }
            hotelViewModel.Permission = "Approved";
            hotelViewModel.HotelOwner = User.Identity.Name;
            Hotel hotel = AutoMapper.Mapper.Map<HotelViewModel, Hotel>(hotelViewModel);                 //Updating Hotel Details
            hotelDetails.UpdateHotel(hotel);
            if (User.IsInRole("Admin"))
                return RedirectToAction("ManageHotel", "Hotel", new { Approved = "Approved" });
            else
                return RedirectToAction("ManageHotel", "Hotel", new { MyHotel = "MyHotel" });
        }
        public ActionResult DeleteHotel(HotelViewModel hotelViewModel)
        {
            Hotel hotel = AutoMapper.Mapper.Map<HotelViewModel, Hotel>(hotelViewModel);                 //Deleting Hotel Details
        //    hotelDetails.DeleteHotel(hotel);
            hotelDetails.DeclineHotel(hotel.HotelId);
            return RedirectToAction("ManageHotel", "Hotel");
        }
        private string ViewImage(byte[] arrayImage)
        {
            return "";
        }
        public ActionResult EditRoomType(HotelRoomCategoryViewModel hotelRoomCategoryViewModel)
        {
            List<RoomCategory> roomCategory = hotelDetails.GetCategory();
            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (RoomCategory detail in roomCategory)                           //For Getting list of Categories
            {
                categoryList.Add(new SelectListItem { Text = @detail.RoomType, Value = @detail.RoomId.ToString() });
            }
            ViewBag.list = categoryList;
            return View(hotelRoomCategoryViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("EditRoomType")]
        public ActionResult UpdateRoomType([Bind(Exclude = "RoomCategory,Hotel")]HotelRoomCategoryViewModel hotelRoomCategoryViewModel)
        {
            HotelRoomBind hotelRoomBind = AutoMapper.Mapper.Map<HotelRoomCategoryViewModel, HotelRoomBind>(hotelRoomCategoryViewModel);                 //Updating Hotel Details
            hotelDetails.UpdateRoomType(hotelRoomBind);
            if (User.IsInRole("Admin"))
                return RedirectToAction("ManageHotel", "Hotel", new { Approved = "Approved" });
            else
                return RedirectToAction("ManageHotel", "Hotel", new { MyHotel = "MyHotel" });
        }
        public ActionResult DeleteRoomType(HotelRoomCategoryViewModel hotelRoomCategoryViewModel)
        {
            HotelRoomBind hotelRoomBind = AutoMapper.Mapper.Map<HotelRoomCategoryViewModel, HotelRoomBind>(hotelRoomCategoryViewModel);                 //Deleting Hotel Details
            hotelDetails.DeleteRoomType(hotelRoomBind.HotelRoomId);
            return RedirectToAction("ManageHotel", "Hotel");
        }
        public ActionResult AcceptRequest(HotelViewModel hotelViewModel)
        {
            hotelDetails.AcceptHotel(hotelViewModel.HotelId);
            return RedirectToAction("ManageHotel", "Hotel", new { Pending = "Pending" });
        }
        public ActionResult DeclineRequest(HotelViewModel hotelViewModel)
        {
            hotelDetails.DeclineHotel(hotelViewModel.HotelId);
            return RedirectToAction("ManageHotel", "Hotel", new { Pending = "Pending" });
        }
    }
}

