using LiveScoreboard.Library.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace LiveScoreboard.Tests
{
    public class Tests
    {
        private readonly IMatchService _matchService;

        public Tests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IMatchService, MatchService>();
            _matchService = serviceCollection.BuildServiceProvider().GetService<IMatchService>();
        }

        [Fact]
        public void StartMatch_Result_True()
        {
            var homeTeamName = "Mexico";
            var awayTeamName = "Canada";
            var startDate = DateTime.UtcNow.AddHours(1);
            var match = _matchService.StartMatch(homeTeamName, awayTeamName, startDate);

            Assert.NotNull(match);
            Assert.Equal(startDate, match.StartDate);
            Assert.Equal(0, match.HomeTeam.Score);
            Assert.Equal(0, match.AwayTeam.Score);
            Assert.Equal(homeTeamName, match.HomeTeam.Name);
            Assert.Equal(awayTeamName, match.AwayTeam.Name);
        }
    }
}