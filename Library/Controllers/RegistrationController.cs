using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class RegistrationController : Controller
    {
        LibraryEntities db = new LibraryEntities();
        // GET: Registration
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(korisnik racun)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    db.korisnik.Add(racun);
                    db.SaveChanges();
                }
                ModelState.Clear();
                return Redirect("/Login/Login");
            }
            return View();
        }
    }
}