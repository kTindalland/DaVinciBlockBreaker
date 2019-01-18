using BlockBreaker.Game.Resources;
using BlockBreaker.Resources;
using DaVinci_Framework.Renderer.Resources;
using System;

namespace BlockBreaker.Renderables
{
    public class Ball : MoveableRenderable
    {
        private char _skin; // What the ball looks like
        private ConsoleColor _colour; // Colour of the ball
        public Vector Direction; // Direction the ball is moving

        public Ball(char skin, ConsoleColor colour, double[] initialPosition, Vector startingVector) : base(initialPosition)
        {
            // Assign private variables
            _skin = skin;
            _colour = colour;
            Direction = startingVector;
        }

        public override Pixel[,] ItemPixels(bool isBlank = false)
        {
            var pixels = new Pixel[1, 1];

            if (!isBlank)
            {
                pixels[0, 0] = new Pixel(_skin, _colour);
            }
            else
            {
                pixels[0, 0] = new Pixel(ConsoleColor.Black);
            }

            return pixels;
        }

        /// <summary>
        /// Move the Ball
        /// </summary>
        public void Tick()
        {
            var newPos = new double[] { _position[0] + Direction.XComponent, _position[1] - Direction.YComponent};

            Move(newPos);
        }

        public override void Move(double[] newPosition)
        {
            base.Move(newPosition);

            if (_position[0] >= Console.WindowWidth)
            {
                _position = new double[] { Console.WindowWidth - 1, _position[1] };
                Direction = new Vector(-Direction.XComponent, Direction.YComponent);
            }
                

            if (_position[0] < 0)
            {
                _position = new double[] { 0, _position[1] };
                Direction = new Vector(-Direction.XComponent, Direction.YComponent);
            }
                

            var args = new BallMovedEventArgs() { Position = _position };
            OnBallMoved(args);
        }

        public delegate void BallMovedHandler(Ball source, BallMovedEventArgs args);
        public event BallMovedHandler BallMoved;

        public void OnBallMoved(BallMovedEventArgs args)
        {
            if (BallMoved != null)
                BallMoved(this, args);
        }
    }
}
