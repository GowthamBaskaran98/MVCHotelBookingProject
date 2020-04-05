using System.Web;
using System.Collections.Generic;
using OnlineHotelBookingApplication.Entity;
using System.ComponentModel.DataAnnotations;

namespace OnlineHotelBookingApplication.Models
{
    public class ImageObjectViewModel
    { 
        [Required]
        [Display(Name = "Upload File")]
        public HttpPostedFileBase[] FileAttach { get; set; }
        public List<ImageObject> ImageList { get; set; }
    }
}