using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockBreaker.Instructions.Resources;
using BlockBreaker.Renderables;
using BlockBreaker.Resources;
using DaVinci_Framework.DefaultRenderables;
using DaVinci_Framework.KeyGetterThread;
using DaVinci_Framework.PortraitManager.Resources;
using DaVinci_Framework.Renderer.Resources;

namespace BlockBreaker.Instructions
{
    public class InstructionsPortrait : Portrait
    {
        private List<Page> _pages;
        private int _currentPage; // The current page of instructions the user is on

        public InstructionsPortrait()
        {
            _register = new Register();
            _pages = new List<Page>();
            GeneratePages();
            FillRegister();
            
        }

        private void GeneratePages()
        {
            _pages.Add(new Page(_register, Page1));
            _pages.Add(new Page(_register, Page2));
            _pages.Add(new Page(_register, Page3));
        }

        private void FillRegister()
        {
            _pages[0].Draw();   
        }

        /// <summary>
        /// Page 1: general game instructions.
        /// </summary>
        /// <param name="reg">The register it writes to.</param>
        private void Page1(Register reg)
        {
            var footer = "   Page 1 >>";
            reg.RegisterItem( new Text(footer, ConsoleColor.White, new double[] {(Console.WindowWidth / 2) - (footer.Length / 2), Console.WindowHeight - 1}));

            var title = "Welcome to Block Breaker!";
            reg.RegisterItem(new Text(title, ConsoleColor.White, new double[] {(Console.WindowWidth / 2) - (title.Length / 2) , 3 }));

            var instructions = new string[]
            {
                "How to play:",
                "",
                "When you have entered your name and began the game the ball",
                "will move away from the paddle. Your aim is to move the",
                "paddle using the left and right arrow keys on your keyboard", 
                "to hit the ball on the return bounce.",
                "",
                "When you break a block, you get the score worth of that block",
                "added to your highscore. Some blocks take longer to break than",
                "others, and those are worth more points for it.",
                "",
                "As soon as you hit the top wall, you've won, so make sure to",
                "break as many blocks as you can before you hit the top wall!"
            };

            for (int i = 0; i < instructions.Length; i++)
            {
                reg.RegisterItem(new Text(instructions[i], ConsoleColor.White, new double[] {1, 5 + i}));
            }
        }

        /// <summary>
        /// Page 2: Explaining objects.
        /// </summary>
        /// <param name="reg">The register it writes to.</param>
        private void Page2(Register reg)
        {
            var footer = "<< Page 2 >>";
            reg.RegisterItem(new Text(footer, ConsoleColor.White, new double[] { (Console.WindowWidth / 2) - (footer.Length / 2), Console.WindowHeight - 1 }));

            reg.RegisterItem(new Block(0, new double[] {2, 2}, new []{10, 2}));
        }

        /// <summary>
        /// Page 3: Explaining score.
        /// </summary>
        /// <param name="reg">The register it writes to.</param>
        private void Page3(Register reg)
        {
            var footer = "<< Page 3   ";
            reg.RegisterItem(new Text(footer, ConsoleColor.White, new double[] { (Console.WindowWidth / 2) - (footer.Length / 2), Console.WindowHeight - 1 }));


        }

        private void BlankPage()
        {
            for (int y = 0; y < Console.WindowHeight; y++)
            {
                var line = new string(' ', Console.WindowWidth);

                _register.RegisterItem(new Text(line, ConsoleColor.Black, new []{0, double.Parse(y.ToString())}));
            }
        }

        private void ChangePage(int change)
        {
            _currentPage += change;

            if (_currentPage < 0)
                _currentPage = 0;

            if (_currentPage >= _pages.Count)
                _currentPage = _pages.Count - 1;

            SwitchItems();
        }

        private void SwitchItems()
        {
            _register.UnRegisterAllItems();
            BlankPage();

            _pages[_currentPage].Draw();

            
        }

        public void OnKeyPress(object source, KeyEventArgs args)
        {
            if (_active)
            {
                switch (args.KeyPressed)
                {
                    case ConsoleKey.RightArrow:
                        ChangePage(1);
                        break;

                    case ConsoleKey.LeftArrow:
                        ChangePage(-1);
                        break;

                    case ConsoleKey.Escape:
                        _manager.SwitchCurrentPortrait((int)PageHandles.MainMenu);
                        break;
                }
            }
        }

        
    }
}
