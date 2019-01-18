using System;

namespace DaVinci_Framework.Renderer.Resources
{
    /// <summary>
    /// A struct that will store color and text details for each pixel in the console.
    /// </summary>
    public struct Pixel
    {
        private readonly char _pixelText; // The text painted within the pixel
        private readonly ConsoleColor _textColor; // The color of the text
        private readonly ConsoleColor _backgroundColor; // The color of the background
        private bool _isNew; // If the pixel has been drawn yet.

        /// <summary>
        /// Pixel constructor for rectangle use, paints a blank pixel with just a background.
        /// </summary>
        /// <param name="backgroundColor">Color of the rectangle.</param>
        public Pixel(ConsoleColor backgroundColor)
        {
            // Assign private variables
            _pixelText = ' ';
            _textColor = ConsoleColor.White;
            _backgroundColor = backgroundColor;
            _isNew = true;
        }

        /// <summary>
        /// Pixel constructor for text use, paints a pixel with text within.
        /// </summary>
        /// <param name="text">The text the pixel will paint.</param>
        /// <param name="textColor">The color the text will be.</param>
        /// <param name="backgroundColor">The color the background will be (defaulted to black).</param>
        public Pixel(char text, ConsoleColor textColor, ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            // Assign private variables
            _pixelText = text;
            _textColor = textColor;
            _backgroundColor = backgroundColor;
            _isNew = true;
        }

        /// <summary>
        /// Paint the pixel to the console.
        /// </summary>
        public void PaintPixel()
        {
            // Set the background and foreground colors
            Console.BackgroundColor = _backgroundColor;
            Console.ForegroundColor = _textColor;

            // Write the text
            Console.Write(_pixelText);

            // Reset the console
            Console.ResetColor();

            _isNew = false;
        }

        /// <summary>
        /// Returns if the pixel has been drawn yet
        /// </summary>
        /// <returns>The isNew boolean of the pixel</returns>
        public bool IsNew()
        {
            return _isNew;
        }
    }
}
