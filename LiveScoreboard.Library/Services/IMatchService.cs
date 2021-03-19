using LiveScoreboard.Library.Models;
using System;

namespace LiveScoreboard.Library.Services
{
    public interface IMatchService
    {
        /// <summary>
        /// Creates a new game
        /// </summary>
        /// <param name="homeTeam">Name of the Home Team</param>
        /// <param name="awayTeam">Name of the Away Team</param>
        /// <param name="startDate">Start date of the match</param>
        /// <returns></returns>
        Game StartMatch(string homeTeam, string awayTeam, DateTime startDate);
    }
}
