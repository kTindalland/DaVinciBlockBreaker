using System;

namespace DaVinci_Framework.Renderer.Resources
{
    /// <summary>
    /// The screen holds the pixel array for the screen.
    /// </summary>
    public class Screen
    {
        private Pixel[,] _screen; // The pixel array for the current screen.

        /// <summary>
        /// Instantiate a new screen
        /// </summary>
        public Screen()
        {
            _screen = new Pixel[Console.WindowWidth, Console.WindowHeight]; // Create the pixel array for the screen
            ClearScreen(); // Fill in the screen with blank pixels
        }

        /// <summary>
        /// Adds the pixels of the item to the screen pixel array
        /// </summary>
        /// <param name="startingPosition">The top left corner of the item</param>
        /// <param name="itemPixels">The array of pixels that the item returns</param>
        public void AddToScreen(int[] startingPosition, Pixel[,] itemPixels)
        {
            var itemWidth = itemPixels.GetLength(0); // Get the width of the item
            var itemHeight = itemPixels.GetLength(1); // Get the height of the item
            var startx = startingPosition[0]; // The starting position of the X-Axis
            var starty = startingPosition[1]; // The starting position of the Y-Axis

            for (var i = 0; i < itemHeight; i++) // Go through the 2D array of item pixels
            {
                for (var j = 0; j < itemWidth; j++)
                {
                    _screen[startx + j, starty + i] = itemPixels[j, i]; // Add the pixels to the correct place in the screen.
                }
            }
        }

        /// <summary>
        /// Fills the screen out with blank pixels
        /// </summary>
        public void ClearScreen()
        {
            for (var i = 0; i < _screen.GetLength(0); i++) // Go through each item in the screen
            {
                for (var j = 0; j < _screen.GetLength(1); j++)
                {
                    _screen[i, j] = new Pixel(ConsoleColor.Black); // Place a blank pixel at the place
                }
            }
        }

        /// <summary>
        /// Return the screen object
        /// </summary>
        /// <returns>The screen object</returns>
        public Pixel[,] ReturnScreen()
        {
            return _screen;
        }
    }
}
