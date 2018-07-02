using BO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DAL
{
    class RaceRepository<T> : GenericRepository<Race> where T : DbContext, IDbContext
    {
        public RaceRepository(T context) : base(context)
        {
        }

        public override void Update(Race race)
        {
            Race r = GetById(race.Id);
            r.Id = race.Id;
            r.Title = race.Title;
            r.Description = race.Description;
            r.DateStart = race.DateStart;
            r.DateEnd = race.DateEnd;
            r.Organizer = race.Organizer;
            // TODO : race competitors
            dbContext.SaveChanges();
        }
    }
}
