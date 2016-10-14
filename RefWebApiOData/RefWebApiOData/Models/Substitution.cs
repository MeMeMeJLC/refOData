using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefWebApiOData.Models
{
    public class Substitution
    {
        public int Id { get; set; }
        public int GamePlayerGoingOffTheFieldId { get; set; }
        public int GamePlayerGoingOnTheFieldId { get; set; }
        public TimeSpan SubstitutionTime { get; set; }

        public GamePlayer GamePlayerGoingOffTheField { get; set; }
        public GamePlayer GamePlayerGoingOnTheField { get; set; }

    }
}