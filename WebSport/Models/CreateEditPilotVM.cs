using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BO;

namespace WebSport.Models
{
    public class CreateEditPilotVM
    {
        public Race Race { get; set; }
        public Organizer Organizer { get; set; }
        public List<Competitor> Competitors { get; set; }
        public List<int> IdCompetitors { get; set; }
    }
}