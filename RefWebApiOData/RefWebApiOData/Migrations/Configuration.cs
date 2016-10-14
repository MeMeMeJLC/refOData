namespace RefWebApiOData.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RefWebApiOData.Models.RefWebApiODataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RefWebApiOData.Models.RefWebApiODataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Games.AddOrUpdate(
                p => p.Description,
                new Game { Description = "Round 1, Game 1", GameDateTime = new DateTime(2016, 10, 20) },
                new Game { Description = "Round 1, Game 2", GameDateTime = new DateTime(2016, 10, 24) }
                );

            context.Teams.AddOrUpdate(
                p => p.Name,
                new Team { Name = "Lions", Colour = "Yellow" },
                new Team { Name = "Tigers", Colour = "Orange" },
                new Team { Name = "Lizards", Colour = "Green" },
                new Team { Name = "Gulls", Colour = "White" }
                );

            context.GameTeams.AddOrUpdate(
                p => p.TeamId,
                new GameTeam { GameId = 1, TeamId = 1 },
                new GameTeam { GameId = 1, TeamId = 2 },
                new GameTeam { GameId = 2, TeamId = 3 },
                new GameTeam { GameId = 2, TeamId = 4 }
                );

            context.Players.AddOrUpdate(
               p => p.LastName,
               new Player { FirstName = "Jeremy", LastName = "Wright", TeamId = 1 },
               new Player { FirstName = "Bill", LastName = "Ranks", TeamId = 1 },
               new Player { FirstName = "Fred", LastName = "Wilson", TeamId = 1 },
               new Player { FirstName = "Mike", LastName = "Marlowe", TeamId = 1 },
               new Player { FirstName = "Bob", LastName = "Carter", TeamId = 1 },
               new Player { FirstName = "Jeff", LastName = "Walker", TeamId = 1 },
               new Player { FirstName = "Steve", LastName = "Bone", TeamId = 2 },
               new Player { FirstName = "Gil", LastName = "Sutton", TeamId = 2 },
               new Player { FirstName = "Sam", LastName = "Gilmore", TeamId = 2 },
               new Player { FirstName = "Andy", LastName = "Luigi", TeamId = 2 },
               new Player { FirstName = "Jon", LastName = "Berg", TeamId = 2 },
               new Player { FirstName = "Stephen", LastName = "Williams", TeamId = 3 },
               new Player { FirstName = "Mark", LastName = "Lower", TeamId = 3 },
               new Player { FirstName = "Luke", LastName = "Magon", TeamId = 3 },
               new Player { FirstName = "Rob", LastName = "Franks", TeamId = 3 },
               new Player { FirstName = "Will", LastName = "Starr", TeamId = 3 },
               new Player { FirstName = "Phil", LastName = "Jones", TeamId = 3 },
               new Player { FirstName = "David", LastName = "davids", TeamId = 4 },
               new Player { FirstName = "Tim", LastName = "lennon", TeamId = 4 },
               new Player { FirstName = "Ravid", LastName = "Ravids", TeamId = 4 },
               new Player { FirstName = "Dim", LastName = "Dith", TeamId = 4 }
                );

            context.GamePlayers.AddOrUpdate(
                p => p.PlayerId,
                new GamePlayer { PlayerId = 1, GameId = 1, SquadNumber=2, IsCaptain = true },
                new GamePlayer { PlayerId = 2, GameId = 1, SquadNumber=4 },
                new GamePlayer { PlayerId = 3, GameId = 1, SquadNumber=11 },
                new GamePlayer { PlayerId = 8, GameId = 1, SquadNumber=5, IsCaptain = true },
                new GamePlayer { PlayerId = 9, GameId = 1, SquadNumber=10 },
                new GamePlayer { PlayerId = 10, GameId = 1, SquadNumber=7 }
                );

            context.Goals.AddOrUpdate(
                p => p.GamePlayerId,
                new Goal { GamePlayerId = 2, GoalTime = new TimeSpan(0, 25, 24) },
                new Goal { GamePlayerId = 3, GoalTime = new TimeSpan(0, 45, 24) },
                new Goal { GamePlayerId = 6, GoalTime = new TimeSpan(0, 55, 24) }


                );

            context.PenaltyTypes.AddOrUpdate(
                p => p.Code,
                new PenaltyType { Code = "y1", Description = "Unsporting behaviour" },
                new PenaltyType { Code = "r1", Description = "Violent conduct" }
                );

            context.Penalties.AddOrUpdate(
                p => p.GamePlayerId,
                new Penalty { GamePlayerId = 1, PenaltyTypeId = 1, PenaltyTime = new TimeSpan(0, 25, 42) },
                new Penalty { GamePlayerId = 3, PenaltyTypeId = 2, PenaltyTime = new TimeSpan(0, 44, 10) }
                );

            context.Substitutions.AddOrUpdate(
                p => p.GamePlayerGoingOffTheFieldId,
                new Substitution { GamePlayerGoingOffTheFieldId = 2, GamePlayerGoingOnTheFieldId = 3, SubstitutionTime = new TimeSpan(0, 38, 28) },
                new Substitution { GamePlayerGoingOffTheFieldId = 3, GamePlayerGoingOnTheFieldId = 4, SubstitutionTime = new TimeSpan(0, 55, 58) }
                ); 
        }
    }
}
