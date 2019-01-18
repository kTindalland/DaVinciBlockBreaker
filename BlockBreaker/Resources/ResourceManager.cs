using BlockBreaker.Highscores.Resources;

namespace BlockBreaker.Resources
{
    public static class ResourceManager
    {
        // The name and score of the player
        private static Score _playerDetails;

        public static Score PlayerDetails
        {
            get { return _playerDetails; }
            set { _playerDetails = value; }
        }

        // If the player has won the game or not
        public static bool GameWon { get; set; }

    }
}
