using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefWebApiOData.Models
{
    public class GameTeam
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int TeamId { get; set; }

        public Game Game { get; set; }
        public Team Team { get; set; }
    }
}