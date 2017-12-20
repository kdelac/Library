using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Library.Models;


namespace Library.Controllers
{
    public class AdminController : Controller
    {
        LibraryEntities db = new LibraryEntities();
        
        // GET: Admin
        public ActionResult Pocetna()
        {
            var view =
                from k in db.knjiga
                join a in db.autor 
                on k.id equals a.knjiga_id
                select new SpojKa {Autor = a, Knjiga = k};

            if (Session["AdminID"] != null)
            {
                return View(view);
            }
            else
            {
                return Redirect("/Login/Login");
            }
            
        }

        public ActionResult Korisnik()
        {
            if (Session["AdminID"] != null)
            {
                return View(db.korisnik.ToList());
            }
            else
            {
                return Redirect("/Login/Login");
            }
        }

        public ActionResult DodajKnjigu()
        {
            ViewBag.zanr_id = new SelectList(db.zanr, "id", "naziv");
            if (Session["AdminID"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("/Login/Login");
            }

        }
        

        [HttpPost]
        public ActionResult DodajKnjigu([Bind(Include = "naslov, zanr_id")] knjiga Knjiga)
        {
            if (ModelState.IsValid)
            {
                db.knjiga.Add(Knjiga);
                db.SaveChanges();
                return RedirectToAction("DodajAutora");
            }
            ModelState.Clear();
            return View();
        }

        public ActionResult DodajAutora()
        {
            ViewBag.knjiga_id = new SelectList(db.knjiga, "id", "naslov");
            if (Session["AdminID"] != null)
            {
            return View();
            }
            else
            {
                return Redirect("/Login/Login");
            }
        }
        [HttpPost]
        public ActionResult DodajAutora([Bind(Include = "ime, prezime, knjiga_id")] autor Autor)
        {
            if (ModelState.IsValid)
            {
                db.autor.Add(Autor);
                db.SaveChanges();
                return RedirectToAction("DodajIzdavaca");
            }   
            ModelState.Clear();
            return View();
        }

        public ActionResult DodajIzdavaca()
        {
            ViewBag.knjiga_id = new SelectList(db.knjiga, "id", "naslov");
        if (Session["AdminID"] != null)
        {
            return View();
        }
        else
        {
            return Redirect("/Login/Login");
        }
}
        [HttpPost]
        public ActionResult DodajIzdavaca([Bind(Include = "naziv, mjesto, knjiga_id")] izdavac Izdavac)
        {
            if (ModelState.IsValid)
            {
                db.izdavac.Add(Izdavac);
                db.SaveChanges();
                return RedirectToAction("Pocetna");
            }
            ModelState.Clear();
            return View();
        }
        public ActionResult IzbrisiKorisnika(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            korisnik korisnik = db.korisnik.Find(id);
            if (korisnik == null)
            {
                return HttpNotFound();
            }
            return DeleteConfirmed(korisnik.id);
        }

        // POST: Odjeli/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            korisnik korisnik = db.korisnik.Find(id);
            db.korisnik.Remove(korisnik);
            db.SaveChanges();
            return RedirectToAction("Korisnik");
        }

    }


}