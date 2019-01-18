using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockBreaker.Resources;
using DaVinci_Framework.DefaultRenderables;
using DaVinci_Framework.KeyGetterThread;
using DaVinci_Framework.PortraitManager.Resources;
using DaVinci_Framework.Renderer.Resources;

namespace BlockBreaker.EndGame
{
    public class EndGamePortrait : Portrait
    {

        public EndGamePortrait()
        {
            _register = new Register();
        }

        public override void SelectThisPortrait()
        {
            base.SelectThisPortrait();

            //_register = new Register();
            if (ResourceManager.GameWon)
            {
                FillRegisterWinner();
            }
            else
            {
                FillRegisterLoser();
            }
        }

        public void FillRegisterWinner()
        {
            FillRegister();

            var text = "WINNER";

            var title = new Text(text, ConsoleColor.Green, new double[] {(Console.WindowWidth / 2) - (text.Length / 2), 12});
            _register.RegisterItem(title);

            /*var para = new string[]
            {
                "Your score WILL be added to the highscores",
                "Well done, but can you do better?"
            };

            for (int i = 0; i < para.Length; i++)
            {
                _register.RegisterItem(new Text(para[i], ConsoleColor.White, new double[] { (Console.WindowWidth / 2) - (para[i].Length / 2), 13 + i }));
            }*/

            _register.Refresh();
        }

        public void FillRegisterLoser()
        {
            FillRegister();

            var text = "LOSER";

            var title = new Text(text, ConsoleColor.Red, new double[] { (Console.WindowWidth / 2) - (text.Length / 2), 12 });
            _register.RegisterItem(title);

            var para = new string[]
            {
                "Your score WILL NOT be added to the highscores",
                "Better luck next time."
            };

            for (int i = 0; i < para.Length; i++)
            {
                _register.RegisterItem(new Text(para[i], ConsoleColor.White, new double[] { (Console.WindowWidth / 2) - (para[i].Length / 2), 13 + i}));
            }
        }

        public void FillRegister()
        {
            _register.RegisterItem(new Text("Your final score: " + ResourceManager.PlayerDetails.HighScore, ConsoleColor.Cyan, new double[]{0,0}));
        }

        public void OnKeyPress(object source, KeyEventArgs args)
        {
            if (_active)
            {
                switch (args.KeyPressed)
                {
                    case ConsoleKey.Spacebar:
                        _manager.SwitchCurrentPortrait((int)PageHandles.MainMenu);
                        break;
                }
            }
        }
    }
}
