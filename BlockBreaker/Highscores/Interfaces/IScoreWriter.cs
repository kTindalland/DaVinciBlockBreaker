using BlockBreaker.Highscores.Resources;

namespace BlockBreaker.Highscores.Interfaces
{
    /// <summary>
    /// Interface that every class that writes new highscores to a leaderboard will implement.
    /// </summary>
    public interface IScoreWriter
    {
        void AppendScore(Score newScore, string path); // Add a new score to the leaderboard.
    }
}
