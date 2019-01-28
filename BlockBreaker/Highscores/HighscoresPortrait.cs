using BlockBreaker.Highscores.Resources;
using BlockBreaker.Highscores.ScoreGrabber;
using BlockBreaker.Resources;
using DaVinci_Framework.DefaultRenderables;
using DaVinci_Framework.KeyGetterThread;
using DaVinci_Framework.PortraitManager.Resources;
using DaVinci_Framework.Renderer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockBreaker.Highscores
{
    public class HighscoresPortrait : Portrait
    {
        private List<Score> _scores; // List of highscores
        private Text _title;


        public override void SelectThisPortrait()
        {
            _register = new Register(); // Create a new blank register
            base.SelectThisPortrait();

            GetScores(); // Get the scores
            FillRegister(); // Fill in the register with the scores
        }

        private void GetScores()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory; // Get the path to the highscores
            path += "../../highscores.xml";

            var scoreReader = new XMLScoreGrabber(); // Create scoregrabber

            _scores = scoreReader.ReadScores(path, 10); // Get the first ten scores
            _scores = _scores.OrderByDescending(x => x.HighScore).ToList<Score>();
        }

        private void FillRegister()
        {
            var topOffset = 5; // How far off the top the scores are

            // Register title
            var title = "HIGHSCORES";
            _title = new Text(title, ConsoleColor.White, new double[] { (Console.WindowWidth / 2) - (title.Length / 2), topOffset - 1 });
            _register.RegisterItem(_title);

            // Register line
            var line = new Text("----------++----------", ConsoleColor.Red, new double[] { (Console.WindowWidth / 2) - 11, topOffset });
            _register.RegisterItem(line);

            // Register scores
            for (int i = 0; i < _scores.Count; i++)
            {
                _register.RegisterItem(new Text((i + 1).ToString().PadLeft(2) + ".", ConsoleColor.Red, new double[] { (Console.WindowWidth / 2) - 15, topOffset + 1 + i })); // Rank
                _register.RegisterItem(new Text(_scores[i].Name, ConsoleColor.Cyan, new double[] { (Console.WindowWidth / 2) - 11, topOffset + 1 + i })); // Name
                _register.RegisterItem(new Text(_scores[i].HighScore.ToString(), ConsoleColor.Green, new double[] { (Console.WindowWidth / 2) + 1, topOffset + 1 + i })); // Score
            }

            // Register info
            _register.RegisterItem(new Text("Press SPACEBAR to return to the menu...", ConsoleColor.White, new double[] { 0, Console.WindowHeight - 1 }));
        }


        public void OnKeyPress(object source, KeyEventArgs args)
        {
            if (_active)
            {
                if (args.KeyPressed == ConsoleKey.Spacebar) // If spacebar is pressed, switch to the menu
                    _manager.SwitchCurrentPortrait((int)PageHandles.MainMenu);
            }
        }
    }
}
