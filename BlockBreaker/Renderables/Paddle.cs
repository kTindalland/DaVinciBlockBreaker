using BlockBreaker.Game.Resources;
using BlockBreaker.Resources;
using DaVinci_Framework.Renderer.Resources;
using System;
using System.Diagnostics;

namespace BlockBreaker.Renderables
{
    public class Paddle : MoveableRenderable
    {
        private ConsoleColor _colour; // The colour of the paddle
        private char _infill; // What the paddle is made of
        private bool _canColide; // If the paddle can collide or not yet

        public Paddle(char infill, ConsoleColor colour, double[] initialPosition) : base(initialPosition)
        {
            // Assign private variables
            _infill = infill;
            _colour = colour;
            _canColide = true;
        }

        public override Pixel[,] ItemPixels(bool isBlank = false)
        {
            var len = 6; // Length of paddle
            var pixels = new Pixel[len, 1]; // Create the pixel array

            // Fill in the pixels
            for (int i = 0; i < len; i++)
            {
                if (!isBlank)
                {
                    pixels[i, 0] = new Pixel(_infill, _colour);
                }
                else
                {
                    pixels[i, 0] = new Pixel(ConsoleColor.Black);
                }

            }

            return pixels;
        }

        /// <summary>
        /// Moves the paddle change amount on the x axis
        /// </summary>
        /// <param name="change">How much it has moved</param>
        public void MoveRelativeXaxis(double change)
        {
            var newX = _position[0] + change; // Get the new x position

            if (!(newX < 0 || newX > (Console.WindowWidth - 6))) // Check the number is still valid
            {
                Move(new double[] { newX, _position[1] }); // Move the paddle
            }
        }

        public void MoveAbsolute(double newX)
        {
            if (!(newX < 0 || newX > (Console.WindowWidth - 6))) // Check the number is still valid
            {
                Move(new double[] { newX, _position[1] }); // Move the paddle
            }
        }

        /// <summary>
        /// Calls when the ball moves.
        /// </summary>
        /// <param name="source">The source of the event</param>
        /// <param name="args">The arguments passed through</param>
        public void OnBallMoved(Ball source, BallMovedEventArgs args)
        {
            var ballPosition = args.Position;

            if (_canColide)
            {
                if (ballPosition[0] >= _position[0] && ballPosition[0] <= (_position[0] + 6))
                {
                    if (ballPosition[1] >= _position[1] - 1.0 && ballPosition[1] <= _position[1] + 1.0)
                    {
                        _changed = true;
                        var changeMap = new double[] { -0.4, -0.2, 0, 0, 0.2, 0.4 }; // How much the balls vector changes

                        var mapIndex = (int)Math.Floor(ballPosition[0] - _position[0]);

                        try
                        {
                            source.Direction = new Vector(source.Direction.XComponent + changeMap[mapIndex],
                                -source.Direction.YComponent);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            source.Direction = new Vector(source.Direction.XComponent, -source.Direction.YComponent);
                            Debug.Print("Caught Exception : IndexOutOfRange in Paddle Collision"); // Log the error
                        }

                        _canColide = false; // Cannot collide

                        // Set a timer for the collision to reactivate after 0.2 seconds
                        var collideTimer = new System.Timers.Timer(200) {Enabled = true};
                        collideTimer.Elapsed += OnTimedEvent;
                        collideTimer.Start();
                    }
                }
            }
            
        }

        /// <summary>
        /// When the timer has elapsed.
        /// </summary>
        /// <param name="source">The timer</param>
        /// <param name="args">The arguments passed through</param>
        public void OnTimedEvent(object source, EventArgs args)
        {
            // Enable collision
            _canColide = true;
        }
    }
}
