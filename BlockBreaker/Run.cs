using BlockBreaker.EnterName;
using BlockBreaker.Game;
using BlockBreaker.Highscores;
using BlockBreaker.MainMenu;
using BlockBreaker.Resources;
using DaVinci_Framework.Display;
using DaVinci_Framework.KeyGetterThread;
using DaVinci_Framework.PortraitManager;
using DaVinci_Framework.Renderer;
using DaVinci_Framework.Renderer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlockBreaker.EndGame;
using BlockBreaker.Highscores.ScoreGrabber;
using BlockBreaker.Highscores.ScoreWriter;
using BlockBreaker.Instructions;

namespace BlockBreaker
{
    class Run
    {
        static void Main()
        {
            var framerate = 60; // How many ticks a second will occur

            var keyListener = new Listener(); // Create the listener
            KeyGetter.Run(keyListener); // Start the listener thread
            Console.CursorVisible = false; // Hide the cursor
            Console.Title = "BlockBreaker"; // Set the title of the console window

            // Setup filepath to highscores
            ResourceManager.HighscoresPath = AppDomain.CurrentDomain.BaseDirectory;
            ResourceManager.HighscoresPath += "../../highscores.xml";

            // Create score handlers
            var scorewriter = new XMLScoreWriter();

            var renderer = new Renderer(new Register());
            var display = new Display();

            // Create Portraits
            var mainMenu = new MenuPortrait();
            var highscores = new HighscoresPortrait();
            var enterName = new EnterNamePortrait();
            var mainGame = new GamePortrait();
            var endGame = new EndGamePortrait(scorewriter);
            var instructions = new InstructionsPortrait();

            // Assign keypress events
            keyListener.KeyGot += mainMenu.OnKeyPress;
            keyListener.KeyGot += highscores.OnKeyPress;
            keyListener.KeyGot += enterName.OnKeyPress;
            keyListener.KeyGot += mainGame.OnKeyPress;
            keyListener.KeyGot += endGame.OnKeyPress;
            keyListener.KeyGot += instructions.OnKeyPress;

            // Set up manager
            var portraitManager = new Manager(renderer, display, (int)PageHandles.MainMenu, mainMenu);
            portraitManager.AddPortrait((int)PageHandles.Highscores, highscores);
            portraitManager.AddPortrait((int)PageHandles.GiveName, enterName);
            portraitManager.AddPortrait((int)PageHandles.MainGame, mainGame);
            portraitManager.AddPortrait((int)PageHandles.EndGame, endGame);
            portraitManager.AddPortrait((int)PageHandles.Instructions, instructions);

            while (true)
            {
                portraitManager.Tick();

                Thread.Sleep(1000 / framerate);
            }

        }
    }
}
