using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefWebApiOData.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime GameDateTime { get; set; }

        public virtual ICollection<GameTeam> GameTeams { get; set; }
        public virtual ICollection<GamePlayer> GamePlayers { get; set; }
    }
}