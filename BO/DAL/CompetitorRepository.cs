using BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class CompetitorRepository<T> : GenericRepository<Competitor> where T : DbContext, IDbContext
    {

        public CompetitorRepository(T context) : base(context)
        {

        }


        public override void Update(Competitor competitor)
        {
            Competitor c = GetById(competitor.Id);
            c.Id = competitor.Id;
            c.Nom = competitor.Nom;
            c.Prenom = competitor.Prenom;
            c.Email = competitor.Email;
            c.DateNaissance = competitor.DateNaissance;
            c.Race = competitor.Race;
            dbContext.SaveChanges();
        }
    }
}
