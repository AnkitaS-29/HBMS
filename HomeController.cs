using HBMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HBMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Login(Users180625 user)
        {
            using (HotelsEntities db = new HotelsEntities())
            {
                var obj = db.Users180625.Where(a => a.UserName.Equals(user.UserName) && a.Password.Equals(user.Password)).FirstOrDefault();
                if (obj != null)
                {
                    Session["UserID"] = obj.UserID.ToString();
                    Session["UserName"] = obj.UserName.ToString();
                    Session["Role"] = obj.Role.ToString();
                    if (Session["Role"].ToString().ToLower() == "admin")
                    {
                        return RedirectToAction("MainContent","Admin");
                    }
                    else if (Session["Role"].ToString().ToLower() == "hotel emp")
                    {
                        return RedirectToAction("MainContent","HotelEmployee");
                    }
                    else if (Session["Role"].ToString().ToLower() == "customer")
                    {
                        return RedirectToAction("MainContent","Customer" );
                    }
                  
                    else
                    {

                    }

                }
                return View();
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(int UserID,string Password, string Role,string UserName,string MobileNo,string Phone,string Address,string Email)
        {
            
            HotelsEntities db = new HotelsEntities();
         Users180625 user = new Users180625();
            if (user == null)
            {
                ViewBag.Message = " Not Successfully Added";
            }
            
            user.UserID = UserID;
            user.Password = Password;
            user.Role = Role;
            user.UserName = UserName;
            user.MobileNo = MobileNo;
            user.Phone = Phone;
            user.Address = Address;
            user.Email = Email;
           
            db.Users180625.Add(user);
            db.SaveChanges();
            ViewBag.Message = "Successfully Added";
            return RedirectToAction("Login");
        
        }
    }
}
        


  