using System.Collections.Generic;
using BlockBreaker.Highscores.Resources;

namespace BlockBreaker.Highscores.Interfaces
{
    /// <summary>
    /// Interface that every class that will grab scores from somewhere will implement
    /// </summary>
    public interface IScoreGrabber
    {
        List<Score> ReadScores(string path, int amount); // Returns a path from where you want to grab scores from.
    }
}
