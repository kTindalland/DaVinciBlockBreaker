using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockBreaker.Game.Resources;
using BlockBreaker.Highscores.Resources;
using BlockBreaker.Renderables;
using BlockBreaker.Resources;
using DaVinci_Framework.DefaultRenderables;
using DaVinci_Framework.KeyGetterThread;
using DaVinci_Framework.PortraitManager.Resources;
using DaVinci_Framework.Renderer.Resources;

namespace BlockBreaker.Game
{
    public class GamePortrait : Portrait
    {
        private Text _nameAndScore; // The text object that is responsible for the display of name and scores
        private List<Block> _blocks; // The list of blocks
        private Paddle _paddle; // The paddle
        private Ball _ball;
        private Text _topWall; // The top wall
        private Text _bottomWall; // The bottom wall

        public GamePortrait()
        {
            _register = new Register(50, 40); // Create a register that has width 50 and height 40


        }

        private void FillRegister()
        {
            // Name and Score
            _nameAndScore = new Text("Temp", ConsoleColor.Cyan, new double[] { 0, 0 });
            _register.RegisterItem(_nameAndScore);

            // Top and bottom walls
            var wallString = new string('#', 50);
            var topPad = 3; var bottomPad = 3;

            _topWall = new Text(wallString, ConsoleColor.DarkYellow, new double[] {0, topPad});
            _bottomWall = new Text(wallString, ConsoleColor.DarkYellow, new double[] {0, 39 - bottomPad});

            _register.RegisterItem(_topWall);
            _register.RegisterItem(_bottomWall);

            // Ball
            _ball = new Ball('@', ConsoleColor.White, new double[] { (Console.WindowWidth / 2), (Console.WindowHeight - 6) },
                new Vector(0.5, 1.0));
            _ball.ChangeMultiplier(0.5f); // Half the speed
            _register.RegisterItem(_ball);
            _ball.BallMoved += OnBallMoved;

            // Paddle
            _paddle = new Paddle('=', ConsoleColor.White,
                new double[] { (Console.WindowWidth / 2) - 3, (Console.WindowHeight - 5) });
            _paddle.ChangeMultiplier(1.5f); // Change the paddles delta-time refresh rate
            _register.RegisterItem(_paddle);
            _ball.BallMoved += _paddle.OnBallMoved; // Set the paddle up for collision
            
        }

        private void FillBlocks()
        {
            _blocks = new List<Block>(); // Create an empty list for the blocks

            // The game level, number corresponds to the difficulty of the block
            var difficultyMap = new [,]
            {
                {2, 1, 0, 1, 2 },
                {0, 2, 1, 2, 0 },
                {1, 1, 2, 1, 1 },
                {0, 2, 1, 2, 0 },
                {2, 1, 0, 1, 2 },
            };
            

            Block tempBlock;
            var topPad = 4;

            // Go through the difficulty map and create the blocks.
            for (var line = 0; line < difficultyMap.GetLength(0); line++) // Each line
            {
                for (var col = 0; col < difficultyMap.GetLength(1); col++) // Each column
                {
                    tempBlock = new Block(difficultyMap[line, col], new double[] { (col * 10), topPad + (line * 2) }, new[] { 10, 2 });
                    tempBlock.BlockBroken += OnBlockBroken; // Add the score tracking
                    _blocks.Add(tempBlock); // Add to the list of blocks
                    _register.RegisterItem(tempBlock); // Register the block to the register
                    _ball.BallMoved += tempBlock.OnBallMoved; // Subscribe the collision event handling
                }
            }
        }

        public override void SelectThisPortrait()
        {
            base.SelectThisPortrait();

            FillRegister(); // Create the main elements
            FillBlocks(); // Create the blocks

            // Update the Name and Score
            _nameAndScore.ChangeText("Hello, " + ResourceManager.PlayerDetails.Name + ". Score: " + ResourceManager.PlayerDetails.HighScore);
        }

        public override void DeSelectThisPortrait()
        {
            base.DeSelectThisPortrait();

            // Move the paddle back to the centre
            _paddle.MoveAbsolute((Console.WindowWidth / 2) - 3);
        }

        public void OnBallMoved(Ball source, BallMovedEventArgs args)
        {
            var ballPosition = args.Position;

            if (ballPosition[1] <= _topWall.StartingLocation()[1])
            {
                ResourceManager.GameWon = true; // Game won
                _manager.SwitchCurrentPortrait((int)PageHandles.EndGame);
            }
            else if (ballPosition[1] >= _bottomWall.StartingLocation()[1])
            {
                _manager.SwitchCurrentPortrait((int)PageHandles.EndGame);
            }
        }

        public void OnBlockBroken(Block source, ScoreEventArgs args)
        {
            // Update the score
            ResourceManager.PlayerDetails = new Score()
            {
                Name = ResourceManager.PlayerDetails.Name,
                HighScore = ResourceManager.PlayerDetails.HighScore + args.Score
            };

            // Update the score on the label
            _nameAndScore.ChangeText("Hello, " + ResourceManager.PlayerDetails.Name + ". Score: " + ResourceManager.PlayerDetails.HighScore);
            source.BlockBroken -= OnBlockBroken; // Unsubscribe the event to prevent the block adding score multiple times

            _register.UnregisterItem(source);
            _ball.BallMoved -= source.OnBallMoved;
        }

        public void OnKeyPress(object source, KeyEventArgs args)
        {
            if (_active)
            {
                switch (args.KeyPressed)
                {
                    case ConsoleKey.Escape:
                        _manager.SwitchCurrentPortrait((int)PageHandles.MainMenu);
                        break;

                    case ConsoleKey.LeftArrow:
                        _paddle.MoveRelativeXaxis(-1);
                        break;

                    case ConsoleKey.RightArrow:
                        _paddle.MoveRelativeXaxis(1);
                        break;
                }
            }
        }

        public override void Tick()
        {
            base.Tick();

            _ball.Tick();
        }
    }
}
