using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RefWebApiOData.Models
{
    public class RefWebApiODataContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public RefWebApiODataContext() : base("name=RefWebApiODataContext")
        {
        }

        public System.Data.Entity.DbSet<RefWebApiOData.Models.Game> Games { get; set; }

        public System.Data.Entity.DbSet<RefWebApiOData.Models.GameTeam> GameTeams { get; set; }

        public System.Data.Entity.DbSet<RefWebApiOData.Models.Team> Teams { get; set; }

        public System.Data.Entity.DbSet<RefWebApiOData.Models.Player> Players { get; set; }

        public System.Data.Entity.DbSet<RefWebApiOData.Models.GamePlayer> GamePlayers { get; set; }

        public System.Data.Entity.DbSet<RefWebApiOData.Models.Goal> Goals { get; set; }

        public System.Data.Entity.DbSet<RefWebApiOData.Models.PenaltyType> PenaltyTypes { get; set; }

        public System.Data.Entity.DbSet<RefWebApiOData.Models.Penalty> Penalties { get; set; }

        public System.Data.Entity.DbSet<RefWebApiOData.Models.Substitution> Substitutions { get; set; }
    }
}
