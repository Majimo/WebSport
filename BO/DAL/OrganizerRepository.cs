using BO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Migrations
{
    class OrganizerRepository<T> : GenericRepository<Organizer> where T : DbContext, IDbContext
    {
        public OrganizerRepository(T context) : base(context)
        {

        }

        public override void Update(Organizer organizer)
        {
            Organizer o = GetById(organizer.Id);
            o.Id = organizer.Id;
            o.Nom = organizer.Nom;
            o.Prenom = organizer.Prenom;
            o.Email = organizer.Email;
            o.DateNaissance = organizer.DateNaissance;
            // o.Races = organizer.Races;
            dbContext.SaveChanges();
        }

    }
}
