namespace LiveScoreboard.Library.Models
{
    public class Team
    {
        public string Name { get; set; }
        public int Score { get; set; }

        private Team(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public static Team NewTeam(string name) => new(name, 0);

        public Team UpdateScore(int score)
        {
            Score = score;
            return this;
        }
    }
}
