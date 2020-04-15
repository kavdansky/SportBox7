using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportBox7.Areas.Editors.ViewModels.Content.TheSportDbModels
{
    public class TeamsContainer
    {
        public TeamsContainer()
        {
            this.teams = new List<Team>();
        }

        public List<Team> teams { get; set; }
    }
}
