using BO;
using System.Data.Entity;

namespace DAL
{
    public interface IDbContext
    {
        DbSet<Race> Races { get; set; }
        DbSet<Competitor> Competitors { get; set; }
        DbSet<Organizer> Organizers { get; set; }
    }
}