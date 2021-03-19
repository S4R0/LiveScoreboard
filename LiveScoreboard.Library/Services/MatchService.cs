using LiveScoreboard.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiveScoreboard.Library.Services
{
    public class MatchService : IMatchService
    {
        private static readonly List<Game> _games = new();

        public Game StartMatch(string homeTeamName, string awayTeamName, DateTime startDate)
        {
            var game = Game.NewGame(homeTeamName, awayTeamName, startDate);

            _games.Add(game);

            return game;
        }

        public bool FinishGame(Guid gameId) => _games.RemoveAll(x => x.Id.Equals(gameId)) > 0;

        public Game UpdateScore(Guid gameId, int scoreHomeTeam, int scoreAwayTeam)
        {
            var game = _games.Single(x => x.Id.Equals(gameId));

            return game.UpdateGameScore(scoreHomeTeam, scoreAwayTeam);
        }
    }
}
