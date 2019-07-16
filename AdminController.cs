using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HBMS.Models;

namespace HBMS.Controllers
{
    public class AdminController : Controller
    {
        private HotelsEntities db = new HotelsEntities();

        // GET: Admin
        public ActionResult MainContent()
        {
            return View();

        }
        public ActionResult ViewReports()
        {
            return View();
        }
        public ActionResult DetailofUser(string id)
        {
            List<Users180625> userlist = db.Users180625.ToList();
            return View(userlist);
        }
        // GET: Admin/Details/5
        public ActionResult Details(string id)
        {
            List<Hotels180625> hotellist = db.Hotels180625.ToList();
            return View(hotellist);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string City, string HotelName, string Address, string Description, int Avg_Rate_Per_Night, string PhoneNo1, string PhoneNo2, string Rating, string Email, string Fax)
        {
            Hotels180625 hotel = new Hotels180625();

            hotel.HotelName = HotelName;
            hotel.City = City;
            hotel.Address = Address;
            hotel.Description = Description;
            hotel.Avg_Rate_Per_Night = Avg_Rate_Per_Night;
            hotel.PhoneNo1 = PhoneNo1;
            hotel.PhoneNo2 = PhoneNo2;
            hotel.Rating = Rating;
            hotel.Email = Email;
            hotel.Fax = Fax;
            db.Hotels180625.Add(hotel);
            db.SaveChanges();
            ViewBag.Message = "Successfully Added";
            return RedirectToAction("Details");

        }
        //GET: Admin/Edit/5

        public ActionResult Edit(int? id)
        {
            Hotels180625 hotel = new Hotels180625();
            hotel = db.Hotels180625.Find(id);
            return View(hotel);

        }


        [HttpPost]
        public ActionResult Edit(Hotels180625 hotel)
        {
            var std = db.Hotels180625.Where(x => x.HotelID == hotel.HotelID).FirstOrDefault();
            std.HotelID = hotel.HotelID;
            std.City = hotel.City;
            std.HotelName = hotel.HotelName;
            std.Address = hotel.Address;
            std.Description = hotel.Description;
            std.Avg_Rate_Per_Night = hotel.Avg_Rate_Per_Night;
            std.PhoneNo1 = hotel.PhoneNo1;
            std.PhoneNo2 = hotel.PhoneNo2;
            std.Rating = hotel.Rating;
            std.Fax = hotel.Fax;

            if (TryUpdateModel(std))
            {
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            return View();
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            Hotels180625 hotel = new Hotels180625();
            hotel = db.Hotels180625.Find(id);
            return View(hotel);


        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var hotel = db.Hotels180625.Where(x => x.HotelID == id).FirstOrDefault();
            db.Hotels180625.Remove(hotel);
            db.SaveChanges();
            return RedirectToAction("MainContent");


        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult RoomIndex()
        {
            var RoomDetails180625 = db.RoomDetails180625.Include(r => r.Hotels180625);
            return View(RoomDetails180625.ToList());
        }

        // GET: RoomDetail/Details/5
        public ActionResult RoomDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomDetails180625 RoomDetails180625 = db.RoomDetails180625.Find(id);
            if (RoomDetails180625 == null)
            {
                return HttpNotFound();
            }
            return View(RoomDetails180625);
        }

        // GET: RoomDetail/Create
        public ActionResult AddRoom()
        {
            ViewBag.HotelID = new SelectList(db.Hotels180625, "HotelID", "City");
            return View();
        }

        // POST: RoomDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult AddRoom([Bind(Include ="hotelid,roomid,roomno,roomtype,per_night_rate,availability,photo")] RoomDetails180625 RoomDetails180625)
        {
            if (ModelState.IsValid)
            {
                db.Entry(RoomDetails180625).State = EntityState.Modified;
                db.RoomDetails180625.Add(RoomDetails180625);
                db.SaveChanges();
                return RedirectToAction("RoomIndex");
            }

            ViewBag.HotelID = new SelectList(db.Hotels180625, "HotelID", "City", RoomDetails180625.HotelID);
            return View(RoomDetails180625);
        }

        // GET: RoomDetail/Edit/5
        public ActionResult EditRoom(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomDetails180625 RoomDetails180625 = db.RoomDetails180625.Find(id);
            if (RoomDetails180625 == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelID = new SelectList(db.Hotels180625, "HotelID", "City", RoomDetails180625.HotelID);
            return View(RoomDetails180625);
        }

        // POST: RoomDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRoom([Bind(Include = "HotelID,RoomID,RoomNo,RoomType,Per_Night_Rate,Availability,Photo")] RoomDetails180625 RoomDetails180625)
        {
            if (ModelState.IsValid)
            {
                db.Entry(RoomDetails180625).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("RoomIndex");
            }
            ViewBag.HotelID = new SelectList(db.Hotels180625, "HotelID", "City", RoomDetails180625.HotelID);
            return View(RoomDetails180625);
        }

        // GET: RoomDetail/Delete/5
        public ActionResult DeleteRoom(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomDetails180625 RoomDetails180625 = db.RoomDetails180625.Find(id);
            if (RoomDetails180625 == null)
            {
                return HttpNotFound();
            }
            return View(RoomDetails180625);
        }

        // POST: RoomDetail/Delete/5
        [HttpPost, ActionName("DeleteRoom")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomDetails180625 RoomDetails180625 = db.RoomDetails180625.Find(id);
            db.RoomDetails180625.Remove(RoomDetails180625);
            db.SaveChanges();
            return RedirectToAction("RoomIndex");
        }
        public ActionResult BookingsOfSpecifichotel(string id)
        {
            BookingDetails180625 bookedroom = new BookingDetails180625();
            Hotels180625 hotel = new Hotels180625();
            return View(db.BookingDetails180625.Where(x => x.RoomID.ToString() == id || id == null).ToList());
           
        
        }
        //public ActionResult GuestListOfSpecificHotel(string id)
        //{
        //    Users180625 user = new Users180625();
        //    Hotels180625 hotel = new Hotels180625();
        //    //return View(db.Users180625.Where(x => x.H.ToString() == id || id == null).ToList());


        //}
        //public ActionResult BookingsforSpecifiedDate(DateTime date)
        //{
        //    BookingDetails180625 bookedroom = new BookingDetails180625();
        //    return View(db.BookingDetails180625.Where(x => DateTime.Parse(x.Booked_From.ToString()) == date || date == null).ToList());
        //}


    }
}
