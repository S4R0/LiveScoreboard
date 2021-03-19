using LiveScoreboard.Library.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LiveScoreboard.Tests
{
    public class Tests
    {
        private readonly IScoreBoardService _scoreBoardService;

        public Tests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IScoreBoardService, ScoreBoardService>();
            _scoreBoardService = serviceCollection.BuildServiceProvider().GetService<IScoreBoardService>();
        }

        public static readonly object[][] TestData =
        {
            new object[] { "Mexico", "Canada", DateTime.UtcNow.AddHours(1)},
            new object[] { "Spain", "Brazil", DateTime.UtcNow.AddMinutes(100)}
        };


        [Theory, MemberData(nameof(TestData))]
        public void StartMatch_Result_True(string homeTeamName, string awayTeamName, DateTime startDate)
        {
            var match = _scoreBoardService.StartMatch(homeTeamName, awayTeamName, startDate);

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
            var match = _scoreBoardService.StartMatch(homeTeamName, awayTeamName, startDate);

            Assert.NotNull(match);

            var finished = _scoreBoardService.FinishGame(match.Id);

            Assert.True(finished);
        }

        [Theory, MemberData(nameof(TestData))]
        public void FinishGame_Deleted_False(string homeTeamName, string awayTeamName, DateTime startDate)
        {
            var match = _scoreBoardService.StartMatch(homeTeamName, awayTeamName, startDate);

            Assert.NotNull(match);

            var finished = _scoreBoardService.FinishGame(Guid.NewGuid());

            Assert.False(finished);
        }

        [Theory, MemberData(nameof(TestData))]
        public void UpdateScore_True(string homeTeamName, string awayTeamName, DateTime startDate)
        {
            var match = _scoreBoardService.StartMatch(homeTeamName, awayTeamName, startDate);

            Assert.NotNull(match);

            var updatedMatch = _scoreBoardService.UpdateScore(match.Id, 1, 0);

            Assert.Equal(1, updatedMatch.HomeTeam.Score);
            Assert.Equal(0, updatedMatch.AwayTeam.Score);
        }

        [Fact]
        public void GamesSummary_True()
        {
            var matchA =_scoreBoardService.StartMatch("Mexico", "Canada", DateTime.UtcNow.AddDays(1));
            var matchB = _scoreBoardService.StartMatch("Spain", "Brazil", DateTime.UtcNow.AddDays(2));
            var matchC = _scoreBoardService.StartMatch("Germany", "France", DateTime.UtcNow.AddDays(3));
            var matchD = _scoreBoardService.StartMatch("Uruguay", "Italy", DateTime.UtcNow.AddDays(4));
            var matchE = _scoreBoardService.StartMatch("Argentina", "Australia", DateTime.UtcNow.AddDays(5));

            _scoreBoardService.UpdateScore(matchA.Id, 0, 5);
            _scoreBoardService.UpdateScore(matchB.Id, 10, 2);
            _scoreBoardService.UpdateScore(matchC.Id, 2, 2);
            _scoreBoardService.UpdateScore(matchD.Id, 6, 6);
            _scoreBoardService.UpdateScore(matchE.Id, 3, 1);

            var orderedList = _scoreBoardService.GamesSummary();

            Assert.Equal(matchD, orderedList.ElementAt(0));
            Assert.Equal(matchB, orderedList.ElementAt(1));
            Assert.Equal(matchA, orderedList.ElementAt(2));
            Assert.Equal(matchE, orderedList.ElementAt(3));
            Assert.Equal(matchC, orderedList.ElementAt(4));
        }
    }
}
