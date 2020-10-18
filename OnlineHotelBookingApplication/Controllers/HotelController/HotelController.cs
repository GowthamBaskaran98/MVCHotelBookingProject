using System;
using System.IO;
using System.Web.Mvc;
using System.Collections.Generic;
using OnlineHotelBookingApplication.BL;
using OnlineHotelBookingApplication.DAL;
using OnlineHotelBookingApplication.Entity;
using OnlineHotelBookingApplication.Models;
using OnlineHotelBookingApplication.App_Start;
using System.Speech.Recognition;
using System.Linq;
using System.Web.Services.Description;
using System.Text;

namespace OnlineHotelBookingApplication.Controllers
{
    [CustomExceptionFilter]
    public class HotelController : Controller
    {
        SpeechRecognitionEngine engine = new SpeechRecognitionEngine();
        private void Speech(object sender , EventArgs e)
        {
            Choices commands = new Choices();
            commands.Add(new string[] { "Say Hello " });
            GrammarBuilder grammerBuilder = new GrammarBuilder();
            grammerBuilder.Append(commands);
            Grammar grammer = new Grammar(grammerBuilder);
            engine.LoadGrammarAsync(grammer);
            engine.SetInputToDefaultAudioDevice();

        }
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
        public ActionResult AddHotel(HotelViewModel hotelViewModel)
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
                return RedirectToAction("AddRoomCategory", "Hotel",hotelViewModel);
            }
            //if (!string.IsNullOrEmpty(Image))
            //{
            //    var db = new UserContext();
            //    if (hotelViewModel.HotelImages.FileName != null)
            //    {
            //        Stream fs = hotelViewModel.HotelImages.InputStream;
            //        BinaryReader br = new BinaryReader(fs);
            //        bytes = br.ReadBytes((Int32)fs.Length);
            //        ImageObject image = new ImageObject();
            //        image.FileName = hotelViewModel.HotelName;
            //        image.HotelImage = bytes;
            //        image.UploadDate = DateTime.Now;
            //        hotelDetails.AddImage(image);
            //     }
            //}
            return View(hotelViewModel);
        }
        public ActionResult AddRoomCategory(HotelRoomCategoryViewModel category)
        {
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
            Hotel hotel = hotelDetails.GetHotelDetailsById(category.HotelId);
            roomViewModel.HotelViewModel = AutoMapper.Mapper.Map<Hotel, HotelViewModel>(hotel);
            return View(roomViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRoomCategory(RoomViewModel roomViewModel, HotelRoomCategoryViewModel hotelRoomCategoryViewModel, string Save)
        {
            Byte[] bytes = null;
            if (roomViewModel.RoomImages.FileName != null)
            {
                Stream fs = roomViewModel.RoomImages.InputStream;
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
            return View(roomViewModel);
        }
        public ActionResult ManageHotel(string Status, string MyCart, string Approved, string Pending, string MyHotel, string Declined, List<Hotel> data)

        {
            List<Hotel> list = new List<Hotel>();
            list = hotelDetails.GetHotelDetails(Status);
            if (TempData["data"] == null)
            {
                if (Approved != null)
                    list = hotelDetails.GetHotelDetails(Approved);
                else if (Pending != null)
                    list = hotelDetails.GetHotelDetails(Pending);
                else if (Declined != null)
                    list = hotelDetails.GetHotelDetails(Declined);
                else if (MyHotel != null)
                    list = hotelDetails.GetHotelByName(User.Identity.Name);
            }
            else
                list = (List<Hotel>)TempData["data"];
            List<HotelViewModel> hotelList = new List<HotelViewModel>();
            foreach (Hotel detail in list)
            {
                HotelViewModel hotelViewModel = AutoMapper.Mapper.Map<Hotel, HotelViewModel>(detail);
                hotelList.Add(hotelViewModel);                                                          //Displaying Hotel Details          
            }
            TimingViewModel timing = new TimingViewModel();
            HotelTimingViewModel hotelTimingViewModel = new HotelTimingViewModel();
            hotelTimingViewModel.HotelViewModels = hotelList;
            hotelTimingViewModel.TimingViewModel = timing;
            UserRepository userDetails = new UserRepository();
            User user = userDetails.GetUserDetailByName(User.Identity.Name);
            hotelTimingViewModel.UserViewModel = AutoMapper.Mapper.Map<User,UserViewModel>(user);
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
                HotelViewModel hotelViewModel;
                if (TempData["CheckIn"] != null)
                {
                    int rooms = hotelDetails.GetAvailableRoomsCount(detail.HotelId,0, Convert.ToString(TempData["CheckIn"]),Convert.ToString(TempData["CheckOut"]));
                    hotelViewModel = AutoMapper.Mapper.Map<Hotel, HotelViewModel>(detail);
                    hotelViewModel.AvailableRooms = rooms;
                }
                else
                    hotelViewModel = AutoMapper.Mapper.Map<Hotel, HotelViewModel>(detail);
                hotelList.Add(hotelViewModel);                                                          //Displaying Hotel Details          
            }
            TimingViewModel timing = new TimingViewModel();
            hotelTimingViewModel.HotelViewModels = hotelList;
            hotelTimingViewModel.TimingViewModel = timing;
            return View(hotelTimingViewModel);
        }
        public ActionResult SearchPeople(string keyword, string Approved)
        {
            System.Threading.Thread.Sleep(2000);
            UserContext user = new UserContext();
            var data = user.HotelDatabases.Where(f => f.HotelName.StartsWith(keyword)).ToList();
            TempData["data"] = data;
            return RedirectToAction("ManageHotel", "Hotel");
        }
        public ActionResult DisplayRoomType(int Hotel, HotelTimingViewModel hotelTimingViewModel)
        {
            HotelViewModel hotelViewModel = new HotelViewModel();
            ViewBag.CheckIn = TempData["CheckIn"];
            ViewBag.CheckOut = TempData["CheckOut"];
            ViewBag.Hotel = Hotel;
            hotelViewModel.HotelId = Hotel;
            TempData["HotelId"] = hotelViewModel.HotelId;
            Hotel hotel = hotelDetails.GetHotelDetailsById(hotelViewModel.HotelId);
            hotelViewModel = AutoMapper.Mapper.Map<Hotel, HotelViewModel>(hotel);
            List<HotelRoomBind> list = hotelDetails.GetRoomCategoryDetails(hotelViewModel.HotelId);
            List<HotelRoomCategoryViewModel> hotelList = new List<HotelRoomCategoryViewModel>();
            TimingViewModel Timing = new TimingViewModel();
            if ((hotelTimingViewModel.TimingViewModel) == null)
            {
                TempData["CheckIn"] = ViewBag.CheckIn;
                TempData["CheckOut"] = ViewBag.CheckOut;
                hotelTimingViewModel.TimingViewModel = Timing;
                if (TempData["CheckIn"] != null)
                {
                    hotelTimingViewModel.TimingViewModel.CheckIn = Convert.ToDateTime(TempData["CheckIn"]);
                    hotelTimingViewModel.TimingViewModel.CheckOut = Convert.ToDateTime(TempData["CheckOut"]);
                }
                else
                {
                    hotelTimingViewModel.TimingViewModel.CheckIn = DateTime.Now;
                    hotelTimingViewModel.TimingViewModel.CheckOut = DateTime.Now.AddDays(1);
                }
            }
            else
            {
                TempData["CheckIn"] = hotelTimingViewModel.TimingViewModel.CheckIn;
                TempData["CheckOut"] = hotelTimingViewModel.TimingViewModel.CheckOut;
                hotelTimingViewModel.TimingViewModel.CheckIn = Convert.ToDateTime(TempData["CheckIn"]);
                hotelTimingViewModel.TimingViewModel.CheckOut = Convert.ToDateTime(TempData["CheckOut"]);
            }
            foreach (HotelRoomBind detail in list)
            {
                HotelRoomCategoryViewModel hotelRoomCategoryViewModel;
                if (TempData["CheckIn"] != null)
                {
                    int rooms = hotelDetails.GetAvailableRoomsCount(detail.HotelId, detail.RoomCategories.RoomId, Convert.ToString(TempData["CheckIn"]), Convert.ToString(TempData["CheckOut"]));
                    hotelRoomCategoryViewModel = AutoMapper.Mapper.Map<HotelRoomBind, HotelRoomCategoryViewModel>(detail);
                    hotelRoomCategoryViewModel.AvailableRooms = rooms;
                }
                else
                {
                    hotelRoomCategoryViewModel = AutoMapper.Mapper.Map<HotelRoomBind, HotelRoomCategoryViewModel>(detail);
                }
                hotelList.Add(hotelRoomCategoryViewModel);                                              //Displaying Room Categories
            }
            TempData["Hotel"] = ViewBag.Hotel;
            hotelTimingViewModel.HotelViewModel = hotelViewModel;
            hotelTimingViewModel.HotelRoomCategoryViewModels = hotelList;
            return View(hotelTimingViewModel);
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
            Hotel hotel = new Hotel();
            Byte[] bytes = null;
            if (hotelViewModel.HotelImages != null)
            {
                Stream fs = hotelViewModel.HotelImages.InputStream;
                BinaryReader br = new BinaryReader(fs);
                bytes = br.ReadBytes((Int32)fs.Length);
                hotelViewModel.HotelImage = bytes;
            }
            else
            {
                hotel = hotelDetails.GetHotelDetailsById(hotelViewModel.HotelId);
                hotelViewModel.HotelImage = hotel.HotelImage;
            }
            hotelViewModel.Permission = "Approved";
            hotelViewModel.HotelOwner = User.Identity.Name;
            hotel = AutoMapper.Mapper.Map<HotelViewModel, Hotel>(hotelViewModel);                 //Updating Hotel Details
            hotelDetails.UpdateHotel(hotel);
            if (User.IsInRole("Admin"))
            {
                TempData["alertMessage"] = "Updated Successfully";
                return RedirectToAction("ManageHotel", "Hotel", new { Approved = "Approved" });
            }
            else
            {
                TempData["alertMessage"] = "Updated Successfully";
                return RedirectToAction("ManageHotel", "Hotel", new { MyHotel = "MyHotel" });
            }
        }
        public ActionResult DeleteHotel(HotelViewModel hotelViewModel)
        {
            Hotel hotel = AutoMapper.Mapper.Map<HotelViewModel, Hotel>(hotelViewModel);                 //Deleting Hotel Details
                                                                                                        //    hotelDetails.DeleteHotel(hotel);
            hotelDetails.DeclineHotel(hotel.HotelId);
            TempData["alertMessage"] = "Deleted Successfully";
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
            HotelRoomBind hotelRoomBind = hotelDetails.GetRoomTypeDetails(hotelRoomCategoryViewModel.HotelRoomId);
            hotelRoomCategoryViewModel = AutoMapper.Mapper.Map<HotelRoomBind, HotelRoomCategoryViewModel>(hotelRoomBind);
            return View(hotelRoomCategoryViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("EditRoomType")]
        public ActionResult UpdateRoomType([Bind(Exclude = "RoomCategory,Hotel")]HotelRoomCategoryViewModel hotelRoomCategoryViewModel)
        {
            Byte[] bytes = null;
            HotelRoomBind hotelRoomBind = new HotelRoomBind();
            if (hotelRoomCategoryViewModel.RoomImages != null)
            {
                Stream fs = hotelRoomCategoryViewModel.RoomImages.InputStream;
                BinaryReader br = new BinaryReader(fs);
                bytes = br.ReadBytes((Int32)fs.Length);
                hotelRoomCategoryViewModel.RoomImage = bytes;
            }
            else
            {
                hotelRoomBind = hotelDetails.GetRoomCategoryDetail(hotelRoomCategoryViewModel.HotelRoomId);
                hotelRoomCategoryViewModel.RoomImage = hotelRoomBind.RoomImage;
            }
            hotelRoomCategoryViewModel.UploadDate = DateTime.Now;
            hotelRoomBind = AutoMapper.Mapper.Map<HotelRoomCategoryViewModel, HotelRoomBind>(hotelRoomCategoryViewModel);                 //Updating Hotel Details
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
            return RedirectToAction("AddRoomCategory", "Hotel");
        }
        public ActionResult BookHotel(HotelTimingViewModel hotelTimingViewModel, HotelRoomCategoryViewModel hotelRoom)
        {
            ViewBag.A = TempData["CheckIn"];
            ViewBag.B = TempData["CheckOut"];
            TempData["CheckIn"] = ViewBag.A;
            TempData["CheckOut"] = ViewBag.B;
            hotelTimingViewModel.BookViewModel = new BookViewModel();
            hotelTimingViewModel.BookViewModel.HotelRoomId = hotelRoom.HotelRoomId;
            if (TempData["CheckIn"] != null)
            {
                hotelTimingViewModel.BookViewModel.CheckIn = Convert.ToDateTime(TempData["CheckIn"]); //DateTime.Parse(a);
                hotelTimingViewModel.BookViewModel.CheckOut = Convert.ToDateTime(TempData["CheckIn"]); //DateTime.Parse(b);
            }
            hotelTimingViewModel.BookViewModel.UserId = HttpContext.User.Identity.Name;
            HotelRoomBind hotelRooms = hotelDetails.GetRoomCategoryDetail(hotelRoom.HotelRoomId);
            Hotel hotel = hotelDetails.GetHotelDetailsById(hotelRooms.HotelId); 
            hotelTimingViewModel.HotelViewModel = AutoMapper.Mapper.Map<Hotel, HotelViewModel>(hotel);
            hotelTimingViewModel.HotelRoomCategoryViewModel = AutoMapper.Mapper.Map<HotelRoomBind, HotelRoomCategoryViewModel>(hotelRooms);
            hotelTimingViewModel.HotelRoomCategoryViewModel.RoomCategories = new RoomCategory();
            hotelTimingViewModel.HotelRoomCategoryViewModel.RoomCategories = (hotelDetails.GetCategoryById(hotelTimingViewModel.HotelRoomCategoryViewModel.RoomId));
            return View(hotelTimingViewModel);
        }
        [HttpPost]
        [ActionName("BookHotel")]
        public ActionResult BookingHotel(HotelTimingViewModel bookHotelViewModel)
        {
            BookHotel bookHotel = AutoMapper.Mapper.Map<BookViewModel, BookHotel>(bookHotelViewModel.BookViewModel);
            hotelDetails.UpdateRoomCount(bookHotelViewModel.BookViewModel.HotelRoomId);
            bookHotel.CheckIn = bookHotelViewModel.TimingViewModel.CheckIn;
            bookHotel.CheckOut = bookHotelViewModel.TimingViewModel.CheckOut;
            bookHotel.Gmail = bookHotelViewModel.BookViewModel.UserId;
            hotelDetails.BookHotel(bookHotel);
            Referral referral = hotelDetails.GetReferrerDetail(bookHotel.Gmail);
            if (referral != null)
                hotelDetails.Reward(referral.ReferrerId);
            TempData["alertMessage"] = "Booked Successfully";
            return RedirectToAction("ManageHotel", "Hotel", new { Approved = "Approved" });
        }
        public ActionResult AcceptRequest(HotelViewModel hotelViewModel, string Pending, string Declined)
        {
            hotelDetails.AcceptHotel(hotelViewModel.HotelId);
            if(Pending == "Pending")
                return RedirectToAction("ManageHotel", "Hotel", new { Pending = "Pending" });
            else
                return RedirectToAction("ManageHotel", "Hotel", new { Declined = "Declined" });
        }
        public ActionResult DeclineRequest(HotelViewModel hotelViewModel)
        {
            hotelDetails.DeclineHotel(hotelViewModel.HotelId);
            return RedirectToAction("ManageHotel", "Hotel", new { Pending = "Pending" });
        }
        public ActionResult Referal()
        {
            UserRepository userDetails = new UserRepository();
            List<ReferralViewModel> referralViewModels = new List<ReferralViewModel>();
            if (User.Identity.Name == null)
                return RedirectToAction("SignIn","User");
            List<Referral> referrals = hotelDetails.GetCandidateDetails(User.Identity.Name);
            foreach (var referral in referrals)
            {
                ReferralViewModel referralViewModel = AutoMapper.Mapper.Map<Referral, ReferralViewModel>(referral);
                referralViewModels.Add(referralViewModel);
            }
            UserReferralViewModel userReferralViewModel = new UserReferralViewModel();
            User user = userDetails.GetUserDetailByName(User.Identity.Name);
            userReferralViewModel.UserViewModel = AutoMapper.Mapper.Map<User, UserViewModel>(user);
            userReferralViewModel.ReferralViewModels = referralViewModels;
            return View(userReferralViewModel);
        }
    }
}