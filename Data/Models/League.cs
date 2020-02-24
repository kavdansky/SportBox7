using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Data.Models
{
    public class League
    {
        public string Id { get; set; }
        public string LeagueName { get; set; }
        public string LeagueNameInBulgarian { get; set; }
        public string SportName { get; set; }
        public string SportNameInBulgarian { get; set; }
        public string LeagueNameAlternate { get; set; }
    }
}
