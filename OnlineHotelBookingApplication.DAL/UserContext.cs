﻿using OnlineHotelBookingApplication.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotelBookingApplication.DAL
{
    public class UserContext : DbContext
    {
        public DbSet<User> dataset { get; set; }
        public DbSet<Hotel> HotelData { get; set; }
        public UserContext() : base("ConnectionDB")
        {

        }
    }
}
