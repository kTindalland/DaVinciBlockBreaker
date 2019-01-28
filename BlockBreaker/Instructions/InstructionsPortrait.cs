using System;
using BlockBreaker.Renderables;
using BlockBreaker.Resources;
using DaVinci_Framework.DefaultRenderables;
using DaVinci_Framework.KeyGetterThread;
using DaVinci_Framework.PortraitManager.Resources;
using DaVinci_Framework.Renderer.Resources;

namespace BlockBreaker.Instructions
{
    /// <summary>
    /// Portrait for the instructions to the game
    /// </summary>
    public class InstructionsPortrait : Portrait
    {
        public InstructionsPortrait()
        {
            _register = new Register();
            FillRegister();
        }

        private void FillRegister()
        {
            // Register the title
            var title = "Welcome to Block Breaker!";
            _register.RegisterItem(new Text(title, ConsoleColor.White, new double[] { (Console.WindowWidth / 2) - (title.Length / 2), 3 }));

            // Register all the instructions
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

            // Go through each line and register it
            for (int i = 0; i < instructions.Length; i++)
            {
                _register.RegisterItem(new Text(instructions[i], ConsoleColor.White, new double[] { 1, 5 + i }));
            }

            // Register the blocks and labels with them
            // Green
            _register.RegisterItem( new Block(0, new []{70.0, 7.0}, new []{10, 2}) );
            _register.RegisterItem(new Text("Green block: 50pts, 1 hit", ConsoleColor.White, new double[] {82, 7}));

            // Yellow
            _register.RegisterItem(new Block(1, new[] { 70.0, 10.0 }, new[] { 10, 2 }));
            _register.RegisterItem(new Text("Yellow block: 150pts, 2 hits", ConsoleColor.White, new double[] { 82, 10 }));

            // Red
            _register.RegisterItem(new Block(2, new[] { 70.0, 13.0 }, new[] { 10, 2 }));
            _register.RegisterItem(new Text("Red block: 250pts, 3 hits", ConsoleColor.White, new double[] { 82, 13 }));

            // Money
            _register.RegisterItem(new DoublePointsBlock(0, new[] { 70.0, 16.0 }, new[] { 10, 2 }, new Text("", ConsoleColor.Black, new double[] {3,5})));
            _register.RegisterItem(new Text("Money block: doubles score multiplier", ConsoleColor.White, new double[] { 82, 16 }));

            // How to exit
            _register.RegisterItem(new Text("Press ESCAPE to go back to main menu", ConsoleColor.White, new double[] {0, Console.WindowHeight - 1}));
        }

        // Get the input
        public void OnKeyPress(object source, KeyEventArgs args)
        {
            if (_active)
            {
                switch (args.KeyPressed) // Check which key was pressed
                {
                    case ConsoleKey.Escape:
                        _manager.SwitchCurrentPortrait((int)PageHandles.MainMenu); // Switch back to the menu
                        break;
                }
            }
        }

        
    }
}
