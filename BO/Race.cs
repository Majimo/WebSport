using System;
using System.Collections.Generic;

namespace BO
{
    public class Race : IIdentifiable
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public virtual List<Competitor> Competitors { get; set; }

        public virtual Organizer Organizer { get; set; }

        public Race()
        {
            Competitors = new List<Competitor>();
        }
    }
}