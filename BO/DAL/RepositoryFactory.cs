//using BO;
using BO;
using DAL.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RepositoryFactory<C> where C : DbContext, IDbContext
    {
        public static IRepository<T> GetRepository<T>(DbContext context) where T : class, IIdentifiable
        {
            return new GenericRepository<T>(context);
        }

        public static IRepository<Competitor> GetCompetitorRepository(C context)
        {
            return new CompetitorRepository<C>(context);
        }
        public static IRepository<Race> GetRaceRepository(C context)
        {
            return new RaceRepository<C>(context);
        }

        public static IRepository<Organizer> GetOrganizerRepository(C context)
        {
            return new OrganizerRepository<C>(context);
        }
    }
}
