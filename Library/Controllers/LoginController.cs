using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class LoginController : Controller
    {
        LibraryEntities db = new LibraryEntities();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(korisnik Korisnik, admin Admin)
        {
            using (db)
            {
                var usr = db.korisnik.Where(u => u.username == Korisnik.username && u.password == Korisnik.password)
                    .FirstOrDefault();
                var adm = db.admin.Where(a => a.username == Admin.username && a.password == Admin.password)
                    .FirstOrDefault();

                if (usr != null)
                {
                    Session["UserID"] = usr.id.ToString();
                    Session["Username"] = usr.username.ToString();
                    return Redirect("/Korisnik/Pocetna");
                }
                else if (adm != null)
                {
                    Session["AdminID"] = adm.id.ToString();
                    Session["AdminName"] = adm.username.ToString();
                    return Redirect("/Admin/Pocetna");

                }
                else
                {
                    ModelState.AddModelError("", "Korisnicko ime ili lozinka je pogresna!");
                }
            }
            return View();
        }

        public ActionResult Logoff()
        {
            Session.Remove("AdminID");
            return Redirect("/Login/Login");
        }
    }
}