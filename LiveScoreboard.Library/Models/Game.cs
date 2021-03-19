using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveScoreboard.Library.Models
{
    public class Game
    {
        public Guid Id { get; private set; }
        public Team HomeTeam { get; private set; }
        public Team AwayTeam { get; private set; }
        public DateTime StartDate { get; private set; }

        private Game(string homeTeamName, string awayTeamName, DateTime startDate)
        {
            Id = Guid.NewGuid();
            HomeTeam = Team.NewTeam(homeTeamName);
            AwayTeam = Team.NewTeam(awayTeamName);
            StartDate = startDate;
        }

        public static Game NewGame(string homeTeamName, string awayTeamName, DateTime startDate) => new(homeTeamName, awayTeamName, startDate);
    }
}
