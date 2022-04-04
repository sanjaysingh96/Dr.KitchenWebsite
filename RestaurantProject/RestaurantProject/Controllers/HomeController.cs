using RestaurantProject.DB_Connect;
using RestaurantProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RestaurantProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {


            return View();
        }

        [HttpPost]
        public ActionResult Login(UserRegModel modobj)
        {
            RestaurantEntities db = new RestaurantEntities();
            var userlogin = db.UserRes.Where(a => a.Email == modobj.Email).FirstOrDefault();
            if (userlogin == null)
            {
                TempData["invalid"] = "Invalid Email !!";
            }
            else
            {
                if(userlogin.Email==modobj.Email && userlogin.Password == modobj.Password)
                {
                    FormsAuthentication.SetAuthCookie(userlogin.Email, false);

                    Session["username"] = userlogin.Name;
                    Session["useremail"] = userlogin.Email;

                    return RedirectToAction("IndexDashboard", "Home");
                }
                else
                {
                    TempData["wrong"] = "Wrong Password !!";
                    return View();
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult UserReg()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserReg( UserRegModel modobj)
        {
            RestaurantEntities db = new RestaurantEntities();

            UserRe tblobj = new UserRe();
            tblobj.Id = modobj.Id;
            tblobj.Phone_Number = modobj.Phone_Number;
            tblobj.Name = modobj.Name;
            tblobj.Email = modobj.Email;
            tblobj.Password = modobj.Password;

            db.UserRes.Add(tblobj);
            db.SaveChanges();
            return RedirectToAction("Login", "Home");
        }

        [Authorize]
        public ActionResult IndexDashboard()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}