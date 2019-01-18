using DaVinci_Framework.Renderer.Resources;
using System;

namespace DaVinci_Framework.DefaultRenderables
{
    public class Text : Renderable
    {
        protected string _text; // The actual text content of the item
        protected ConsoleColor _textColor; // The colour of the text content of the item
        protected ConsoleColor _backgroundColor; // The background colour of the item
        private bool _textChanged; // Boolean to store whether or not the text has changed from last display
        private string _newText; // Buffer for the new text to sit in for a tick

        /// <summary>
        /// Instantiate a new Text object: For default background colour
        /// </summary>
        /// <param name="text">The text content of the item</param>
        /// <param name="textColor">The colour of the text content</param>
        /// <param name="position">The top left position of the item</param>
        public Text(string text, ConsoleColor textColor, double[] initialPosition) : base(initialPosition)
        {
            // Set all the private variables
            _text = text;
            _textColor = textColor;
            _backgroundColor = ConsoleColor.Black;
            _textChanged = false;
        }

        /// <summary>
        /// Instantiate a new Text object: For manual background colour
        /// </summary>
        /// <param name="text">The text content of the item</param>
        /// <param name="textColor">The colour of the text content</param>
        /// <param name="backgroundColor">The background colour of the item</param>
        /// <param name="position">The top left position of the item</param>
        public Text(string text, ConsoleColor textColor, ConsoleColor backgroundColor, double[] initialPosition) : base(initialPosition)
        {
            // Set all the private variables
            _text = text;
            _textColor = textColor;
            _backgroundColor = backgroundColor;
            _textChanged = false;
        }

        /// <summary>
        /// Change the text colour of the
        /// </summary>
        /// <param name="textColor"></param>
        public void ChangeTextColor(ConsoleColor textColor)
        {
            _changed = true; // Flag up that the item has changed
            _textColor = textColor; // Overwrite the text colour of the object
        }

        /// <summary>
        /// Returns an array of pixels that describe the item
        /// </summary>
        /// <param name="isBlank">Whether or not the pixels are blank or real</param>
        /// <returns>An array of pixels</returns>
        /// <summary>
        /// Returns an array of pixels that describe the item
        /// </summary>
        /// <param name="isBlank">Whether or not the pixels are blank or real</param>
        /// <returns>An array of pixels</returns>
        public override Pixel[,] ItemPixels(bool isBlank = false)
        {
            var pixels = new Pixel[_text.Length, 1]; // Define the size of the pixel array

            for (int i = 0; i < _text.Length; i++) // Go through the length of the text
            {
                if (!isBlank) // If not returning blank pixels
                {
                    pixels[i, 0] = new Pixel(_text[i], _textColor, _backgroundColor); // Insert pixel with text content
                }
                else if (_textChanged)
                {
                    pixels[i, 0] = new Pixel(ConsoleColor.Black); // Insert a blank pixel
                }
                else
                {
                    pixels[i, 0] = new Pixel(ConsoleColor.Black); // Insert a blank pixel
                }

            }

            if (_textChanged) // If textChanged, then update the text variable and set the flag back to false
            {
                _text = _newText;
                _textChanged = false;
            }

            return pixels; // Return the pixels
        }

        /// <summary>
        /// Changes the text displayed by this object
        /// </summary>
        /// <param name="newText">The text you want to replace the old variable</param>
        public void ChangeText(string newText)
        {
            _newText = newText;
            _textChanged = true;
            _changed = true; // Set changed flag to make sure it gets updated on screen
        }

        /// <summary>
        /// Return the currently displayed text
        /// </summary>
        /// <returns>The currently displayed text</returns>
        public string ReturnText()
        {
            return _text;
        }
    }
}