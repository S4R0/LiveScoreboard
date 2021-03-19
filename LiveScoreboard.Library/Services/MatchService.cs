using LiveScoreboard.Library.Models;
using System;
using System.Collections.Generic;

namespace LiveScoreboard.Library.Services
{
    public class MatchService : IMatchService
    {
        private static readonly List<Game> _games = new List<Game>();

        public Game StartMatch(string homeTeamName, string awayTeamName, DateTime startDate)
        {
            var game = Game.NewGame(homeTeamName, awayTeamName, startDate);

            _games.Add(game);

            return game;
        }

        public bool FinishGame(Guid gameId)
        {
            try
            {
                _games.RemoveAll(x => x.Id.Equals(gameId));
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
