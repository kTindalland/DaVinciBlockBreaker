using BlockBreaker.Game.Resources;
using BlockBreaker.Resources;
using DaVinci_Framework.Renderer.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBreaker.Renderables
{
    public class Block : Renderable
    {
        private ConsoleColor[] _difficultyColours; // The different colours of the difficulties
        private int _difficulty; // How many hits it takes to break this block
        private int _scoreWorth; // How much score this block is worth to break
        private int[] _dimensions; // The dimensions of the block (width, height)
        private bool _canBeHit; // To delay hits

        public Block(int difficulty, double[] initialPosition, int[] dimensions) : base(initialPosition)
        {
            // Assign private variables
            _difficultyColours = new ConsoleColor[]
            {
                ConsoleColor.Green,
                ConsoleColor.Yellow,
                ConsoleColor.Red
            };

            _canBeHit = true;
            _difficulty = difficulty;
            _dimensions = dimensions;

            // Calculates how much score this block is worth
            _scoreWorth = (_difficulty * 100) + 50;
        }

        public override Pixel[,] ItemPixels(bool isBlank = false)
        {
            var pixels = new Pixel[_dimensions[0], _dimensions[1]];

            for (int i = 0; i < _dimensions[1]; i++) // The Y axis
            {
                for (int j = 0; j < _dimensions[0]; j++) // The X axis
                {
                    if (!isBlank)
                    {
                        pixels[j, i] = new Pixel(_difficultyColours[_difficulty]);
                    }
                    else
                    {
                        pixels[j, i] = new Pixel(ConsoleColor.Black);
                    }

                }
            }

            return pixels;
        }

        /// <summary>
        /// When the block has been hit the required amount of times, this method is called
        /// </summary>
        public void Break()
        {
            var args = new ScoreEventArgs() { Score = _scoreWorth }; // Tell the subscriber how many points this block was worth
            OnBlockBroken(args); // Publish event
        }

        private bool InXBounds(double[] objectPosition)
        {
            if (objectPosition[0] >= _position[0] && objectPosition[0] <= (_position[0] + _dimensions[0]))
            {
                return true;
            }

            return false;
        }

        private bool InYBounds(double[] objectPosition)
        {
            if (objectPosition[1] >= _position[1] && objectPosition[1] <= (_position[1] + _dimensions[1]))
            {
                return true;
            }

            return false;
        }

        public void Hit()
        {
            _difficulty -= 1;
            _changed = true;

            if (_difficulty < 0)
            {
                _difficulty = 0;
                Break();
            }

            _canBeHit = false;

            var hitTimer = new System.Timers.Timer(200);
            hitTimer.Enabled = true;
            hitTimer.Elapsed += OnTimedEvent;
            hitTimer.Start();

        }

        public delegate void ScoreEventHandler(Block source, ScoreEventArgs args);

        public event ScoreEventHandler BlockBroken;

        public void OnBlockBroken(ScoreEventArgs args)
        {
            if (BlockBroken != null)
                BlockBroken(this, args);
        }

        public void OnBallMoved(Ball source, BallMovedEventArgs args)
        {
            var ballPosition = args.Position;
            var ballPrevPosition = new double[] { ballPosition[0] - source.Direction.XComponent, ballPosition[1] + source.Direction.YComponent};

            if (InXBounds(ballPosition)) // If in the x-boundaries
            {
                if (InYBounds(ballPosition)) // If in the y-boundaries
                {

                    if (InXBounds(ballPrevPosition))
                    {

                        if (_canBeHit)
                        {
                            source.Direction = new Vector(source.Direction.XComponent, -source.Direction.YComponent);
                            Hit();
                        }
                        
                    }
                    else
                    {

                        if (_canBeHit)
                        {
                            source.Direction = new Vector(-source.Direction.XComponent, source.Direction.YComponent);
                            Hit();
                        }
                        
                    }
                    

                }
            }
        }

        public void OnTimedEvent(object source, EventArgs args)
        {
            _canBeHit = true;
        }
    }
}
