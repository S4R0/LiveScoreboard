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

        public static readonly object[][] TestData =
        {
            new object[] { "Mexico", "Canada", DateTime.UtcNow.AddHours(1)},
            new object[] { "Spain", "Brazil", DateTime.UtcNow.AddMinutes(100)}
        };


        [Theory, MemberData(nameof(TestData))]
        public void StartMatch_Result_True(string homeTeamName, string awayTeamName, DateTime startDate)
        {
            var match = _matchService.StartMatch(homeTeamName, awayTeamName, startDate);

            Assert.NotNull(match);
            Assert.Equal(startDate, match.StartDate);
            Assert.Equal(0, match.HomeTeam.Score);
            Assert.Equal(0, match.AwayTeam.Score);
            Assert.Equal(homeTeamName, match.HomeTeam.Name);
            Assert.Equal(awayTeamName, match.AwayTeam.Name);
        }

        [Theory, MemberData(nameof(TestData))]
        public void FinishGame_Deleted_True(string homeTeamName, string awayTeamName, DateTime startDate)
        {
            var match = _matchService.StartMatch(homeTeamName, awayTeamName, startDate);

            Assert.NotNull(match);

            var finished = _matchService.FinishGame(match.Id);

            Assert.True(finished);
        }

        [Theory, MemberData(nameof(TestData))]
        public void FinishGame_Deleted_False(string homeTeamName, string awayTeamName, DateTime startDate)
        {
            var match = _matchService.StartMatch(homeTeamName, awayTeamName, startDate);

            Assert.NotNull(match);

            var finished = _matchService.FinishGame(Guid.NewGuid());

            Assert.False(finished);
        }
    }
}
