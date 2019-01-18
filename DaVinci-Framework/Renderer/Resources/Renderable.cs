using System;

namespace DaVinci_Framework.Renderer.Resources
{
    public class Renderable
    {
        protected bool _changed; // If the item has been moved / altered
        protected double[] _position; // The top left position of the item
        protected double[] _prevPosition; // The previous top left position of the item
        protected bool _delete; // Marks if the item needs to be deleted

        /// <summary>
        /// The base constructor of a renderable
        /// </summary>
        /// <param name="initialPosition">The starting position of the item</param>
        public Renderable(double[] initialPosition)
        {
            // Set all the private variables
            _changed = true;
            _delete = false;
            _position = initialPosition;
            _prevPosition = _position;
        }

        /// <summary>
        /// Returns if the item has been changed since last render
        /// </summary>
        /// <returns>The boolean changed value</returns>
        public bool HasChanged()
        {
            return _changed;
        }

        /// <summary>
        /// Resets the changed boolean
        /// </summary>
        public void ResetChanged(bool value = false)
        {
            _changed = value;
        }

        /// <summary>
        /// Returns the current starting location of the item
        /// </summary>
        /// <returns>The integer array storing the top left position. [x, y]</returns>
        public double[] StartingLocation()
        {
            return _position;
        }

        /// <summary>
        /// Returns the previous starting location of the item
        /// </summary>
        /// <returns>The previous starting location of the item</returns>
        public double[] PreviousLocation()
        {
            return _prevPosition;
        }
        

        public virtual Pixel[,] ItemPixels(bool isBlank = false)
        {
            var pixels = new Pixel[1, 1];

            pixels[0, 0] = new Pixel(ConsoleColor.Black);

            return pixels;
        }

        /// <summary>
        /// Marks the item for deletion
        /// </summary>
        public void Delete()
        {
            _delete = true;
        }

        /// <summary>
        /// Gets if the item needs to be deleted
        /// </summary>
        /// <returns>The value of the deletion flag</returns>
        public bool HasBeenDeleted()
        {
            return _delete;
        }
    }
}
