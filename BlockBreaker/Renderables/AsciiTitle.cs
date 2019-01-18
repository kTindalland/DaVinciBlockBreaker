using DaVinci_Framework.Renderer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockBreaker.Renderables
{
    public class AsciiTitle : Renderable
    {
        private string[] _text; // The strings that make up the title
        private ConsoleColor _textColor; // The color of the title

        public AsciiTitle(ConsoleColor textColor, double[] initialPosition) : base(initialPosition)
        {
            // Assign private variables
            _textColor = textColor;

            _text = new[]
            {
                "__________ .__                    __                                    ",
                "\\______   \\|  |    ____    ____  |  | __                                ",
                " |    |  _/|  |   /  _ \\ _/ ___\\ |  |/ /                                ",
                " |    |   \\|  |__(  <_> )\\  \\___ |    <                                 ",
                " |______  /|____/_\\____/_ \\___  >|__|_ \\           __                   ",
                "        \\/     \\______   \\____\\/_   __\\/  _____   |  | __  ____ _______ ",
                "                |    |  _/\\_  __ \\_/ __ \\ \\__  \\  |  |/ /_/ __ \\\\_  __ \\",
                "                |    |   \\ |  | \\/\\  ___/  / __ \\_|    < \\  ___/ |  | \\/",
                "                |______  / |__|    \\___  >(____  /|__|_ \\ \\___  >|__|   ",
                "                       \\/              \\/      \\/      \\/     \\/        ",
            };
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
        /// Get the pixel array for the title
        /// </summary>
        /// <param name="isBlank">Whether or not it spits out blank pixels</param>
        /// <returns>A pixel array</returns>
        public override Pixel[,] ItemPixels(bool isBlank = false)
        {
            var pixels = new Pixel[_text[0].Length, _text.Length];

            for (var y = 0; y < _text.Length; y++) // Go through each character
            {

                for (var x = 0; x < _text[0].Length; x++)
                {
                    if (!isBlank)
                    {
                        pixels[x, y] = new Pixel(_text[y][x], _textColor, ConsoleColor.Black);
                    }
                    else
                    {
                        pixels[x, y] = new Pixel(ConsoleColor.Black);
                    }
                }

            }

            return pixels;
        }
    }
}
