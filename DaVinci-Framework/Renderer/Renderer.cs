using System;
using DaVinci_Framework.Renderer.Resources;

namespace DaVinci_Framework.Renderer
{
    /// <summary>
    /// The renderer ties together the registers and the screen object
    /// </summary>
    public class Renderer
    {
        private Register _currentRegister; // Register stores the items for the current screen
        private Screen _screen; // Stores the pixels for the screen
        private int[] _screenSize; // The size of the screen

        public Renderer(Register initialRegister)
        {
            SetRegister(initialRegister); // Set the register to fetch the items from
            _screen = new Screen(); // Create a new screen object
            _screen.ClearScreen(); // Clear the screen
            _screenSize = initialRegister.getScreenSize(); // The screen size the current register requires
        }

        /// <summary>
        /// Set a new register to fetch items from. This allows different screens to be drawn through registers.
        /// </summary>
        /// <param name="newRegister">The new register object</param>
        public void SetRegister(Register newRegister)
        {
            // Check the screen size is correct
            if (newRegister.getScreenSize() != _screenSize)
            {
                _screenSize = newRegister.getScreenSize(); // Change the screen size if it's different from the previous portrait
                FitScreenSize();
            }

            _currentRegister = newRegister; // Set the new register
            if (_screen != null)
                _screen.ClearScreen();
            _currentRegister.Refresh();



        }

        private void FitScreenSize()
        {
            Console.SetWindowSize(_screenSize[0], _screenSize[1]);
            _screen = new Screen();
            _screen.ClearScreen();
        }

        /// <summary>
        /// Changes the screen object with any changes from the register.
        /// </summary>
        /// <returns>The current screen</returns>
        public Screen Render()
        {
            //Console.Clear();

            var itemsToRender = _currentRegister.GetChangedItems(); // Gets a list of items that have changed since last render.
            var deletedItems = _currentRegister.GetDeletedItems();
            

            foreach (var item in itemsToRender) // Go through each changed item
            {
                _screen.AddToScreen(new int[] {(int)Math.Floor(item.PreviousLocation()[0]), (int)Math.Floor(item.PreviousLocation()[1]) }, item.ItemPixels(true)); // Clear where the item used to be.

                _screen.AddToScreen(new int[] { (int)Math.Floor(item.StartingLocation()[0]), (int)Math.Floor(item.StartingLocation()[1]) }, item.ItemPixels()); // Draw the item in the new position
            }

            foreach (var item in deletedItems)
            {
                _screen.AddToScreen(new int[] { (int)Math.Floor(item.StartingLocation()[0]), (int)Math.Floor(item.StartingLocation()[1]) }, item.ItemPixels(true)); // Clear where the item used to be.
            }



            return _screen; // Return the new screen object
        }

    }
}
