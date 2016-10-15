using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefWebApiOData.Models
{
    public class Penalty
    {
        public int Id { get; set; }
        public int GamePlayerId { get; set; }
        public int PenaltyTypeId { get; set; }
        public TimeSpan PenaltyTime { get; set; }

        public GamePlayer GamePlayer { get; set; }
        public PenaltyType PenaltyType { get; set; }
    }
}