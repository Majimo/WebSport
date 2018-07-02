using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BO;
using Microsoft.AspNet.Identity;
using WebSport.Models;

namespace WebSport.Controllers
{
    [Authorize(Roles = "Organizer,Competitor")]
    public class RacesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        // GET: Races
        public ActionResult Index()
        {
            return View(db.Races.ToList());
        }

        [AllowAnonymous]
        // GET: Races/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Race race = db.Races.Find(id);
            if (race == null)
            {
                return HttpNotFound();
            }
            return View(race);
        }

        [Authorize(Roles = "Competitor")]
        // GET: Races/RegisterDetails/5
        public ActionResult RegisterDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Race race = db.Races.Find(id);
            if (race == null)
            {
                return HttpNotFound();
            }
            return View(race);
        }

        [Authorize(Roles = "Competitor")]
        // GET: Races/Register/5
        public ActionResult Register(int? id)
        {
            if (ModelState.IsValid)
            {
                var competitor = db.Competitors.FirstOrDefault(c => c.Email == User.Identity.Name);
                var race = db.Races.Find(id);
                competitor.Race = race;
                db.Entry(competitor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("RegisterDetails");
        }
        
        [Authorize(Roles = "Organizer")]
        // GET: Races/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Organizer")]
        // POST: Races/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,DateStart,DateEnd")] Race race)
        {
            if (ModelState.IsValid)
            {
                race.Organizer = db.Organizers.FirstOrDefault(c => c.Email == User.Identity.Name);
                db.Races.Add(race);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(race);
        }

        [Authorize(Roles = "Organizer")]
        // GET: Races/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Race race = db.Races.Find(id);
            if (race == null)
            {
                return HttpNotFound();
            }
            var organizerConnnect = db.Organizers.FirstOrDefault(c => c.Email == User.Identity.Name);
            if (User.IsInRole("Administrator") || race.Organizer.Id == organizerConnnect.Id)
            {
                return View(race);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Organizer")]
        // POST: Races/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,DateStart,DateEnd")] Race race)
        {
            if (ModelState.IsValid)
            {
                db.Entry(race).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(race);
        }

        [Authorize(Roles = "Organizer")]
        // GET: Races/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Race race = db.Races.Find(id);
            if (race == null)
            {
                return HttpNotFound();
            }
            var organizerConnnect = db.Organizers.FirstOrDefault(c => c.Email == User.Identity.Name);
            if (User.IsInRole("Administrator") || race.Organizer.Id == organizerConnnect.Id)
            {
                return View(race);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "Organizer")]
        // POST: Races/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Race race = db.Races.Find(id);
            db.Races.Remove(race);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
