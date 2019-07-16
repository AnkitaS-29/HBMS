using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HBMS.Models;

namespace HBMS.Controllers
{
    public class CustomerController : Controller
    {
        private HotelsEntities db = new HotelsEntities();

        // GET: HotelEmployee
        public ActionResult MainContent()
        {
            return View();
        }



        // GET: HotelEmployee/Create
        public ActionResult BookHotel()
        {
            ViewBag.RoomID = new SelectList(db.RoomDetails180625, "RoomID", "RoomNo");
            ViewBag.UserID = new SelectList(db.Users180625, "UserID", "Password");
            return View();
        }
        public ActionResult SearchbyId(string id)
        {
            RoomDetails180625 room = new RoomDetails180625();
            return View(db.RoomDetails180625.Where(x => x.HotelID.ToString() == id || id == null).ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookHotel([Bind(Include = "BookingID,RoomID,UserID,Booked_From,Booked_To,No_of_Adults,No_of_Children,Amount")] BookingDetails180625 BookingDetails180625)
        {
            if (ModelState.IsValid)
            {
                db.BookingDetails180625.Add(BookingDetails180625);
                db.SaveChanges();
                return RedirectToAction("MainContent");
            }

            ViewBag.UserID = new SelectList(db.Users180625, "UserID", "Password", BookingDetails180625.UserID);
            ViewBag.RoomID = new SelectList(db.RoomDetails180625, "RoomID", "RoomNo", BookingDetails180625.RoomID);
            return View(BookingDetails180625);
        }

        //[HttpPost]
        //public ActionResult BookHotel(int RoomID, int UserID, DateTime Booked_From, DateTime Booked_To, int No_of_Adults, int No_of_Children)
        //{

        //    BookingDetails180625 hotel = new BookingDetails180625();
        //    hotel.RoomID = RoomID;
        //    hotel.UserID = UserID;
        //    hotel.Booked_From = Booked_From;
        //    hotel.Booked_To = Booked_To;
        //    hotel.No_of_Adults = No_of_Adults;
        //    hotel.No_of_Children = No_of_Children;

        //    db.BookingDetails180625.Add(hotel);
        //    db.SaveChanges();
        //    ViewBag.Message = "Successfully Added";
        //    return RedirectToAction("MainContent");

        //}


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
