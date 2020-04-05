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
            //        dynamic model = new ExpandoObject();
            //        model.Customers = GetHotel();
            //        model.Employees = GetRoom();
            //        return View(model);
            //    }

            //    private static List<HotelViewModel> GetHotel()
            //    {
            //        List<HotelViewModel> hotels = new List<HotelViewModel>();
            //        using (UserContext userContext = new UserContext())
            //        {
            //            var hotelList = userContext.HotelData.SqlQuery("Select * from Students").ToList<Hotel>();
            //            using (SqlDataReader sdr = cmd.ExecuteReader())
            //            {
            //                while (sdr.Read())
            //                {
            //                    customers.Add(new CustomerModel
            //                    {
            //                        CustomerId = sdr["CustomerID"].ToString(),
            //                        CustomerName = sdr["ContactName"].ToString(),
            //                        City = sdr["City"].ToString(),
            //                        Country = sdr["Country"].ToString()
            //                    });
            //                }
            //            }
            //            return customers;
            //        }
            //    }
            //}

            //private static List<HotelRoomCategory> GetRoom()
            //{
            //    List<EmployeeModel> employees = new List<EmployeeModel>();
            //    string query = "SELECT EmployeeID, (FirstName + ' ' + LastName) [Name], City, Country FROM Employees";
            //    string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
            //    using (SqlConnection con = new SqlConnection(constr))
            //    {
            //        using (SqlCommand cmd = new SqlCommand(query))
            //        {
            //            cmd.Connection = con;
            //            con.Open();
            //            using (SqlDataReader sdr = cmd.ExecuteReader())
            //            {
            //                while (sdr.Read())
            //                {
            //                    employees.Add(new EmployeeModel
            //                    {
            //                        EmployeeId = sdr["EmployeeID"].ToString(),
            //                        EmployeeName = sdr["Name"].ToString(),
            //                        City = sdr["City"].ToString(),
            //                        Country = sdr["Country"].ToString()
            //                    });
            //                }
            //                con.Close();
            //                return employees;
            //            }
            //        }
            //    }
            ////}
            //List<Hotel> list = hotelDetails.GetHotelDetails();
            //List<HotelViewModel> hotelList = new List<HotelViewModel>();
            //foreach (Hotel detail in list)
            //{
            //    HotelViewModel hotelViewModel = new HotelViewModel
            //    {
            //        HotelId = detail.HotelId,
            //        HotelName = detail.HotelName,
            //        Description = detail.Description,
            //        Street = detail.Street,
            //        State = detail.State,
            //        City = detail.City,
            //    };
            //    hotelList.Add(hotelViewModel);
            //}
            return View();
        }
    }
}