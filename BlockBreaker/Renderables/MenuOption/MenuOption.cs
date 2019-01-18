using DaVinci_Framework.DefaultRenderables;
using DaVinci_Framework.Renderer.Resources;
using System;

namespace BlockBreaker.Renderables.MenuOption
{
    public class MenuOption : Text
    {
        private string _workingText; // The text that gets painted to the screen
        private bool _selected; // If the current item is selected
        private int _maxLen; // The maximum length off all other options
        private int _prevTextLength; // The previous text length
        private OptionName _option; // The option this item represents

        public MenuOption(string text, ConsoleColor textColor, double[] initialPosition) : base(text, textColor, initialPosition)
        {
            // Assign all the private variables
            _workingText = text;
            _prevTextLength = text.Length;

            switch (_text)
            {
                case "Play Game":
                    _option = OptionName.Play;
                    break;

                case "Options":
                    _option = OptionName.Options;
                    break;

                case "Highscores":
                    _option = OptionName.Highscores;
                    break;

                case "Exit":
                    _option = OptionName.Exit;
                    break;
            }
        }

        private void Move(double[] newPosition)
        {
            _changed = true; // Flag up that the item has changed
            _prevPosition = _position; // Store the old position in the previous position variable
            _position = newPosition; // Overwrites the position with the new position
        }

        public void Select()
        {
            _selected = true;
            _workingText = ("[" + _text).PadRight(_maxLen) + "]"; // Wrap the text in brackets
            Move(new[] { _position[0] - 1, _position[1] }); // Move one space backwards
        }

        public void DeSelect()
        {
            _selected = false;
            _workingText = _text; // Get rid of the brackets
            Move(new[] { _position[0] + 1, _position[1] }); // Move back to the origical position
        }

        public void SetMaxLen(int maxlen)
        {
            _maxLen = maxlen + 5; // Set the max len, with an extra 5 pixels buffer
        }

        public OptionName getOptionName()
        {
            return _option;
        }

        public override Pixel[,] ItemPixels(bool isBlank = false)
        {
            var newtextColor = _textColor; // The original colour

            if (_selected)
            {
                newtextColor = ConsoleColor.Red; // If selected, red
            }

            Pixel[,] pixels;
            

            if (!isBlank)
            {
                pixels = new Pixel[_workingText.Length, 1]; // Define the size of the pixel array
                for (int i = 0; i < _workingText.Length; i++)
                {
                    pixels[i, 0] = new Pixel(_workingText[i], newtextColor, _backgroundColor); // Insert pixel with text content
                }
            }
            else
            {
                pixels = new Pixel[_maxLen + 1, 1]; // Define the size of the pixel array
                for (int i = 0; i < _maxLen + 1; i++)
                {
                    pixels[i, 0] = new Pixel(ConsoleColor.Black); // Insert a blank pixel
                }
            }

            return pixels; // Return the pixels
        }
    }
}
