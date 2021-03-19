using LiveScoreboard.Library.Models;
using System;
using System.Collections.Generic;

namespace LiveScoreboard.Library.Services
{
    public interface IScoreBoardService
    {
        /// <summary>
        /// Creates a new game
        /// </summary>
        /// <param name="homeTeam">Name of the Home Team</param>
        /// <param name="awayTeam">Name of the Away Team</param>
        /// <param name="startDate">Start date of the match</param>
        /// <returns></returns>
        Game StartMatch(string homeTeam, string awayTeam, DateTime startDate);

        /// <summary>
        /// Removes a game from the live scoreboard
        /// </summary>
        /// <param name="gameId">Game Identifier</param>
        /// <returns></returns>
        bool FinishGame(Guid gameId);

        /// <summary>
        /// Updates the score of a game
        /// </summary>
        /// <param name="gameId">Game Identifier</param>
        /// <param name="scoreHomeTeam">Score of the Home Team</param>
        /// <param name="scoreAwayTeam">Score of the Away Team</param>
        /// <returns></returns>
        Game UpdateScore(Guid gameId, int scoreHomeTeam, int scoreAwayTeam);

        /// <summary>
        /// Order the games by the total score and system entry date
        /// </summary>
        /// <returns></returns>
        List<Game> GamesSummary();
    }
}
