using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefWebApiOData.Models
{
    public class GamePlayer
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int GameId { get; set; }
        public int SquadNumber { get; set; }
        public bool IsCaptain { get; set; }

        public virtual Game Game { get; set; }
        public virtual Player Player { get; set; }
        public virtual ICollection<Goal> Goals { get; set; }
        public virtual ICollection<Substitution> Substitutions { get; set; }
        public virtual ICollection<Penalty> Penalties { get; set; }

    }
}