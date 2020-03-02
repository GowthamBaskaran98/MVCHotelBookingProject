﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace OnlineHotelBookingApplication.Entity
{
    public enum UserType
    {
        HotelOwner,
        Customer
    }
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        [MaxLength(30)]
        //[RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        [MaxLength(30)]
        //[RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Mobile Number is required.")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        [Index(IsUnique = true)]
        public long MobileNumber { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        //[RegularExpression("^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@" + "[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "Enter a valid email id.")]
        public string Gmail { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        public string Password { get; set; }
        //[Required]
        public string UserType { get; set; }
        //public User(int UserId, string FirstName, string LastName, long MobileNumber, string Gmail, string Password, string UserType)
        //{
        //    this.UserId = UserId;
        //    this.FirstName = FirstName;
        //    this.LastName = LastName;
        //    this.MobileNumber = MobileNumber;
        //    this.Gmail = Gmail;
        //    this.Password = Password;
        //}
        //public User(string Gmail, string Password)
        //{
        //    this.Gmail = Gmail;
        //    this.Password = Password;
        //}
        public User()
        {

        }
    }
}
