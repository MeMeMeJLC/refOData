using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefWebApiOData.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TeamId { get; set; }

        public Team Team { get; set; }
    }
}