using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaVinci_Framework.Renderer.Resources;

namespace DaVinci_Framework.Display
{
    /// <summary>
    /// Takes the screen object and displays it to the screen
    /// </summary>
    public class Display
    {
        /// <summary>
        /// Paints all the pixels to the screen
        /// </summary>
        /// <param name="screen">The screen object that holds all the pixels</param>
        public void PaintScreen(Screen screen)
        {
            var pixels = screen.ReturnScreen(); // The array of pixels that needs painting to the screen
            var screenWidth = pixels.GetLength(0); // The width of the pixel array
            var screenHeight = pixels.GetLength(1); // The height of the pixel array

            for (var y = 0; y < screenHeight; y++) // Go through the width and height
            {
                for (var x = 0; x < screenWidth; x++)
                {
                    if (pixels[x, y].IsNew()) // If the pixel hasn't been drawn before
                    {
                        Console.SetCursorPosition(x, y); // Go to the cursory position
                        pixels[x, y].PaintPixel(); // Draw the pixel
                    }
                }
            }
            Console.SetCursorPosition(0, 0); // Set the cursor position to 0,0: this is to counter the console scrolling down
        }
    }
}
