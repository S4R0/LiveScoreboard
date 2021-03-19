using System;

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

        public Game UpdateGameScore(int scoreHomeTeam, int scoreAwayTeam)
        {
            HomeTeam = HomeTeam.UpdateScore(scoreHomeTeam);
            AwayTeam = AwayTeam.UpdateScore(scoreAwayTeam);

            return this;
        }
    }
}
