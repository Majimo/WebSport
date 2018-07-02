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
using Microsoft.AspNet.Identity.EntityFramework;
using WebSport.Models;

namespace WebSport.Controllers
{
    [Authorize(Roles = "Administrator, Organizer")]
    public class OrganizersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Organizers
        public ActionResult Index()
        {
            return View(db.Organizers.ToList());
        }

        // GET: Organizers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organizer organizer = db.Organizers.Find(id);
            if (organizer == null)
            {
                return HttpNotFound();
            }
            return View(organizer);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Organizers/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        // POST: Organizers/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom,Prenom,Email,DateNaissance")] Organizer organizer)
        {
            if (ModelState.IsValid)
            {
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
                var organizerUser = new ApplicationUser();
                organizerUser.Email = organizerUser.UserName = organizer.Email;
                if (userManager.Create(organizerUser, "Pa$$w0rd").Succeeded)
                {
                    userManager.AddToRole(organizerUser.Id, "Organizer");
                    userManager.AddToRole(organizerUser.Id, "Visitor");
                    db.Organizers.Add(organizer);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(organizer);
        }

        [Authorize(Roles = "Organizer")]
        // GET: Organizers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organizer organizer = db.Organizers.Find(id);
            if (organizer == null)
            {
                return HttpNotFound();
            }
            var organizerConnnect = db.Organizers.FirstOrDefault(c => c.Email == User.Identity.Name);
            if (User.IsInRole("Administrator") || organizer == organizerConnnect)
            {
                return View(organizer);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Organizer")]
        // POST: Organizers/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom,Prenom,Email,DateNaissance")] Organizer organizer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(organizer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(organizer);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Organizers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organizer organizer = db.Organizers.Find(id);
            if (organizer == null)
            {
                return HttpNotFound();
            }
            return View(organizer);
        }

        [Authorize(Roles = "Administrator")]
        // POST: Organizers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Organizer organizer = db.Organizers.Find(id);
            db.Organizers.Remove(organizer);
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
