using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class KorisnikController : Controller
    {
        LibraryEntities db = new LibraryEntities();
        // GET: Korisnik
        public ActionResult Pocetna()
        {
            var view =
                from k in db.knjiga
                join a in db.autor
                on k.id equals a.knjiga_id 
                orderby k.naslov
                select new SpojKa { Autor = a, Knjiga = k };
            if (Session["UserID"] != null)
            {
                return View(view);
            }
            else
            {
                return Redirect("/Login/Login");
            }
        }

        public ActionResult Rezervacija()
        {

            return View();
        }


    }
}