namespace BlockBreaker.Highscores.Resources
{
    /// <summary>
    /// A struct to hold all the details of a highscore in one place.
    /// </summary>
    public struct Score
    {
        public string Name { get; set; } // The name of the player that obtained the score
        public int HighScore { get; set; } // The score obtained by the player

        /// <summary>
        /// Create a new instance of score.
        /// </summary>
        /// <param name="name">The player's username</param>
        /// <param name="highscore">The score they got</param>
        public Score(string name, int highscore)
        {
            Name = name;
            HighScore = highscore;
        }
    }
}
